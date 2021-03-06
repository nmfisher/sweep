namespace Sweep

open Sweep.Model.Event
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
              AddEventStatusCode422 { content = "Event name must not be empty" }
            else 
              let orgId = getOrgId ctx.User.Claims
              let event = CompositionRoot.addEvent args.bodyParams.EventName args.bodyParams.Params orgId
              AddEventDefaultStatusCode { content = "OK" }
          with 
          | e ->   
            AddEventStatusCode422 { content = e.ToString() }

        member this.GetEventById ctx args =
          let userId = getUserId ctx.User.Claims
          let orgId = getOrgId ctx.User.Claims
          match CompositionRoot.getEvent args.pathParams.eventId orgId  with 
          | Some e ->
              GetEventByIdDefaultStatusCode { content = e }
          | None ->              
              GetEventByIdStatusCode404 { content = "Event not found" }
           
        member this.ListEvents ctx (args:ListEventsArgs)  =
          let userId = getUserId ctx.User.Claims
          let orgId = getOrgId ctx.User.Claims
          match args.queryParams with 
          | Ok queryParams -> 
            let withActions = match queryParams.withActions with | Some w -> w | None -> false
            let startDate = match queryParams.startDate with | Some s -> s | None -> DateTime(1970, 1, 1)
            let endDate = match queryParams.endDate with | Some s -> s | None -> DateTime.Now;
            let events = CompositionRoot.listEvents orgId withActions startDate endDate |> Seq.toArray
            ListEventsDefaultStatusCode { content = events }
          | _ ->
            ListEventsStatusCode500 { content = "Unknown query parameter" }
          

      //#endregion

    let EventApiService = EventApiServiceImpl() :> IEventApiService