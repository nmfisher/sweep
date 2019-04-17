namespace Sweep

open Sweep.Model.Message
open System.Collections.Generic
open System

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

    //#region Query parameters
    [<CLIMutable>]
    type ListMessagesQueryParams = {
      startDate : DateTime option;
      

      endDate : DateTime option;
      
    }
    //#endregion

    
    type ListMessagesDefaultStatusCodeResponse = {
      content:Message[];
      
    }
    
    type ListMessagesStatusCode500Response = {
      content:string;
      
    }
    type ListMessagesResult = ListMessagesDefaultStatusCode of ListMessagesDefaultStatusCodeResponse|ListMessagesStatusCode500 of ListMessagesStatusCode500Response

    type ListMessagesArgs = {
      queryParams:Result<ListMessagesQueryParams,string>;
    }
    