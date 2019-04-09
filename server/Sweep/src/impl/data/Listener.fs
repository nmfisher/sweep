namespace Sweep.Data

open Sweep.Model.Listener
open Sql
open System
open Sweep.Exceptions
open Newtonsoft.Json
open System.Text.RegularExpressions
open System

module Listener = 

  let deserializeListener (prop,value) =
     match prop with
     | "Id" ->
        value.ToString() :> obj
     | "Trigger" ->
        if isNull value then
          None |> box
        else 
          Some(value.ToString()) |> box
     | _ -> 
        value

  type ListenerCondition = {
    EventName:string;
    Duration:TimeSpan;
    Key: string option;
  }

  let parse trigger = 
    match trigger with 
    | None ->
      None
    | Some triggerString ->        
      let rgx = Regex.Match(triggerString, "AND (?!(WITHIN|AND|DAYS|HOURS|MINUTES|MATCH|ON|NULL))([a-zA-Z_0-9]+) WITHIN ([0-9]+) (DAYS|HOURS|MINUTES) MATCH ON (?!(WITHIN|AND|DAYS|HOURS|MINUTES|MATCH|ON))([a-zA-Z_0-9]+)")
      match rgx.Success with
      | true ->
        let num = Convert.ToInt32(rgx.Groups.[3].Value)
        let timespan = (
          match rgx.Groups.[4].Value with
            | "DAYS" ->
              TimeSpan(num,0,0,0)
            | "HOURS" ->
              TimeSpan(0,num,0,0)
            | "MINUTES" ->
              TimeSpan(0,0,num,0)
            | _ ->
              raise (Exception("Condition could not be correctly parsed.")))
        if timespan.TotalDays > (float 28) then
          raise (Exception("Maximum condition duration is 28 days."))
        Some({
          EventName=rgx.Groups.[2].Value;
          Duration=timespan;
          Key= match rgx.Groups.[6].Value with | "NULL" -> None | _ -> Some(rgx.Groups.[6].Value) 
        })
      | false ->
        raise (Exception("Condition could not be correctly parsed."))


  let add eventName trigger userId orgId = 
    let ctx = Sql.GetDataContext()
    let listener = ctx.SweepDevelopment.Listener.Create()
    listener.Id <- Guid.NewGuid().ToString()
    listener.EventName <- eventName
    listener.OrganizationId <- orgId
    listener.Trigger <- trigger
    ctx.SubmitUpdates()

  let get id orgId =
    let ctx = Sql.GetDataContext();
    let row = query {
      for listener in ctx.SweepDevelopment.Listener do
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
    let ctx = Sql.GetDataContext();
    let row = query {
      for listener in ctx.SweepDevelopment.Listener do
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
    let ctx = Sql.GetDataContext()
    query {
      for listener in ctx.SweepDevelopment.Listener do
      where (listener.OrganizationId = orgId)
      select listener
    } |> Seq.map(fun x -> x.MapTo<Listener>(deserializeListener))
    |> Seq.toArray
