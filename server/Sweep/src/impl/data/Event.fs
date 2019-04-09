namespace Sweep.Data

open System
open System.Collections.Generic
open Sql
open Sweep.Exceptions
open Newtonsoft.Json

type Event = Sweep.Model.Event.Event

module Event = 

  let deserializeEvent (prop,value) =
     match prop with
     | "ProcessedOn" ->
      if (isNull value) then
        None |> box
      else
        Some(DateTime.Parse(value.ToString())) |> box
     | "Params" -> 
        if isNull value then
          None |> box
        else 
          let optional = JsonConvert.DeserializeObject<Dictionary<string,obj> option>(value.ToString())
          match optional with
            | Some p ->
              Some(p :> IDictionary<string,obj>) |> box
            | None ->
              None |> box
     | "Id" ->
        value.ToString() :> obj
     | _ -> 
        value
  
  let add eventName (eventParams:IDictionary<string,obj> option) organizationId =
    let ctx = Sql.GetDataContext()
    let event = ctx.SweepDevelopment.Event.Create()
    event.EventName <- eventName
    match eventParams with
    | Some d ->
        event.Params <- Some(Newtonsoft.Json.JsonConvert.SerializeObject eventParams)
    | None ->
        event.Params <- None
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

  let listAllAfter eventId = 
    let ctx = Sql.GetDataContext()  
    let baseEvent = 
      query {
        for event in ctx.SweepDevelopment.Event do 
        where (event.Id = eventId)
        select event
      } |> Seq.map (fun x -> x.MapTo<Event>(deserializeEvent))
        |> Seq.head
    query {      
      for event in ctx.SweepDevelopment.Event do
      where (baseEvent.ReceivedOn <= event.ReceivedOn)
      select event
    } 
    |> Seq.map (fun x-> x.MapTo<Event>(deserializeEvent))  

  let listAllUnprocessed () = 
    let ctx = Sql.GetDataContext()
    query {      
      for event in ctx.SweepDevelopment.Event do
      where (event.ProcessedOn.IsNone)
      select event
    } 
    |> Seq.map (fun x-> x.MapTo<Event>(deserializeEvent))  

  let markAsProcessed (event:Event) =
    let ctx = Sql.GetDataContext()
    let row = 
        query {
          for evt in ctx.SweepDevelopment.Event do
          where (evt.Id = event.Id)                    
          select evt
          exactlyOneOrDefault
        }    
    row.ProcessedOn <- Some(DateTime.Now)
    ctx.SubmitUpdates()


  
  