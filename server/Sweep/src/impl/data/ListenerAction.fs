namespace Sweep.Data

open Sweep.Model.Listener
open Sweep.Model.ListenerTemplate
open Sweep.Model.ListenerAction
open Sql
open Sweep.Exceptions
open System.Text.RegularExpressions
open System

module ListenerAction = 

  let deserialize (prop,value) =
    match (prop.ToString().Contains("Id")) with
    | true -> value.ToString() :> obj
    | false -> value

  let list organizationId = 
    let ctx = Sql.GetDataContext()
    query {      
      for listenerAction in ctx.SweepDevelopment.Listeneraction do
      where (listenerAction.OrganizationId = organizationId)
      select (listenerAction)
    } 
    |> Seq.map (fun x -> x.MapTo<ListenerAction>(deserialize))
    |> Seq.toArray

  let create eventId listenerId organizationId = 
    let ctx = Sql.GetDataContext()
    let listenerAction = ctx.SweepDevelopment.Listeneraction.Create()
    listenerAction.OrganizationId <- organizationId
    listenerAction.ListenerId <- listenerId
    listenerAction.EventId <- eventId
    ctx.SubmitUpdates()  

  let createFromEvent (event:Sweep.Model.Event.Event) = 
    let ctx = Sweep.Data.Sql.Sql.GetDataContext()
    query { 
      for listener in ctx.SweepDevelopment.Listener do
      where (listener.EventName = event.EventName && listener.OrganizationId = event.OrganizationId)
      select listener
    } 
    |> Seq.map (fun x -> 
      let listener = x.MapTo<Listener>(Listener.deserializeListener)
      let listenerAction = ctx.SweepDevelopment.Listeneraction.Create()
      listenerAction.Id <- Guid.NewGuid().ToString()
      listenerAction.ListenerId <- listener.Id
      listenerAction.EventId <- event.Id
      listenerAction.OrganizationId <- event.OrganizationId
      listenerAction.Error <- None
      listenerAction.Completed <- sbyte(0)
      ctx.SubmitUpdates())
    |> Seq.toList

  let listIncomplete () = 
    let ctx = Sweep.Data.Sql.Sql.GetDataContext()
    query {
      for listenerAction in ctx.SweepDevelopment.Listeneraction do
      where (listenerAction.Completed = sbyte(0))
      select listenerAction
    } |> Seq.map (fun x -> x.MapTo<Sweep.Model.ListenerAction.ListenerAction>(deserialize))     

  let markAsComplete listenerActionId error =
    let ctx = Sweep.Data.Sql.Sql.GetDataContext()
    let row = 
      query {
        for listenerAction in ctx.SweepDevelopment.Listeneraction do
        where (listenerAction.Id = listenerActionId)
        select listenerAction
        exactlyOneOrDefault
      } 
    row.Completed <- sbyte(1)
    row.Error <- error    
    ctx.SubmitUpdates()
