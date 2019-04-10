namespace Sweep
open Sweep.Model.Event
open Sweep.Model.EventRequestBody
open EventApiHandlerParams
open EventApiServiceInterface
open System.Collections.Generic
open System
open Giraffe

module EventApiServiceImplementation =
    
    //#region Service implementation
    type EventApiServiceImpl() = 
      interface IEventApiService with
      
        member this.AddEvent ctx args =
          if true then 
            let content = "An event has been successfully created." 
            AddEventDefaultStatusCode { content = content }
          else
            let content = "Invalid input" 
            AddEventStatusCode422 { content = content }

        member this.GetEventById ctx args =
          if true then 
            let content = "successful operation" :> obj :?> Event // this cast is obviously wrong, and is only intended to allow generated project to compile   
            GetEventByIdDefaultStatusCode { content = content }
          else
            let content = "Order not found" 
            GetEventByIdStatusCode404 { content = content }

        member this.ListEvents ctx args =
            let content = "successful operation" :> obj :?> Event[] // this cast is obviously wrong, and is only intended to allow generated project to compile   
            ListEventsDefaultStatusCode { content = content }

      //#endregion

    let EventApiService = EventApiServiceImpl() :> IEventApiService