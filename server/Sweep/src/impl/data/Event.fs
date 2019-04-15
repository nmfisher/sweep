namespace Sweep.Data

open System
open System.Collections.Generic
open Sql
open Sweep.Exceptions
open Newtonsoft.Json

type Event = Sweep.Model.Event.Event

module Event = 

  let deserializeEvent (prop,value) =
    match isNull value with 
    | true ->
      null
    | false ->    
     match prop with
     | "ProcessedOn" ->
        DateTime.Parse(value.ToString()) |> box
     | "Params" -> 
          JsonConvert.DeserializeObject<Dictionary<string,obj>>(value.ToString()) |> box
     | "Id" ->
        value.ToString() :> obj
     | _ -> 
        value
  
  let add eventName (eventParams:IDictionary<string,obj>) organizationId =
    let ctx = GetDataContext()
    let event = ctx.SweepDb.Event.Create()
    event.EventName <- eventName
    match isNull(eventParams) with
    | true -> 
      event.Params <- None
    | false ->
      event.Params <- Some(Newtonsoft.Json.JsonConvert.SerializeObject eventParams)
    event.OrganizationId <- organizationId
    event.ReceivedOn <- DateTime.Now
    event.Id <- Guid.NewGuid().ToString()
    ctx.SubmitUpdates()

  let get eventId organizationId  = 
    let ctx = GetDataContext()
    query {
      for event in ctx.SweepDb.Event do
      where (event.OrganizationId = organizationId && event.Id = eventId)
      select event
    } 
    |> Seq.map (fun x -> x.MapTo<Event>(deserializeEvent))
    |> Seq.tryHead
      
  let list organizationId =
    let ctx = GetDataContext()
    query {      
      for event in ctx.SweepDb.Event do
      where (event.OrganizationId = organizationId)
      select (event)
    } |> Seq.map (fun x -> x.MapTo<Event>(deserializeEvent))

  let listAllAfter eventId = 
    let ctx = GetDataContext()  
    let baseEvent = 
      query {
        for event in ctx.SweepDb.Event do 
        where (event.Id = eventId)
        select event
      } |> Seq.map (fun x -> x.MapTo<Event>(deserializeEvent))
        |> Seq.head
    query {      
      for event in ctx.SweepDb.Event do
      where (baseEvent.ReceivedOn <= event.ReceivedOn)
      select event
    } 
    |> Seq.map (fun x-> x.MapTo<Event>(deserializeEvent))  

  let listAllUnprocessed () = 
    let ctx = GetDataContext()
    query {      
      for event in ctx.SweepDb.Event do
      where (event.ProcessedOn.IsNone)
      select event
    } 
    |> Seq.map (fun x-> x.MapTo<Event>(deserializeEvent))  

  let markAsProcessed (event:Event) =
    let ctx = GetDataContext()
    let row = 
        query {
          for evt in ctx.SweepDb.Event do
          where (evt.Id = event.Id)                    
          select evt
          exactlyOneOrDefault
        }    
    row.ProcessedOn <- Some(DateTime.Now)
    ctx.SubmitUpdates()


  
  