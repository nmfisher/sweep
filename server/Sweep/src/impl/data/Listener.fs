namespace Sweep.Data

open Sweep.Model.Listener
open Sql
open System
open Sweep.Exceptions
open Newtonsoft.Json

module Listener = 
  let deserializeListener (prop,value) =
     match prop with
     | "Id" ->
        value.ToString() :> obj
     | _ -> 
        value

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
