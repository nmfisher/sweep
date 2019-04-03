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

module CustomHandlers = 

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
        ctx.HttpContext.GetLogger("DEVELOPMENT").LogCritical(ctx.User.ToString())
        let id = (ctx.User.["id"].ToString())
        let email = (ctx.User.["email"].ToString())
        let orgId = fetchOrCreateUser id email 

        ctx.Identity.AddClaim(new Claim(ClaimTypes.GroupSid, orgId))
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

  
  
  let logout = signOut CookieAuthenticationDefaults.AuthenticationScheme >=> text "Logged out"

  let challenge (scheme : string) (redirectUri : string) : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                do! ctx.ChallengeAsync(
                        scheme,
                        AuthenticationProperties(RedirectUri = redirectUri))
                return! next ctx
            }
  
  let handlers : HttpHandler list = [
    // insert your handlers here
    GET >=> 
      choose [
        route "/login-with-GitHub" >=> challenge "GitHub" "/events"
        route "/login-with-Google" >=> challenge "Google" "/events"
        route "/logout" >=> logout
        route "/foo" >=> (fun nxt ctx -> 
            let loggerA = ctx.GetLogger("FOo").LogCritical("FOO!!!!")
            text "FOO" nxt ctx)
      ]
  ]

  let configureServices (services:AuthenticationBuilder) = 
    services
