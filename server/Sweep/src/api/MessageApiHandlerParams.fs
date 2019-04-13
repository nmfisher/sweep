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

    //#region Header parameters
    [<CLIMutable>]
    type GetMessageByIdHeaderParams = {
      apiKey : string option;
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
      headerParams:GetMessageByIdHeaderParams;
      pathParams:GetMessageByIdPathParams;
    }

    //#region Header parameters
    [<CLIMutable>]
    type ListMessagesHeaderParams = {
      apiKey : string option;
    }
    //#endregion

    
    type ListMessagesDefaultStatusCodeResponse = {
      content:Message[];
      
    }
    type ListMessagesResult = ListMessagesDefaultStatusCode of ListMessagesDefaultStatusCodeResponse

    type ListMessagesArgs = {
      headerParams:ListMessagesHeaderParams;
    }
    