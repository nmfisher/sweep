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

module CustomHandlers = 

  let cookieAuth (o : CookieAuthenticationOptions) =
    do
        o.Cookie.HttpOnly     <- true
        o.Cookie.SecurePolicy <- CookieSecurePolicy.SameAsRequest
        o.SlidingExpiration   <- true
        o.ExpireTimeSpan      <- TimeSpan.FromDays 7.0

  let onCreatingTicket name (ctx:OAuthCreatingTicketContext) = 
    task {
       ()
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
  
  let logout = signOut CookieAuthenticationDefaults.AuthenticationScheme >=> redirectTo false "/"

  let loginView =
    html [] [
        head [] [
            title [] [ str "Welcome" ]
        ]
        body [] [
            h1 [] [ str "Welcome" ]
            a [_href "/login-with-GitHub"] [ str "Login with GitHub" ]
            a [_href "/login-with-Google"] [ str "Login with Google" ]
            a [_href "/login-with-api_key"] [ str "Login with api_key" ]
        ]
    ]
  
  let redirectToLogin : HttpHandler = 
    htmlView loginView    
  
  let handlers : HttpHandler list = [
    // insert your handlers here
    GET >=> 
      choose [
        route "/login" >=> redirectToLogin
        route "/login-with-GitHub" >=> challenge "GitHub"
        route "/login-with-Google" >=> challenge "Google"
        route "/login-with-api_key" >=> challenge "api_key"
        route "/logout" >=> logout
      ]
  ]

  let configureWebHost (builder: IWebHostBuilder)  =
      // builder
      //  .UseContentRoot("content")
      //  .UseWebRoot("static")
      builder

  let configureApp (app : IApplicationBuilder) =
    app

  let configureServices (services:IServiceCollection) (authBuilder:AuthenticationBuilder) = 
    ()
