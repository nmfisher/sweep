namespace Sweep
open EventModel
open LoggedEventModel
open EventApiHandlerParams
open EventApiServiceInterface
open System.Collections.Generic
open System
open Giraffe
open EventApiHandlerParams

module EventApiServiceImplementation =

    let claimsIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
    
    //#region Service implementation
    type EventApiServiceImpl() = 
      let getUserId (claims:IEnumerable<Security.Claims.Claim>) = 
        claims
        |> Seq.where (fun (c:Security.Claims.Claim) -> c.Type = claimsIdentifier)
        |> Seq.head
        |> (fun (c:Security.Claims.Claim) -> c.Value)
      
      interface IEventApiService with
        member this.AddEvent ctx args =
          try
            let userId = getUserId ctx.User.Claims
            let event = CompositionRoot.addEvent args.bodyParams userId
            AddEventDefaultStatusCode { content = "OK" }
          with 
          | e ->   
            AddEventStatusCode405 { content = e.ToString() }

        member this.GetEventById ctx args =
          let userId = getUserId ctx.User.Claims
          match CompositionRoot.getEvent args.pathParams.eventId userId  with 
          | Some e ->
              GetEventByIdDefaultStatusCode { content = e }
          | None ->              
              GetEventByIdStatusCode404 { content = "Event not found" }
           
        member this.ListEvents ctx  =
          let userId = getUserId ctx.User.Claims
          let events = CompositionRoot.listEvents userId
          raise (Exception())
          //ListEventsDefaultStatusCode { content = events }

      //#endregion

    let EventApiService = EventApiServiceImpl() :> IEventApiService