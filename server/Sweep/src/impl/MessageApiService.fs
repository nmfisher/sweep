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

        member this.ListMessages ctx =
          let orgId = getOrgId ctx.User.Claims
          let messages = CompositionRoot.listMessages orgId 
          ListMessagesDefaultStatusCode { content = messages }

      //#endregion

    let MessageApiService = MessageApiServiceImpl() :> IMessageApiService