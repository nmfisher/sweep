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
open System.Diagnostics
open Giraffe.GiraffeViewEngine
open EventApiHandlerParams
open ListenerApiHandlerParams
open MessageApiHandlerParams
open TemplateApiHandlerParams
open UserApiHandlerParams
open Giraffe

module App =

  // ---------------------------------
  // Error handler
  // ---------------------------------

  let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(EventId(), ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

  // ---------------------------------
  // Web app
  // ---------------------------------

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

  let HttpGet = GET
  let HttpPost = POST
  let HttpPut = PUT
  let HttpDelete = DELETE

  let webApp =
    choose (CustomHandlers.handlers @ [
      HttpGet >=> route "/login" >=> redirectToLogin
      HttpPost >=> route "/events" >=> requiresAuthentication redirectToLogin >=> EventApiHandler.AddEvent;
      HttpGet >=> routeBind<GetEventByIdPathParams> "/events/{eventId}"  (fun x -> requiresAuthentication redirectToLogin >=> EventApiHandler.GetEventById x);
      HttpGet >=> route "/events" >=> requiresAuthentication redirectToLogin >=> EventApiHandler.ListEvents;
      HttpPost >=> route "/listeners" >=> requiresAuthentication redirectToLogin >=> ListenerApiHandler.AddListener;
      HttpDelete >=> routeBind<DeleteListenerPathParams> "/listeners/{listenerId}"  (fun x -> requiresAuthentication redirectToLogin >=> ListenerApiHandler.DeleteListener x);
      HttpGet >=> routeBind<GetListenerByIdPathParams> "/listeners/{listenerId}"  (fun x -> requiresAuthentication redirectToLogin >=> ListenerApiHandler.GetListenerById x);
      HttpGet >=> route "/listeners" >=> requiresAuthentication redirectToLogin >=> ListenerApiHandler.ListListeners;
      HttpPut >=> routeBind<UpdateListenerPathParams> "/listeners/{listenerId}"  (fun x -> requiresAuthentication redirectToLogin >=> ListenerApiHandler.UpdateListener x);
      HttpGet >=> routeBind<GetMessageByIdPathParams> "/messages/{messageId}"  (fun x -> requiresAuthentication redirectToLogin >=> MessageApiHandler.GetMessageById x);
      HttpGet >=> route "/messages" >=> requiresAuthentication redirectToLogin >=> MessageApiHandler.ListMessages;
      HttpPost >=> route "/templates" >=> requiresAuthentication redirectToLogin >=> TemplateApiHandler.AddTemplate;
      HttpDelete >=> routeBind<DeleteTemplatePathParams> "/templates/{templateId}"  (fun x -> requiresAuthentication redirectToLogin >=> TemplateApiHandler.DeleteTemplate x);
      HttpGet >=> routeBind<GetTemplateByIdPathParams> "/templates/{templateId}"  (fun x -> requiresAuthentication redirectToLogin >=> TemplateApiHandler.GetTemplateById x);
      HttpGet >=> route "/templates" >=> requiresAuthentication redirectToLogin >=> TemplateApiHandler.ListTemplate;
      HttpPut >=> routeBind<UpdateTemplatePathParams> "/templates/{templateId}"  (fun x -> requiresAuthentication redirectToLogin >=> TemplateApiHandler.UpdateTemplate x);
      HttpPost >=> route "/user" >=> requiresAuthentication redirectToLogin >=> UserApiHandler.CreateUser;
      HttpDelete >=> routeBind<DeleteUserPathParams> "/user/{userId}"  (fun x -> requiresAuthentication redirectToLogin >=> UserApiHandler.DeleteUser x);
      HttpGet >=> routeBind<GetUserByNamePathParams> "/user/{userId}"  (fun x -> requiresAuthentication redirectToLogin >=> UserApiHandler.GetUserByName x);
      HttpGet >=> route "/user/login" >=> requiresAuthentication redirectToLogin >=> UserApiHandler.LoginUser;
      HttpGet >=> route "/user/logout" >=> requiresAuthentication redirectToLogin >=> UserApiHandler.LogoutUser;
      HttpPut >=> routeBind<UpdateUserPathParams> "/user/{userId}"  (fun x -> requiresAuthentication redirectToLogin >=> UserApiHandler.UpdateUser x);
      RequestErrors.notFound (text "Not Found") 
    ])
  // ---------------------------------
  // Main
  // ---------------------------------

  let configureApp (app : IApplicationBuilder) =
    app.UseGiraffeErrorHandler(errorHandler)
      .UseStaticFiles()
      .UseAuthentication()
      .UseResponseCaching()
      .UseGiraffe webApp

  let configureServices (services : IServiceCollection) =
    services
          .AddResponseCaching()
          .AddGiraffe()
          |> AuthSchemes.configureServices      
          |> CustomHandlers.configureServices
          |> ignore          
    services.AddDataProtection() |> ignore

  let configureLogging (loggerBuilder : ILoggingBuilder) =
    loggerBuilder.AddFilter(fun lvl -> lvl.Equals LogLevel.Error)
                  .AddConsole()
                  .AddDebug() |> ignore

  [<EntryPoint>]
  let main _ =
    Trace.Listeners.Add(new TextWriterTraceListener(Console.Out)) |> ignore
    WebHost.CreateDefaultBuilder()
          .Configure(Action<IApplicationBuilder> configureApp)
          .ConfigureServices(configureServices)
          .ConfigureLogging(configureLogging)
          .Build()
          .Run()
    0