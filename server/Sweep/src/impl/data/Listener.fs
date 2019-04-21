namespace Sweep.Data

open Sweep.Model.Listener
open Sql
open System
open Sweep.Exceptions
open Newtonsoft.Json
open System.Text.RegularExpressions
open System
open System.Collections.Generic

module Listener = 

  let deserializeListener (prop,value:obj) =
     match prop with
     | "Id" -> 
        value.ToString() |> box
      | "OrganizationId" -> 
        value.ToString() |> box
     | "EventParams" -> 
        match isNull value with 
        | true ->
          null
        | _ ->
          JsonConvert.DeserializeObject<string[]>(value.ToString()) :> obj
      | "TriggerMatch" ->
        match isNull value with
        | true -> "" |> box
        | _ -> value.ToString() |> box
      | _ -> 
        value :> obj

  type ListenerCondition = {
    EventName:string;
    Duration:TimeSpan;
    Key: string option;
  }

  let parse event (number:int) period key = 

    match (String.IsNullOrWhiteSpace(event) && int(number) = 0 && String.IsNullOrWhiteSpace(period) && String.IsNullOrWhiteSpace(key)) with
    | true ->
      None
    | false ->    
      if String.IsNullOrEmpty(event) then
        raise (Exception("Condition could not be correctly parsed."))
      let timespan = (
        let num = Convert.ToInt32(number)
        match period with
          | "DAYS" ->
            TimeSpan(num,0,0,0) 
          | "HOURS" ->
            TimeSpan(0,num,0,0)
          | "MINUTES" ->
            TimeSpan(0,0,num,0)
          | _ ->
            raise (Exception("Condition could not be correctly parsed."))
      )

      if timespan.TotalDays > (float 28) then
        raise (Exception("Maximum condition duration is 28 days."))
      Some({
        EventName=event;
        Duration=timespan;
        Key= match key with | "NULL" -> None | "" -> None | _ -> Some(key) 
      })


  let add eventName eventParams triggerEvent triggerNumber triggerPeriod triggerMatch  userId orgId = 
    let ctx = GetDataContext()
    let listener = ctx.SweepDb.Listener.Create()
    listener.Id <- Guid.NewGuid().ToString()
    if not(isNull eventParams) then
      listener.EventParams <- Some(Newtonsoft.Json.JsonConvert.SerializeObject eventParams)
    listener.EventName <- eventName
    listener.OrganizationId <- orgId
    listener.TriggerEvent <- Some(triggerEvent)
    listener.TriggerNumber <- Some(triggerNumber)
    listener.TriggerPeriod <- Some(triggerPeriod)
    listener.TriggerMatch <- Some(triggerMatch)
    ctx.SubmitUpdates()
    {
      Listener.Id=listener.Id;
      EventName=eventName
      EventParams=eventParams;
      TriggerEvent=triggerEvent;
      TriggerNumber=triggerNumber;
      TriggerPeriod=triggerPeriod;
      TriggerMatch=triggerMatch;
      OrganizationId=orgId
    }

  let update listenerId eventName eventParams triggerEvent triggerNumber triggerPeriod triggerMatch userId orgId =
    let ctx = GetDataContext()
    let row = query {
      for listener in ctx.SweepDb.Listener do
      where (listener.Id = listenerId && listener.OrganizationId = orgId)
      select listener
      exactlyOneOrDefault
    }
    match isNull row with
    | true -> 
      raise (NotFoundException("Not found"))
    | false ->
      if not (isNull eventParams) then
          row.EventParams <- Some(Newtonsoft.Json.JsonConvert.SerializeObject eventParams)
      else
        row.EventParams <- Some(Newtonsoft.Json.JsonConvert.SerializeObject([||]))
      row.EventName <- eventName      
      row.TriggerEvent <- Some(triggerEvent)
      row.TriggerNumber <- Some(triggerNumber)
      row.TriggerPeriod <- Some(triggerPeriod)
      row.TriggerMatch <- Some(triggerMatch)
      ctx.SubmitUpdates()
      {
        Listener.Id=listenerId;
        EventName=eventName
        EventParams=eventParams;
        TriggerEvent=triggerEvent;
        TriggerNumber=triggerNumber;
        TriggerPeriod=triggerPeriod;
        TriggerMatch=triggerMatch;
        OrganizationId=orgId
      }

  let get id orgId =
    let ctx = GetDataContext();
    let row = query {
      for listener in ctx.SweepDb.Listener do
      where (listener.Id = id && listener.OrganizationId = orgId)
      select listener
      exactlyOneOrDefault
    } 
    match isNull row with
    | true -> 
      raise (NotFoundException("Not found"))
    | false ->
      row.MapTo<Listener>(deserializeListener)

  let delete id userId orgId = 
    let ctx = GetDataContext();
    let row = query {
      for listener in ctx.SweepDb.Listener do
      where (listener.Id = id && listener.OrganizationId = orgId)
      select listener
      exactlyOneOrDefault
    } 
    match (isNull row) with 
    | true ->
      raise (NotFoundException("Listener not found"))      
    | _ ->
      row.Delete()
      ctx.SubmitUpdates()

  let list orgId = 
    let ctx = GetDataContext()
    query {
      for listener in ctx.SweepDb.Listener do
      where (listener.OrganizationId = orgId)
      select listener
    } |> Seq.map(fun x -> x.MapTo<Listener>(deserializeListener))
    |> Seq.toArray

