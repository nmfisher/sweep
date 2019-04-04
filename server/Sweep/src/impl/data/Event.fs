namespace Sweep.Data

open System
open System.Collections.Generic
open Sql
open Sweep.Exceptions
open Newtonsoft.Json

type Event = Sweep.EventModel.Event

module Event = 

  let deserializeEvent (prop,value) =
     match prop with
     | "Params" -> 
          if value <> null
          then JsonConvert.DeserializeObject<Dictionary<string,obj>>(value.ToString()) |> box
          else Unchecked.defaultof<obj[]> |> box
     | "Id" ->
        value.ToString() :> obj
     | _ -> 
        value
  
  let add eventName eventParams organizationId =
    let ctx = Sql.GetDataContext()
    let event = ctx.SweepDevelopment.Event.Create()
    event.EventName <- eventName
    event.Params <- eventParams |> Newtonsoft.Json.JsonConvert.SerializeObject |> Some
    event.OrganizationId <- organizationId
    event.ReceivedOn <- DateTime.Now
    event.Id <- Guid.NewGuid().ToString()
    ctx.SubmitUpdates()

  let get eventId organizationId  = 
    let ctx = Sql.GetDataContext()
    query {
      for event in ctx.SweepDevelopment.Event do
      where (event.OrganizationId = organizationId && event.Id = eventId)
      select event
    } 
    |> Seq.map (fun x -> x.MapTo<Event>(deserializeEvent))
    |> Seq.tryHead
      
  let list organizationId =
    let ctx = Sql.GetDataContext()
    query {      
      for event in ctx.SweepDevelopment.Event do
      where (event.OrganizationId = organizationId)
      select (event)
    } |> Seq.map (fun x -> x.MapTo<Event>(deserializeEvent))
  