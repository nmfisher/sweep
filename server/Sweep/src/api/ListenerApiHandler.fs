namespace Sweep

open System.Collections.Generic
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open ListenerApiHandlerParams
open ListenerApiServiceInterface
open ListenerApiServiceImplementation
open Sweep.Model.Listener
open Sweep.Model.ListenerRequestBody
open Sweep.Model.ListenerTemplate

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
          let serviceArgs = {     bodyParams=bodyParams } : AddListenerArgs
          let result = ListenerApiService.AddListener ctx serviceArgs
          return! (match result with 
                      | AddListenerDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
                      | AddListenerStatusCode422 resolved ->
                            setStatusCode 422 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region AddListenerTemplate
    /// <summary>
    /// Associates a Template to a Listener
    /// </summary>

    let AddListenerTemplate (pathParams:AddListenerTemplatePathParams) : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let serviceArgs = {    pathParams=pathParams;  } : AddListenerTemplateArgs
          let result = ListenerApiService.AddListenerTemplate ctx serviceArgs
          return! (match result with 
                      | AddListenerTemplateDefaultStatusCode resolved ->
                            setStatusCode 200 >=> text resolved.content 
                      | AddListenerTemplateStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
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
          let serviceArgs = {    pathParams=pathParams;  } : DeleteListenerArgs
          let result = ListenerApiService.DeleteListener ctx serviceArgs
          return! (match result with 
                      | DeleteListenerDefaultStatusCode resolved ->
                            setStatusCode 200 >=> text resolved.content 
                      | DeleteListenerStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region DeleteListenerTemplate
    /// <summary>
    /// Disassociates a Template from a Listener
    /// </summary>

    let DeleteListenerTemplate (pathParams:DeleteListenerTemplatePathParams) : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let serviceArgs = {    pathParams=pathParams;  } : DeleteListenerTemplateArgs
          let result = ListenerApiService.DeleteListenerTemplate ctx serviceArgs
          return! (match result with 
                      | DeleteListenerTemplateDefaultStatusCode resolved ->
                            setStatusCode 200 >=> text resolved.content 
                      | DeleteListenerTemplateStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region GetListener
    /// <summary>
    /// Get a listener by ID
    /// </summary>

    let GetListener (pathParams:GetListenerPathParams) : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let serviceArgs = {    pathParams=pathParams;  } : GetListenerArgs
          let result = ListenerApiService.GetListener ctx serviceArgs
          return! (match result with 
                      | GetListenerDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
                      | GetListenerStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region ListListenerTemplates
    /// <summary>
    /// List Templates for Listener
    /// </summary>

    let ListListenerTemplates (pathParams:ListListenerTemplatesPathParams) : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let serviceArgs = {    pathParams=pathParams;  } : ListListenerTemplatesArgs
          let result = ListenerApiService.ListListenerTemplates ctx serviceArgs
          return! (match result with 
                      | ListListenerTemplatesDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
                      | ListListenerTemplatesStatusCode404 resolved ->
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
    /// Updates a Listener
    /// </summary>

    let UpdateListener (pathParams:UpdateListenerPathParams) : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let! bodyParams = 
            ctx.BindJsonAsync<UpdateListenerBodyParams>()
          let serviceArgs = {    pathParams=pathParams; bodyParams=bodyParams } : UpdateListenerArgs
          let result = ListenerApiService.UpdateListener ctx serviceArgs
          return! (match result with 
                      | UpdateListenerDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
                      | UpdateListenerStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
                      | UpdateListenerStatusCode422 resolved ->
                            setStatusCode 422 >=> text resolved.content 
          ) next ctx
        }
    //#endregion


    
