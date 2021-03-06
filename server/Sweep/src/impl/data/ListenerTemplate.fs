namespace Sweep.Data

open Sweep.Model.ListenerTemplate
open Sweep.Model.Template
open Sql
open Sweep.Exceptions
open Sweep.Model
open System

module ListenerTemplate = 

  let deserializeListenerTemplate (prop,value) =
    value.ToString() :> obj

  let list listenerId organizationId = 
    let ctx = GetDataContext()
    query {      
      for listenerTemplate in ctx.SweepDb.Listenertemplate do
      where (listenerTemplate.ListenerId = listenerId && listenerTemplate.OrganizationId = organizationId)
      select (listenerTemplate)
    } 
    |> Seq.map (fun x -> x.MapTo<ListenerTemplate>(deserializeListenerTemplate))
    |> Seq.toArray

  let create listenerId templateId organizationId = 
    let ctx = GetDataContext()
    let existing = query {      
      for listenerTemplate in ctx.SweepDb.Listenertemplate do
      where (listenerTemplate.ListenerId = listenerId && listenerTemplate.TemplateId = templateId && listenerTemplate.OrganizationId = organizationId)
      select (listenerTemplate)
      exactlyOneOrDefault
    } 
    match isNull existing with
    | false ->
      () // noop
    | true ->
      let listenerTemplate = ctx.SweepDb.Listenertemplate.Create()
      listenerTemplate.OrganizationId <- organizationId
      listenerTemplate.ListenerId <- listenerId
      listenerTemplate.TemplateId <- templateId
      ctx.SubmitUpdates()
  
  let delete listenerId templateId organizationId =
    let ctx = GetDataContext()
    let listenerTemplate = query {      
      for listenerTemplate in ctx.SweepDb.Listenertemplate do
      where (listenerTemplate.ListenerId = listenerId && listenerTemplate.OrganizationId = organizationId)
      select (listenerTemplate)
      exactlyOneOrDefault
    } 
    match isNull listenerTemplate with
    | true -> 
      raise (NotFoundException("Not found"))
    | false ->    
      listenerTemplate.Delete()
      ctx.SubmitUpdates()

  let get listenerId organizationId = 
    let ctx = GetDataContext()
    let listenerTemplate = query {      
      for listenerTemplate in ctx.SweepDb.Listenertemplate do
      where (listenerTemplate.ListenerId = listenerId && listenerTemplate.OrganizationId = organizationId)
      select (listenerTemplate)
      exactlyOneOrDefault
    } 
    match isNull listenerTemplate with
    | true -> 
      raise (NotFoundException("Not found"))
    | false ->    
      listenerTemplate.Delete()
      ctx.SubmitUpdates()

  let listTemplatesForListener listenerId orgId = 
    let ctx = GetDataContext() 
    query {
      for listenerTemplate in ctx.SweepDb.Listenertemplate do
      join template in ctx.SweepDb.Template on (listenerTemplate.TemplateId = template.Id)
      where (listenerTemplate.ListenerId = listenerId && listenerTemplate.OrganizationId = orgId)
      select template
    } |> Seq.map(fun x -> x.MapTo<Template>(Template.deserializeTemplate))