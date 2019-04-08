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
     | _ -> 
        value

  type ListenerCondition = {
    EventName:string;
    Duration:TimeSpan;
    Key: string option;
  }

  let parse condition = 
    let rgx = Regex.Match(condition, "AND (?!(WITHIN|AND|DAYS|HOURS|MINUTES|NULL))([a-zA-Z_0-9]+) WITHIN ([0-9]+) (DAYS|HOURS|MINUTES) MATCH ON ([a-zA-Z0-9]+)")

    match rgx.Success with
    | true ->
      Some({
        EventName=rgx.Captures.[0].Value;
        Duration=TimeSpan();
        Key= match rgx.Captures.[2].Value with | "NULL" -> None | _ -> Some(rgx.Captures.[2].Value) 
      })
    | false ->
      None

  let add eventName userId orgId = 
    let ctx = Sql.GetDataContext()
    let listener = ctx.SweepDevelopment.Listener.Create()
    listener.Id <- Guid.NewGuid().ToString()
    listener.EventName <- eventName
    listener.OrganizationId <- orgId
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
