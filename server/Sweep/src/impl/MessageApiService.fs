namespace Sweep
open MessageModel
open MessageApiHandlerParams
open MessageApiServiceInterface
open System.Collections.Generic
open System
open Giraffe

module MessageApiServiceImplementation =
    
    //#region Service implementation
    type MessageApiServiceImpl() = 
      interface IMessageApiService with
      
        member this.GetMessageById ctx args =
          if true then 
            let content = "successful operation" :> obj :?> Message // this cast is obviously wrong, and is only intended to allow generated project to compile   
            GetMessageByIdDefaultStatusCode { content = content }
          else if true then 
            let content = "Invalid ID supplied" 
            GetMessageByIdStatusCode400 { content = content }
          else
            let content = "message not found" 
            GetMessageByIdStatusCode404 { content = content }

        member this.ListMessages ctx  =
            let content = "successful operation" :> obj :?> Message // this cast is obviously wrong, and is only intended to allow generated project to compile   
            ListMessagesDefaultStatusCode { content = content }

      //#endregion

    let MessageApiService = MessageApiServiceImpl() :> IMessageApiService