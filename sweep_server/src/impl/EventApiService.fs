namespace Sweep
open EventModel
open LoggedEventModel
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
            CompositionRoot.
            AddEventStatusCode405 { content = content }

        member this.GetEventById ctx args =
          if true then 
            let content = "successful operation" :> obj :?> LoggedEvent // this cast is obviously wrong, and is only intended to allow generated project to compile   
            GetEventByIdDefaultStatusCode { content = content }
          else if true then 
            let content = "Invalid ID supplied" 
            GetEventByIdStatusCode400 { content = content }
          else
            let content = "Order not found" 
            GetEventByIdStatusCode404 { content = content }

        member this.ListEvents ctx  =
            let content = "successful operation" :> obj :?> LoggedEvent // this cast is obviously wrong, and is only intended to allow generated project to compile   
            ListEventsDefaultStatusCode { content = content }

      //#endregion

    let EventApiService = EventApiServiceImpl() :> IEventApiService