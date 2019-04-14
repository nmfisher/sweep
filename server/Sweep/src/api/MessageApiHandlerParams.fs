namespace Sweep

open Sweep.Model.Message
open System.Collections.Generic

module MessageApiHandlerParams = 

    //#region Path parameters
    [<CLIMutable>]
    type GetMessageByIdPathParams = {
      messageId : string ;
    }
    //#endregion

    
    type GetMessageByIdDefaultStatusCodeResponse = {
      content:Message;
      
    }
    
    type GetMessageByIdStatusCode404Response = {
      content:string;
      
    }
    type GetMessageByIdResult = GetMessageByIdDefaultStatusCode of GetMessageByIdDefaultStatusCodeResponse|GetMessageByIdStatusCode404 of GetMessageByIdStatusCode404Response

    type GetMessageByIdArgs = {
      pathParams:GetMessageByIdPathParams;
    }

    
    type ListMessagesDefaultStatusCodeResponse = {
      content:Message[];
      
    }
    type ListMessagesResult = ListMessagesDefaultStatusCode of ListMessagesDefaultStatusCodeResponse

    