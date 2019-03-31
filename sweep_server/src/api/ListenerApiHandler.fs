namespace Sweep

open System.Collections.Generic
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open ListenerApiHandlerParams
open ListenerApiServiceInterface
open ListenerApiServiceImplementation
open ListenerModel

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
                      | DeleteListenerStatusCode400 resolved ->
                            setStatusCode 400 >=> text resolved.content 
                      | DeleteListenerStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region GetListenerById
    /// <summary>
    /// Find Listener by ID
    /// </summary>

    let GetListenerById (pathParams:GetListenerByIdPathParams) : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let serviceArgs = {   pathParams=pathParams;  } : GetListenerByIdArgs
          let result = ListenerApiService.GetListenerById ctx serviceArgs
          return! (match result with 
                      | GetListenerByIdDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
                      | GetListenerByIdStatusCode400 resolved ->
                            setStatusCode 400 >=> text resolved.content 
                      | GetListenerByIdStatusCode404 resolved ->
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

    //#region UpdateListener
    /// <summary>
    /// Update an existing Listener
    /// </summary>

    let UpdateListener  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let! bodyParams = 
            ctx.BindJsonAsync<UpdateListenerBodyParams>()
          let serviceArgs = {    bodyParams=bodyParams } : UpdateListenerArgs
          let result = ListenerApiService.UpdateListener ctx serviceArgs
          return! (match result with 
                      | UpdateListenerStatusCode400 resolved ->
                            setStatusCode 400 >=> text resolved.content 
                      | UpdateListenerStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
                      | UpdateListenerStatusCode405 resolved ->
                            setStatusCode 405 >=> text resolved.content 
          ) next ctx
        }
    //#endregion


    
