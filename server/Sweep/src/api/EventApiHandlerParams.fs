namespace Sweep

open Sweep.Model.Event
open Sweep.Model.EventRequestBody
open System.Collections.Generic
open System

module EventApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddEventBodyParams = EventRequestBody 
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type AddEventHeaderParams = {
      apiKey : string option;
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
      headerParams:AddEventHeaderParams;
      bodyParams:AddEventBodyParams
    }
    //#region Path parameters
    [<CLIMutable>]
    type GetEventByIdPathParams = {
      eventId : string ;
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

    //#region Query parameters
    [<CLIMutable>]
    type ListEventsQueryParams = {
      withActions : bool option;
      

      startDate : DateTime option;
      

      endDate : DateTime option;
      
    }
    //#endregion

    
    type ListEventsDefaultStatusCodeResponse = {
      content:Event[];
      
    }
    
    type ListEventsStatusCode500Response = {
      content:string;
      
    }
    type ListEventsResult = ListEventsDefaultStatusCode of ListEventsDefaultStatusCodeResponse|ListEventsStatusCode500 of ListEventsStatusCode500Response

    type ListEventsArgs = {
      queryParams:Result<ListEventsQueryParams,string>;
    }
    