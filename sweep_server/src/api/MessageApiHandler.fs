namespace Sweep

open System.Collections.Generic
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open MessageApiHandlerParams
open MessageApiServiceInterface
open MessageApiServiceImplementation
open MessageModel

module MessageApiHandler = 

    /// <summary>
    /// 
    /// </summary>

    //#region GetmessageById
    /// <summary>
    /// Find message by ID
    /// </summary>

    let GetmessageById (pathParams:GetmessageByIdPathParams) : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let serviceArgs = {   pathParams=pathParams;  } : GetmessageByIdArgs
          let result = MessageApiService.GetmessageById ctx serviceArgs
          return! (match result with 
                      | GetmessageByIdDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
                      | GetmessageByIdStatusCode400 resolved ->
                            setStatusCode 400 >=> text resolved.content 
                      | GetmessageByIdStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region ListMessages
    /// <summary>
    /// List all messages
    /// </summary>

    let ListMessages  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let result = MessageApiService.ListMessages ctx 
          return! (match result with 
                      | ListMessagesDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
          ) next ctx
        }
    //#endregion


    
