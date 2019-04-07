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

  let HttpGet = GET
  let HttpPost = POST
  let HttpPut = PUT
  let HttpDelete = DELETE

  let authFailure : HttpHandler = 
    setStatusCode 401 >=> text "You must be authenticated to access this resource."

  let webApp =
    choose (CustomHandlers.handlers @ [
      HttpPost >=> route "/1.0.0/events" >=> requiresAuthentication authFailure >=> EventApiHandler.AddEvent;
      HttpGet >=> routeBind<GetEventByIdPathParams> "/1.0.0/events/{eventId}"  (fun x -> requiresAuthentication authFailure >=> EventApiHandler.GetEventById x);
      HttpGet >=> route "/1.0.0/events" >=> requiresAuthentication authFailure >=> EventApiHandler.ListEvents;
      HttpPost >=> route "/1.0.0/listeners" >=> requiresAuthentication authFailure >=> ListenerApiHandler.AddListener;
      HttpDelete >=> routeBind<DeleteListenerPathParams> "/1.0.0/listeners/{listenerId}"  (fun x -> requiresAuthentication authFailure >=> ListenerApiHandler.DeleteListener x);
      HttpGet >=> route "/1.0.0/listeners" >=> requiresAuthentication authFailure >=> ListenerApiHandler.ListListeners;
      HttpGet >=> routeBind<GetMessageByIdPathParams> "/1.0.0/messages/{messageId}"  (fun x -> requiresAuthentication authFailure >=> MessageApiHandler.GetMessageById x);
      HttpGet >=> route "/1.0.0/messages" >=> requiresAuthentication authFailure >=> MessageApiHandler.ListMessages;
      HttpPost >=> route "/1.0.0/templates" >=> requiresAuthentication authFailure >=> TemplateApiHandler.AddTemplate;
      HttpDelete >=> routeBind<DeleteTemplatePathParams> "/1.0.0/templates/{templateId}"  (fun x -> requiresAuthentication authFailure >=> TemplateApiHandler.DeleteTemplate x);
      HttpGet >=> routeBind<GetTemplateByIdPathParams> "/1.0.0/templates/{templateId}"  (fun x -> requiresAuthentication authFailure >=> TemplateApiHandler.GetTemplateById x);
      HttpGet >=> route "/1.0.0/templates" >=> requiresAuthentication authFailure >=> TemplateApiHandler.ListTemplate;
      HttpPut >=> routeBind<UpdateTemplatePathParams> "/1.0.0/templates/{templateId}"  (fun x -> requiresAuthentication authFailure >=> TemplateApiHandler.UpdateTemplate x);
      HttpPost >=> route "/1.0.0/user" >=> requiresAuthentication authFailure >=> UserApiHandler.CreateUser;
      HttpDelete >=> routeBind<DeleteUserPathParams> "/1.0.0/user/{userId}"  (fun x -> requiresAuthentication authFailure >=> UserApiHandler.DeleteUser x);
      HttpGet >=> routeBind<GetUserByNamePathParams> "/1.0.0/user/{userId}"  (fun x -> requiresAuthentication authFailure >=> UserApiHandler.GetUserByName x);
      HttpGet >=> route "/1.0.0/user/login" >=> requiresAuthentication authFailure >=> UserApiHandler.LoginUser;
      HttpGet >=> route "/1.0.0/user/logout" >=> requiresAuthentication authFailure >=> UserApiHandler.LogoutUser;
      HttpPut >=> routeBind<UpdateUserPathParams> "/1.0.0/user/{userId}"  (fun x -> requiresAuthentication authFailure >=> UserApiHandler.UpdateUser x);
      RequestErrors.notFound (text "Not Found") 
    ])
  // ---------------------------------
  // Main
  // ---------------------------------

  let configureApp (app : IApplicationBuilder) =
    app.UseGiraffeErrorHandler(errorHandler)
      .UseStaticFiles()
      .UseAuthentication()
      .UseResponseCaching() |> ignore
    CustomHandlers.configureApp app |> ignore
    app.UseGiraffe webApp |> ignore
    

  let configureServices (services : IServiceCollection) =
    services
          .AddResponseCaching()
          .AddGiraffe()
          |> AuthSchemes.configureServices      
          |> CustomHandlers.configureServices services
          |> ignore
    services.AddDataProtection() |> ignore

  let configureLogging (loggerBuilder : ILoggingBuilder) =
    loggerBuilder.AddFilter(fun lvl -> lvl.Equals LogLevel.Error)
                  .AddConsole()
                  .AddDebug() |> ignore

  [<EntryPoint>]
  let main _ =
    let builder = WebHost.CreateDefaultBuilder()
                    .Configure(Action<IApplicationBuilder> configureApp)
                    .ConfigureServices(configureServices)
                    .ConfigureLogging(configureLogging)
                    |> CustomHandlers.configureWebHost
    builder.Build()
            .Run()
    0