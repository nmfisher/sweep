namespace Sweep

open EventModel
open LoggedEventModel
open System.Collections.Generic

module EventApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddEventBodyParams = {
      _event : Event
    }
    //#endregion

    
    type AddEventStatusCode405Response = {
      content:string;
      
    }
    type AddEventResult = AddEventStatusCode405 of AddEventStatusCode405Response

    type AddEventArgs = {
      bodyParams:AddEventBodyParams
    }
    //#region Path parameters
    [<CLIMutable>]
    type GetEventByIdPathParams = {
      eventId : int64
    }
    //#endregion

    
    type GetEventByIdDefaultStatusCodeResponse = {
      content:LoggedEvent;
      
    }
    
    type GetEventByIdStatusCode400Response = {
      content:string;
      
    }
    
    type GetEventByIdStatusCode404Response = {
      content:string;
      
    }
    type GetEventByIdResult = GetEventByIdDefaultStatusCode of GetEventByIdDefaultStatusCodeResponse|GetEventByIdStatusCode400 of GetEventByIdStatusCode400Response|GetEventByIdStatusCode404 of GetEventByIdStatusCode404Response

    type GetEventByIdArgs = {
      pathParams:GetEventByIdPathParams;
    }

    
    type ListEventsDefaultStatusCodeResponse = {
      content:LoggedEvent;
      
    }
    type ListEventsResult = ListEventsDefaultStatusCode of ListEventsDefaultStatusCodeResponse

    