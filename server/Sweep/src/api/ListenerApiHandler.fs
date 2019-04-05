namespace Sweep

open System.Collections.Generic
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open ListenerApiHandlerParams
open ListenerApiServiceInterface
open ListenerApiServiceImplementation
open Sweep.Model.Listener

module ListenerApiHandler = 

    /// <summary>
    /// 
    /// </summary>

    //#region AddListener
    /// <summary>
    /// Create a new Listener
    /// </summary>

    let AddListener  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let! bodyParams = 
            ctx.BindJsonAsync<AddListenerBodyParams>()
          let serviceArgs = {    bodyParams=bodyParams } : AddListenerArgs
          let result = ListenerApiService.AddListener ctx serviceArgs
          return! (match result with 
                      | AddListenerDefaultStatusCode resolved ->
                            setStatusCode 200 >=> text resolved.content 
                      | AddListenerStatusCode405 resolved ->
                            setStatusCode 405 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region DeleteListener
    /// <summary>
    /// Deletes a Listener
    /// </summary>

    let DeleteListener (pathParams:DeleteListenerPathParams) : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let serviceArgs = {   pathParams=pathParams;  } : DeleteListenerArgs
          let result = ListenerApiService.DeleteListener ctx serviceArgs
          return! (match result with 
                      | DeleteListenerDefaultStatusCode resolved ->
                            setStatusCode 200 >=> text resolved.content 
                      | DeleteListenerStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region ListListeners
    /// <summary>
    /// List all Listeners
    /// </summary>

    let ListListeners  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let result = ListenerApiService.ListListeners ctx 
          return! (match result with 
                      | ListListenersDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
          ) next ctx
        }
    //#endregion


    
