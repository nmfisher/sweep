namespace Sweep.Data

open Sweep.Model.Listener
open Sweep.Model.ListenerTemplate
open Sweep.Model.ListenerAction
open Sql
open Sweep.Exceptions
open System.Text.RegularExpressions
open System

module ListenerAction = 

  let deserializeListenerAction (prop,value) =
    if (prop.ToString().Contains("Id")) then
      value.ToString() :> obj
    else if prop.Equals("completed") then
      value :> obj
    else
      value    

  let get id organizationId = 
    let ctx = GetDataContext()
    let row = 
      query {      
        for listenerAction in ctx.SweepDb.Listeneraction do
        where (listenerAction.Id = id && listenerAction.OrganizationId = organizationId)
        select (listenerAction)
      } 
      |> Seq.map (fun x -> x.MapTo<ListenerAction>(deserializeListenerAction))
      |> Seq.tryHead
    match row with 
    | None ->
      raise (NotFoundException("Not Found"))
    | Some listenerAction -> 
      listenerAction    
    
    

  let list organizationId = 
    let ctx = GetDataContext()
    query {      
      for listenerAction in ctx.SweepDb.Listeneraction do
      where (listenerAction.OrganizationId = organizationId)
      select (listenerAction)
    } 
    |> Seq.map (fun x -> x.MapTo<ListenerAction>(deserializeListenerAction))
    |> Seq.toArray

  let create eventId listenerId organizationId = 
    let ctx = GetDataContext()
    let listenerAction = ctx.SweepDb.Listeneraction.Create()
    listenerAction.OrganizationId <- organizationId
    listenerAction.ListenerId <- listenerId
    listenerAction.EventId <- eventId
    ctx.SubmitUpdates()  

  let createFromEvent (event:Sweep.Model.Event.Event) = 
    let ctx = GetDataContext()
    query { 
      for listener in ctx.SweepDb.Listener do
      where (listener.EventName = event.EventName && listener.OrganizationId = event.OrganizationId)
      select listener
    } 
    |> Seq.map (fun x -> 
      let listener = x.MapTo<Listener>(Listener.deserializeListener)
      let listenerAction = ctx.SweepDb.Listeneraction.Create()
      listenerAction.Id <- Guid.NewGuid().ToString()
      listenerAction.ListenerId <- listener.Id
      listenerAction.EventId <- event.Id
      listenerAction.OrganizationId <- event.OrganizationId
      listenerAction.Error <- None
      listenerAction.Completed <- sbyte(0)
      ctx.SubmitUpdates())
    |> Seq.toList

  let listIncomplete () = 
    let ctx = GetDataContext()
    query {
      for listenerAction in ctx.SweepDb.Listeneraction do
      where (listenerAction.Completed = sbyte(0))
      select listenerAction
    } |> Seq.map (fun x -> x.MapTo<Sweep.Model.ListenerAction.ListenerAction>(deserializeListenerAction))     

  let markAsComplete listenerActionId error =
    let ctx = GetDataContext()
    let row = 
      query {
        for listenerAction in ctx.SweepDb.Listeneraction do
        where (listenerAction.Id = listenerActionId)
        select listenerAction
        exactlyOneOrDefault
      } 
    row.Completed <- sbyte(1)
    row.Error <- error    
    ctx.SubmitUpdates()
