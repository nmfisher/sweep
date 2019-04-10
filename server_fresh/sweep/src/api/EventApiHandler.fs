namespace Sweep

open System.Collections.Generic
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open EventApiHandlerParams
open EventApiServiceInterface
open EventApiServiceImplementation
open Sweep.Model.Event
open Sweep.Model.EventRequestBody

module EventApiHandler = 

    /// <summary>
    /// 
    /// </summary>

    //#region AddEvent
    /// <summary>
    /// Raise an event
    /// </summary>

    let AddEvent  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let! bodyParams = 
            ctx.BindJsonAsync<AddEventBodyParams>()
          let serviceArgs = {    bodyParams=bodyParams } : AddEventArgs
          let result = EventApiService.AddEvent ctx serviceArgs
          return! (match result with 
                      | AddEventDefaultStatusCode resolved ->
                            setStatusCode 200 >=> text resolved.content 
                      | AddEventStatusCode422 resolved ->
                            setStatusCode 422 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region GetEventById
    /// <summary>
    /// Find raised event by ID
    /// </summary>

    let GetEventById (pathParams:GetEventByIdPathParams) : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let serviceArgs = {   pathParams=pathParams;  } : GetEventByIdArgs
          let result = EventApiService.GetEventById ctx serviceArgs
          return! (match result with 
                      | GetEventByIdDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
                      | GetEventByIdStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region ListEvents
    /// <summary>
    /// List all received events
    /// </summary>

    let ListEvents  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let serviceArgs = {     } : ListEventsArgs
          let result = EventApiService.ListEvents ctx serviceArgs
          return! (match result with 
                      | ListEventsDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
          ) next ctx
        }
    //#endregion


    
