namespace Sweep
open EventApiHandlerParams
open System
open Giraffe
open Microsoft.AspNetCore.Http


module EventApiServiceInterface =
    
    //#region Service interface
    type IEventApiService = 
      abstract member AddEvent:HttpContext -> AddEventArgs->AddEventResult
      abstract member GetEventById:HttpContext -> GetEventByIdArgs->GetEventByIdResult
      abstract member ListEvents:HttpContext ->ListEventsResult
    //#endregion