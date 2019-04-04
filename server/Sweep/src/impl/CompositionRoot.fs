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
open Sweep.Data.Sql
open Sweep.Exceptions
open Newtonsoft.Json
open ListenerModel
open ListenerTemplateModel
open MessageModel
open System.Collections.Generic

module CompositionRoot =
  
  let serialize = Newtonsoft.Json.JsonConvert.SerializeObject

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
     
  // Template

  let addTemplate = Sweep.Data.Template.add
  let deleteTemplate = Sweep.Data.Template.delete
  let getTemplate = Sweep.Data.Template.get
  let updateTemplate = Sweep.Data.Template.update
  let listTemplates = Sweep.Data.Template.list

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
  let addListener  = Sweep.Data.Listener.add
  let getListener = Sweep.Data.Listener.get
  let deleteListener = Sweep.Data.Listener.delete
  let listListeners = Sweep.Data.Listener.list
  let updateListener = Sweep.Data.Listener.update
      
  // Messages
  let getMessage = Sweep.Data.Message.get
  let listMessages = Sweep.Data.Message.list

  // ListenerTemplates  
  let listListenerTemplates = Sweep.Data.ListenerTemplate.list getListener
  let createListenerTemplate = Sweep.Data.ListenerTemplate.create getListener 
  let deleteListenerTemplate = Sweep.Data.ListenerTemplate.delete  