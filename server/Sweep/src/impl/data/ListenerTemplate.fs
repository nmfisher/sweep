namespace Sweep.Data

open Sweep.ListenerTemplateModel
open Sql
open Sweep.Exceptions

module ListenerTemplate = 

  let deserializeListenerTemplate (prop,value) =
    value.ToString() :> obj

  let list getListener listenerId organizationId = 
    let ctx = Sql.GetDataContext()
    getListener listenerId organizationId |> ignore // throws NotFoundException
    query {      
      for listenerTemplate in ctx.SweepDevelopment.Listenertemplate do
      where (listenerTemplate.ListenerId = listenerId && listenerTemplate.OrganizationId = organizationId)
      select (listenerTemplate)
    } 
    |> Seq.map (fun x -> x.MapTo<ListenerTemplate>(deserializeListenerTemplate))
    |> Seq.toArray

  let create getListener listenerId templateId organizationId = 
    let ctx = Sql.GetDataContext()
    let existing = query {      
      for listenerTemplate in ctx.SweepDevelopment.Listenertemplate do
      where (listenerTemplate.ListenerId = listenerId && listenerTemplate.TemplateId = templateId && listenerTemplate.OrganizationId = organizationId)
      select (listenerTemplate)
      exactlyOneOrDefault
    } 
    match isNull existing with
    | false ->
      () // noop
    | true ->
      getListener listenerId organizationId |> ignore // throws NotFoundException
      let listenerTemplate = ctx.SweepDevelopment.Listenertemplate.Create()
      listenerTemplate.OrganizationId <- organizationId
      listenerTemplate.ListenerId <- listenerId
      listenerTemplate.TemplateId <- templateId
      ctx.SubmitUpdates()
  
  let delete listenerId templateId organizationId =
    let ctx = Sql.GetDataContext()
    let listenerTemplate = query {      
      for listenerTemplate in ctx.SweepDevelopment.Listenertemplate do
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
