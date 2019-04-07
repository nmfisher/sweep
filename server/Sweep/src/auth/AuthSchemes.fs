namespace Sweep

open Microsoft.AspNetCore.Authentication
open Microsoft.AspNetCore.Authentication.Cookies
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Authentication.OAuth
open System
open Giraffe
open FSharp.Control.Tasks.V2.ContextInsensitive
open Microsoft.Extensions.Configuration

module AuthSchemes =

  let accessDenied : HttpHandler = setStatusCode 401 >=> text "Access Denied"

  let buildGoogle (builder:AuthenticationBuilder) name authorizationUrl scopes (settings:IConfiguration) = 
    builder.AddGoogle(fun googleOptions -> CustomHandlers.setOAuthOptions "Google" googleOptions scopes settings)

  let buildGitHub (builder:AuthenticationBuilder) name authorizationUrl scopes (settings:IConfiguration) = 
    builder.AddGitHub(fun githubOptions -> CustomHandlers.setOAuthOptions "GitHub" githubOptions scopes settings)

  let buildOAuth (builder:AuthenticationBuilder) (name:string) authorizationUrl scopes (settings:IConfiguration) = 
    builder.AddOAuth(name, (fun (options:OAuthOptions) -> 
      options.AuthorizationEndpoint <- authorizationUrl
      options.TokenEndpoint <- settings.[name + "TokenUrl"]
      options.CallbackPath <- PathString(settings.[name + "CallbackPath"])
      CustomHandlers.setOAuthOptions "" options scopes settings
  ))

  let OAuthBuilders = Map.empty.Add("Google", buildGoogle).Add("GitHub", buildGitHub)

  let checkEnvironment (settings:IConfiguration) name =
    if (isNull settings.[name + "ClientId"]) then
      raise (Exception(name + "ClientId is not set."))
    else if (isNull settings.[name + "ClientSecret"]) then
      raise (Exception((name + "ClientSecret is not set.")))

  let build settings name =  
    // check that "xxxClientId" and "xxxClientSecret" configuration variables have been set for all OAuth providers
    checkEnvironment settings name
    if OAuthBuilders.ContainsKey(name) then
      OAuthBuilders.[name]
    else
      buildOAuth

  let configureOAuth (settings:IConfiguration) services =
    (build settings "GitHub") services "GitHub" "https://github.com/login/oauth/authorize2" ["user:email";] settings
    (build settings "Google") services "Google" "https://accounts.google.com/o/oauth2/v2/auth" ["https://www.googleapis.com/auth/userinfo.email";] settings

  let cookieAuth (o : CookieAuthenticationOptions) =
    do
        o.Cookie.HttpOnly     <- false
        o.Cookie.SecurePolicy <- CookieSecurePolicy.None
        o.Cookie.SameSite <-  SameSiteMode.None
        o.SlidingExpiration   <- true
        o.ExpireTimeSpan      <- TimeSpan.FromDays 7.0

  let configureCookie (builder:AuthenticationBuilder) =
      builder.AddCookie(cookieAuth)

  let configureServices (services:IServiceCollection) = 
    let serviceProvider = services.BuildServiceProvider()
    let settings = serviceProvider.GetService<IConfiguration>()
    services.AddAuthentication(fun o -> o.DefaultScheme <- CookieAuthenticationDefaults.AuthenticationScheme)
    |> configureOAuth settings 
    |> configureCookie
    
  let (|||) v1 v2 = 
      match v1 with 
      | Some v -> v1
      | None -> v2

  // this can be replaced with ctx.GetCookieValue in Giraffe >= 3.6
  let getCookieValue (ctx:HttpContext) (key : string)  =
        match ctx.Request.Cookies.TryGetValue key with
        | true , cookie -> Some cookie
        | false, _ -> None

  