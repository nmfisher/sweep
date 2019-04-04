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
  
  // Event        
  let addEvent = Sweep.Data.Event.add
  let getEvent  = Sweep.Data.Event.get
  let listEvents = Sweep.Data.Event.list
     
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