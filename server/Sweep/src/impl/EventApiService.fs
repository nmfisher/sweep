namespace Sweep
open EventModel
open EventApiHandlerParams
open EventApiServiceInterface
open System.Collections.Generic
open System
open Giraffe
open EventApiHandlerParams
open System.Security.Claims
open UserContext

module EventApiServiceImplementation =
    
    //#region Service implementation
    type EventApiServiceImpl() = 
      
      interface IEventApiService with
        member this.AddEvent ctx args =
          try
            if String.IsNullOrWhiteSpace(args.bodyParams.EventName) then
              AddEventStatusCode405 { content = "Event name must not be empty" }
            else 
              let userId = getUserId ctx.User.Claims
              let orgId = getOrgId ctx.User.Claims
              let event = CompositionRoot.addEvent args.bodyParams.EventName args.bodyParams.Params orgId
              AddEventDefaultStatusCode { content = "OK" }
          with 
          | e ->   
            AddEventStatusCode405 { content = e.ToString() }

        member this.GetEventById ctx args =
          let userId = getUserId ctx.User.Claims
          let orgId = getOrgId ctx.User.Claims
          match CompositionRoot.getEvent args.pathParams.eventId orgId  with 
          | Some e ->
              GetEventByIdDefaultStatusCode { content = e }
          | None ->              
              GetEventByIdStatusCode404 { content = "Event not found" }
           
        member this.ListEvents ctx  =
          let userId = getUserId ctx.User.Claims
          let orgId = getOrgId ctx.User.Claims
          let events = CompositionRoot.listEvents orgId |> Seq.toArray
          ListEventsDefaultStatusCode { content = events }

      //#endregion

    let EventApiService = EventApiServiceImpl() :> IEventApiService