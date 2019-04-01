namespace Sweep

open MessageModel
open System.Collections.Generic

module MessageApiHandlerParams = 

    //#region Path parameters
    [<CLIMutable>]
    type GetmessageByIdPathParams = {
      messageId : string
    }
    //#endregion

    
    type GetmessageByIdDefaultStatusCodeResponse = {
      content:Message;
      
    }
    
    type GetmessageByIdStatusCode400Response = {
      content:string;
      
    }
    
    type GetmessageByIdStatusCode404Response = {
      content:string;
      
    }
    type GetmessageByIdResult = GetmessageByIdDefaultStatusCode of GetmessageByIdDefaultStatusCodeResponse|GetmessageByIdStatusCode400 of GetmessageByIdStatusCode400Response|GetmessageByIdStatusCode404 of GetmessageByIdStatusCode404Response

    type GetmessageByIdArgs = {
      pathParams:GetmessageByIdPathParams;
    }

    
    type ListMessagesDefaultStatusCodeResponse = {
      content:Message;
      
    }
    type ListMessagesResult = ListMessagesDefaultStatusCode of ListMessagesDefaultStatusCodeResponse

    