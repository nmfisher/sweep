namespace Sweep

open System
open System.Net.Http
open System.Security.Claims
open System.Threading
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Http.Features
open Microsoft.AspNetCore.Authentication
open Microsoft.AspNetCore.Authentication.Cookies
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open FSharp.Control.Tasks.V2.ContextInsensitive
open Giraffe
open Giraffe.GiraffeViewEngine
open Microsoft.AspNetCore.Authentication.OAuth
open System.Diagnostics
open Microsoft.AspNetCore.Http
open System.IO
open Microsoft.AspNetCore.Cors.Infrastructure
open TemplateRenderer
open AspNet.Security.ApiKey.Providers.Events
open System.Threading.Tasks
open AspNet.Security.ApiKey.Providers

module CustomHandlers = 

  let cookieAuth (o : CookieAuthenticationOptions) =
    do
        o.Cookie.HttpOnly     <- false
        o.Cookie.SecurePolicy <- CookieSecurePolicy.SameAsRequest
        o.Cookie.SameSite <- SameSiteMode.None
        o.SlidingExpiration   <- true
        o.ExpireTimeSpan      <- TimeSpan.FromDays 7.0

  let fetchOrCreateUser id (email:string) (logger:ILogger)  = 
    match CompositionRoot.getUser id with 
    | Some u -> 
        u
    | None ->
        match CompositionRoot.getUserByUsername email with 
        | Some u -> 
          u
        | None ->
          logger.LogDebug(sprintf "Creating user with ID: %s" id)
          let orgId = (Guid.NewGuid().ToString())
          CompositionRoot.saveUser id email orgId
          let primaryApiKey = (Guid.NewGuid().ToString())
          let secondaryApiKey = (Guid.NewGuid().ToString())
          CompositionRoot.saveOrganization orgId primaryApiKey secondaryApiKey
          match CompositionRoot.getUser id with
          | Some u ->
            u
          | None ->
            raise (Exception("Unknown error occurred saving user."))


  let onCreatingTicket name (ctx:OAuthCreatingTicketContext) = 
    task {
        let id = (ctx.User.["id"].ToString())
        let email = (ctx.User.["email"].ToString())
        let user = fetchOrCreateUser id email (ctx.HttpContext.GetLogger("Sweep.OAuth"))
        ctx.Identity.AddClaim(Claim(ClaimTypes.GroupSid, user.OrganizationId))
    } :> Tasks.Task

  let setOAuthOptions name (options:OAuthOptions) scopes (settings:IConfiguration) = 
    options.ClientId <- settings.[name + "ClientId"]
    options.ClientSecret <- settings.[name + "ClientSecret"]
    for scope in scopes do
      options.Scope.Add scope

    options.Events.OnCreatingTicket <- Func<OAuthCreatingTicketContext,Tasks.Task>(onCreatingTicket name)
    match name with
    | "Google" ->
      ()  
    | "GitHub" ->
      ()
    | _ -> 
      ()

  let setApiKeyEvents name (events:ApiKeyEvents) = 
    events.OnApiKeyValidated <- (fun ctx -> 
      task {
        match CompositionRoot.findOrgByApiKey ctx.ApiKey with
        | Some org ->
          let claims = 
            [|  
              Claim(ClaimTypes.GroupSid, org.Id);
            |]  
          let identity = ClaimsIdentity(claims, ApiKeyDefaults.AuthenticationScheme)
          ctx.HttpContext.User <- ClaimsPrincipal([|identity|])
          ctx.Success()
        | _ -> 
          ctx.HttpContext.GetLogger("Sweep.APIAuth").LogError(sprintf "API key authentication failure : %s" ctx.ApiKey)
          ()
      } :> Task
    )
    events
  
  let signOut (authScheme : string) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            do! ctx.SignOutAsync authScheme
            
            for cookie in ctx.Request.Cookies do
              ctx.Response.Cookies.Delete(cookie.Key)

            return! next ctx
  }

  let redirectToLogin = 
    htmlFile "wwwroot/login.html" 

  let challenge (scheme : string) (redirectUri : string) : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                do! ctx.ChallengeAsync(
                        scheme,
                        AuthenticationProperties(RedirectUri = redirectUri))
                return! next ctx
            }

  let handlers : HttpHandler list = [
    GET >=> 
      choose [
        route "/login-with-GitHub" >=> challenge "GitHub" "/dashboard"
        route "/login-with-Google" >=> challenge "Google" "/dashboard"
        route "/" >=> requiresAuthentication redirectToLogin >=> htmlFile "wwwroot/index.html"
        route "/dashboard" >=> requiresAuthentication redirectToLogin >=> htmlFile "wwwroot/index.html"
        route "/logout" >=> signOut CookieAuthenticationDefaults.AuthenticationScheme >=> redirectToLogin 
      ]
  ]

  let configureCors (builder : CorsPolicyBuilder) =
    builder
    #if DEBUG
      .WithOrigins([|"http://localhost:8080";"http://sweep-development.ngrok.io";"null"|])
    #else     
      .WithOrigins([|"http://app.sweephq.com";"https://app.sweephq.com";|])
    #endif
     .AllowCredentials().AllowAnyMethod().AllowAnyHeader() |> ignore

  let configureApp (app : IApplicationBuilder) = 
    app.UseCors(configureCors)

  let configureWebHost (builder: IWebHostBuilder)  =
      builder
        .UseContentRoot(".")
        .UseWebRoot("wwwroot")

  let configureServices (services:IServiceCollection) (authBuilder:AuthenticationBuilder) = 
    let serviceProvider = services.BuildServiceProvider()
    let settings = serviceProvider.GetService<IConfiguration>()
     
    for k in [|"DefaultFromAddress";"DefaultFromName";"DefaultSubject";"SendGridApiKey"|] do
      if (isNull settings.[k]) then
        raise (Exception(k + " is not set."))

    let mailDefaults = { 
        FromAddress=settings.["DefaultFromAddress"];
        FromName=settings.["DefaultFromName"];
        Subject=settings.["DefaultSubject"];
    }
    services.AddSingleton<MailDefaults>(fun sp -> mailDefaults) |> ignore
    EventQueue.initialize settings.["SendGridApiKey"] mailDefaults 
    services.AddCors()
