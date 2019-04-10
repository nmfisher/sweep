namespace Sweep

open System.Collections.Generic
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open MessageApiHandlerParams
open MessageApiServiceInterface
open MessageApiServiceImplementation
open Sweep.Model.Message

module MessageApiHandler = 

    /// <summary>
    /// 
    /// </summary>

    //#region GetMessageById
    /// <summary>
    /// Find message by ID
    /// </summary>

    let GetMessageById (pathParams:GetMessageByIdPathParams) : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let headerParams = {
              GetMessageByIdHeaderParams.apiKey=ctx.TryGetRequestHeader "apiKey";
          }
          let serviceArgs = { headerParams=headerParams;   pathParams=pathParams;  } : GetMessageByIdArgs
          let result = MessageApiService.GetMessageById ctx serviceArgs
          return! (match result with 
                      | GetMessageByIdDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
                      | GetMessageByIdStatusCode404 resolved ->
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
          let headerParams = {
              ListMessagesHeaderParams.apiKey=ctx.TryGetRequestHeader "apiKey";
          }
          let serviceArgs = { headerParams=headerParams;     } : ListMessagesArgs
          let result = MessageApiService.ListMessages ctx serviceArgs
          return! (match result with 
                      | ListMessagesDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
          ) next ctx
        }
    //#endregion


    
