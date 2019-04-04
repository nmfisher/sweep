namespace Sweep

open System
open UserModel
open Microsoft.FSharp.Linq.RuntimeHelpers
open System.Linq.Expressions
open FSharp.Control.AsyncSeqExtensions
open FSharp.Control
open TemplateModel
open System.Linq.Expressions
open Microsoft.FSharp.Quotations
open FSharp.Data.Sql
open Newtonsoft.Json
open ListenerModel
open ListenerTemplateModel
open MessageModel
open System.Collections.Generic

module CompositionRoot =

  exception NotFoundException of string
  
  let serialize = Newtonsoft.Json.JsonConvert.SerializeObject

  let [<Literal>] connectionString = "server=localhost;database=sweep_development;user=root;password=MyNewPass"
  let [<Literal>] resPath = __SOURCE_DIRECTORY__ + @"/../../lib"

  type Sql = SqlDataProvider< 
              ConnectionString = connectionString,
              DatabaseVendor = Common.DatabaseProviderTypes.MYSQL,
              ResolutionPath = resPath,
              IndividualsAmount = 1000,
              UseOptionTypes = true>

  // Event        

  let deserializeEvent (prop,value) =
     match prop with
     | "Params" -> 
          if value <> null
          then JsonConvert.DeserializeObject<Dictionary<string,obj>>(value.ToString()) |> box
          else Unchecked.defaultof<obj[]> |> box
     | "Id" ->
        value.ToString() :> obj
     | _ -> 
        value
  
  let addEvent eventName eventParams organizationId =
    let ctx = Sql.GetDataContext()
    let event = ctx.SweepDevelopment.Event.Create()
    event.EventName <- eventName
    event.Params <- eventParams |> serialize |> Some
    event.OrganizationId <- organizationId
    event.ReceivedOn <- DateTime.Now
    event.Id <- Guid.NewGuid().ToString()
    ctx.SubmitUpdates()

  let getEvent eventId organizationId  = 
    let ctx = Sql.GetDataContext()
    query {
      for event in ctx.SweepDevelopment.Event do
      where (event.OrganizationId = organizationId && event.Id = eventId)
      select event
    } 
    |> Seq.map (fun x -> x.MapTo<EventModel.Event>(deserializeEvent))
    |> Seq.tryHead
      
  let listEvents organizationId =
    let ctx = Sql.GetDataContext()
    query {      
      for event in ctx.SweepDevelopment.Event do
      where (event.OrganizationId = organizationId)
      select (event)
    } |> Seq.map (fun x -> x.MapTo<EventModel.Event>(deserializeEvent))

  
  // let processEvent event = 
  //   try
  //     StubbleBuilder().Build().Render(event.C event.Params
  //   with
  //   | e ->
  //     ()

  // let dequeue = 
  //   let ctx = Sql.GetDataContext()
  //   let unprocessed = 
  //     query {      
  //       for event in ctx.SweepDevelopment.Event do
  //       join listener in ctx.SweepDevelopment.Listener on (listener.EventName = event.eventName && listener.OrganizationId = event.organizationId)
  //       where isNull event.ProcessedOn
  //       select (event)
  //     } 
  //     |> Seq.map (fun x -> x.MapTo<EventModel.Event>(deserializeEvent))
  //     |> Seq.toList

  //   unprocessed
  //   |> Seq.map processEvent 
    
  
  // Template

  let deserializeTemplate (prop,value:obj) =
   match prop with
    | "Id" ->
      value.ToString() :> obj
    | "SendTo" ->
      JsonConvert.DeserializeObject<string[]> (value.ToString()) :> obj
    | _ -> 
      value


  let addTemplate (content:string) (sendTo:string[]) (organizationId:string) (userId:string) =
    let ctx = Sql.GetDataContext()
    let template = ctx.SweepDevelopment.Template.Create()
    template.Content <- content
    template.SendTo <- JsonConvert.SerializeObject sendTo
    template.OrganizationId <- organizationId
    template.UserId <- userId
    template.Id <- Guid.NewGuid().ToString()
    ctx.SubmitUpdates()

  let deleteTemplate id orgId userId =
    let ctx = Sql.GetDataContext()
    let row = query {
      for template in ctx.SweepDevelopment.Template do
      where (template.Id = id && template.OrganizationId = orgId)
      select template
      exactlyOneOrDefault
    } 
    match (isNull row) with 
    | true ->
      raise (NotFoundException("Template not found"))      
    | _ ->
      row.Deleted <- Some((sbyte)1)
      ctx.SubmitUpdates()
               
  let getTemplate id organizationId = 
    let ctx = Sql.GetDataContext()
    let row = query {      
      for template in ctx.SweepDevelopment.Template do
      where (template.Id = id && template.OrganizationId = organizationId && (template.Deleted.IsNone || (template.Deleted.IsSome && template.Deleted.Value = sbyte(0))))
      select (template)
      exactlyOneOrDefault
    } 
    if isNull row then
      raise (NotFoundException("Not found"))
    else
      row.MapTo<Template>(deserializeTemplate)

  let updateTemplate id content sendTo organizationId = 
    let ctx = Sql.GetDataContext()
    let row = query {      
      for template in ctx.SweepDevelopment.Template do
      where (template.Id = id && template.OrganizationId = organizationId)
      select (template)
      exactlyOneOrDefault
    } 
    if isNull row then
      raise (NotFoundException("Not found"))
    else
      row.SendTo <- JsonConvert.SerializeObject sendTo
      row.Content <- content
      ctx.SubmitUpdates()

  let listTemplates organizationId =
    let ctx = Sql.GetDataContext()
    query {      
      for template in ctx.SweepDevelopment.Template do
      where (template.OrganizationId = organizationId && (template.Deleted.IsNone || (template.Deleted.IsSome && template.Deleted.Value = sbyte(0))))
      select (template)
    } |> Seq.map (fun x -> x.MapTo<Template>(deserializeTemplate))
      |> Seq.toArray

  // Users

  let saveUser id username apiKey orgId = 
    let ctx = Sql.GetDataContext()
    let user = ctx.SweepDevelopment.User.Create()
    user.Id <- id
    ctx.SubmitUpdates()

  let getUser id : User option = 
    let ctx = Sql.GetDataContext()
    query {
      for user in ctx.SweepDevelopment.User do
      where (user.Id = id)
      select user
    } 
    |> Seq.map (fun x -> x.MapTo<User>())
    |> Seq.tryHead

  // Organizations

  let saveOrganization id =
    let ctx = Sql.GetDataContext()
    let org = ctx.SweepDevelopment.Organization.Create()
    org.Id <- id
    ctx.SubmitUpdates()

  // Listeners
  let deserializeListener (prop,value) =
     match prop with
     | "Id" ->
        value.ToString() :> obj
     | _ -> 
        value

  let addListener eventName userId orgId = 
    let ctx = Sql.GetDataContext()
    let listener = ctx.SweepDevelopment.Listener.Create()
    listener.EventName <- eventName
    listener.OrganizationId <- orgId
    listener.Id <- Guid.NewGuid().ToString()
    listener.UserId <- userId
    ctx.SubmitUpdates()

  let getListener id orgId =
    let ctx = Sql.GetDataContext();
    let row = query {
      for listener in ctx.SweepDevelopment.Listener do
      where (listener.Id = id && listener.OrganizationId = orgId && (listener.Deleted.IsNone || (listener.Deleted.IsSome && listener.Deleted.Value = sbyte(0))))
      select listener
      exactlyOneOrDefault
    } 
    match isNull row with
    | true -> 
      raise (NotFoundException("Not found"))
    | false ->
      row.MapTo<ListenerModel.Listener>(deserializeListener)
      

  let deleteListener id userId orgId = 
    let ctx = Sql.GetDataContext();
    let row = query {
      for listener in ctx.SweepDevelopment.Listener do
      where (listener.Id = id && listener.UserId = userId && listener.OrganizationId = orgId)
      select listener
      exactlyOneOrDefault
    } 
    match (isNull row) with 
    | true ->
      raise (NotFoundException("Listener not found"))      
    | _ ->
      row.Deleted <- Some((sbyte)1)
      ctx.SubmitUpdates()

  let listListeners orgId = 
    let ctx = Sql.GetDataContext()
    query {
      for listener in ctx.SweepDevelopment.Listener do
      where (listener.OrganizationId = orgId && (listener.Deleted.IsNone || (listener.Deleted.IsSome && listener.Deleted.Value = sbyte(0))))
      select listener
    } |> Seq.map(fun x -> x.MapTo<Listener>(deserializeListener))
    |> Seq.toArray

  let updateListener id eventName orgId =
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
      row.EventName <- eventName
      ctx.SubmitUpdates()
      
  // Messages
  let deserializeMessage (prop,value) =
    match prop with
     | "Id" ->
        value.ToString() :> obj
     | "SentTo" ->   
        JsonConvert.DeserializeObject<string[]> (value.ToString()) :> obj
     | _ -> 
        value

  let getMessage id organizationId = 
    let ctx = Sql.GetDataContext()
    let row = query {
      for message in ctx.SweepDevelopment.Message do
      where (message.OrganizationId = organizationId && message.Id = id)
      select message
      exactlyOneOrDefault
    } 
    match isNull row with
    | true ->
      raise (NotFoundException("Not found"))
    | false ->        
      row.MapTo<Message>(deserializeMessage)
      
  let listMessages organizationId =
    let ctx = Sql.GetDataContext()
    query {      
      for message in ctx.SweepDevelopment.Message do
      where (message.OrganizationId = organizationId)
      select (message)
    } 
    |> Seq.map (fun x -> x.MapTo<Message>(deserializeMessage))
    |> Seq.toArray


  // ListenerTemplates  
  let deserializeListenerTemplate (prop,value) =
    value.ToString() :> obj

  let listListenerTemplates listenerId organizationId = 
    let ctx = Sql.GetDataContext()
    getListener listenerId organizationId |> ignore // throws NotFoundException
    query {      
      for listenerTemplate in ctx.SweepDevelopment.Listenertemplate do
      where (listenerTemplate.ListenerId = listenerId && listenerTemplate.OrganizationId = organizationId)
      select (listenerTemplate)
    } 
    |> Seq.map (fun x -> x.MapTo<ListenerTemplate>(deserializeListenerTemplate))
    |> Seq.toArray

  let createListenerTemplate listenerId templateId organizationId = 
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
  
  let deleteListenerTemplate listenerId templateId organizationId =
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