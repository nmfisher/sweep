namespace Sweep
open Sweep.Model.Message
open MessageApiHandlerParams
open MessageApiServiceInterface
open System.Collections.Generic
open System
open Giraffe
open UserContext
open CompositionRoot
open Exceptions


module MessageApiServiceImplementation =
    
    //#region Service implementation
    type MessageApiServiceImpl() = 
      interface IMessageApiService with
      
        member this.GetMessageById ctx args =
          try
            let orgId = getOrgId ctx.User.Claims
            let message = CompositionRoot.getMessage args.pathParams.messageId orgId 
            GetMessageByIdDefaultStatusCode { content = message }
          with
          | NotFoundException(msg) ->
            GetMessageByIdStatusCode404 { content = msg }

        member this.ListMessages ctx args =
          let userId = getUserId ctx.User.Claims
          let orgId = getOrgId ctx.User.Claims
          match args.queryParams with 
          | Ok queryParams -> 
            let startDate = match queryParams.startDate with | Some d -> d | None -> DateTime(1970,1,1)
            let endDate = match queryParams.endDate with | Some d -> d | None -> DateTime.Now
            let messages = CompositionRoot.listMessages startDate endDate orgId 
            ListMessagesDefaultStatusCode { content = messages }
          | _ -> 
            ListMessagesStatusCode500 { content = "Malformed query parameter" }

      //#endregion

    let MessageApiService = MessageApiServiceImpl() :> IMessageApiService