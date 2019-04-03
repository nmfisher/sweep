namespace Sweep

open EventModel
open LoggedEventModel
open System.Collections.Generic

module EventApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddEventBodyParams = Event
    //#endregion

    
    type AddEventDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type AddEventStatusCode405Response = {
      content:string;
      
    }
    type AddEventResult = AddEventDefaultStatusCode of AddEventDefaultStatusCodeResponse|AddEventStatusCode405 of AddEventStatusCode405Response

    type AddEventArgs = {
      bodyParams:AddEventBodyParams
    }
    //#region Path parameters
    [<CLIMutable>]
    type GetEventByIdPathParams = {
      eventId : string
    }
    //#endregion

    
    type GetEventByIdDefaultStatusCodeResponse = {
      content:LoggedEvent;
      
    }
    
    type GetEventByIdStatusCode404Response = {
      content:string;
      
    }
    type GetEventByIdResult = GetEventByIdDefaultStatusCode of GetEventByIdDefaultStatusCodeResponse|GetEventByIdStatusCode404 of GetEventByIdStatusCode404Response

    type GetEventByIdArgs = {
      pathParams:GetEventByIdPathParams;
    }

    
    type ListEventsDefaultStatusCodeResponse = {
      content:LoggedEvent[];
      
    }
    type ListEventsResult = ListEventsDefaultStatusCode of ListEventsDefaultStatusCodeResponse

    