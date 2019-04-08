namespace Sweep

open Sweep.Model.Event
open Sweep.Model.EventRequestBody
open System.Collections.Generic

module EventApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddEventBodyParams = EventRequestBody
    //#endregion

    
    type AddEventDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type AddEventStatusCode422Response = {
      content:string;
      
    }
    type AddEventResult = AddEventDefaultStatusCode of AddEventDefaultStatusCodeResponse|AddEventStatusCode422 of AddEventStatusCode422Response

    type AddEventArgs = {
      bodyParams:AddEventBodyParams
    }
    //#region Path parameters
    [<CLIMutable>]
    type GetEventByIdPathParams = {
      eventId : string;
    }
    //#endregion

    
    type GetEventByIdDefaultStatusCodeResponse = {
      content:Event;
      
    }
    
    type GetEventByIdStatusCode404Response = {
      content:string;
      
    }
    type GetEventByIdResult = GetEventByIdDefaultStatusCode of GetEventByIdDefaultStatusCodeResponse|GetEventByIdStatusCode404 of GetEventByIdStatusCode404Response

    type GetEventByIdArgs = {
      pathParams:GetEventByIdPathParams;
    }

    
    type ListEventsDefaultStatusCodeResponse = {
      content:Event[];
      
    }
    type ListEventsResult = ListEventsDefaultStatusCode of ListEventsDefaultStatusCodeResponse

    