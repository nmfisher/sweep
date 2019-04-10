namespace Sweep

open Sweep.Model.Event
open Sweep.Model.EventRequestBody
open System.Collections.Generic

module EventApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddEventBodyParams = EventRequestBody
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type AddEventHeaderParams = {
      apiKey : Option<string>;
    }
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

    //#region Header parameters
    [<CLIMutable>]
    type GetEventByIdHeaderParams = {
      apiKey : Option<string>;
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

    //#region Header parameters
    [<CLIMutable>]
    type ListEventsHeaderParams = {
      apiKey : Option<string>;
    }
    //#endregion

    
    type ListEventsDefaultStatusCodeResponse = {
      content:Event[];
      
    }
    type ListEventsResult = ListEventsDefaultStatusCode of ListEventsDefaultStatusCodeResponse

    type ListEventsArgs = {
    }
    