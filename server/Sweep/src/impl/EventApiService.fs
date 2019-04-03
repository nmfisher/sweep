namespace Sweep
open EventModel
open LoggedEventModel
open EventApiHandlerParams
open EventApiServiceInterface
open System.Collections.Generic
open System
open Giraffe
open EventApiHandlerParams
open System.Security.Claims

module EventApiServiceImplementation =
    
    //#region Service implementation
    type EventApiServiceImpl() = 
      let getUserId (claims:IEnumerable<Security.Claims.Claim>) = 
        claims
        |> Seq.where (fun (c:Security.Claims.Claim) -> c.Type = ClaimTypes.Email)
        |> Seq.head
        |> (fun (c:Security.Claims.Claim) -> c.Value)

      let getOrgId (claims:IEnumerable<Security.Claims.Claim>) = 
        claims
        |> Seq.where (fun (c:Security.Claims.Claim) -> c.Type = ClaimTypes.GroupSid)
        |> Seq.head
        |> (fun (c:Security.Claims.Claim) -> c.Value)
      
      interface IEventApiService with
        member this.AddEvent ctx args =
          try
            let userId = getUserId ctx.User.Claims
            let orgId = getOrgId ctx.User.Claims
            let event = CompositionRoot.addEvent args.bodyParams orgId
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