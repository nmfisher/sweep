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

module CustomHandlers = 

  let cookieAuth (o : CookieAuthenticationOptions) =
    do
        o.Cookie.HttpOnly     <- false
        o.Cookie.SecurePolicy <- CookieSecurePolicy.SameAsRequest
        o.Cookie.SameSite <- SameSiteMode.None
        o.SlidingExpiration   <- true
        o.ExpireTimeSpan      <- TimeSpan.FromDays 7.0

  let fetchOrCreateUser id email  = 
    match CompositionRoot.getUser id with 
    | Some u -> 
        u.OrganizationId
    | None ->
        let apiKey = ApiKey.generate().ToString()
        CompositionRoot.saveUser id email apiKey (Guid.NewGuid().ToString()) |> ignore
        CompositionRoot.saveOrganization id
        match CompositionRoot.getUser id with
        | Some u ->
          u.OrganizationId
        | None ->
          raise (Exception("Unknown error occurred saving user."))

  let onCreatingTicket name (ctx:OAuthCreatingTicketContext) = 
    task {
        let id = (ctx.User.["id"].ToString())
        let email = (ctx.User.["email"].ToString())
        let orgId = fetchOrCreateUser id email 

        ctx.Identity.AddClaim(Claim(ClaimTypes.GroupSid, orgId))
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
  
  //let logout = signOut CookieAuthenticationDefaults.AuthenticationScheme >=> text "Logged out"
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
    builder.WithOrigins([|"http://localhost:8080";"http://sweep-development.ngrok.io";|])
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           |> ignore

  let configureApp (app : IApplicationBuilder) = 
    app.UseCors(configureCors)

  let configureWebHost (builder: IWebHostBuilder)  =
      builder
        .UseContentRoot(".")
        .UseWebRoot("wwwroot")

  let configureServices (services:IServiceCollection) (authBuilder:AuthenticationBuilder) = 
    let serviceProvider = services.BuildServiceProvider()
    let settings = serviceProvider.GetService<IConfiguration>()
    // let queue = EventQueue.EventQueue(settings)
    // queue.Start() |> ignore
    services.AddCors()
