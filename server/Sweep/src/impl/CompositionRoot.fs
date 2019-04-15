namespace Sweep

open System
open Sweep.Model.User
open Microsoft.FSharp.Linq.RuntimeHelpers
open System.Linq.Expressions
open FSharp.Control.AsyncSeqExtensions
open FSharp.Control
open Sweep.Model.Template
open System.Linq.Expressions
open Microsoft.FSharp.Quotations
open FSharp.Data.Sql
open Sweep.Data.Sql
open Sweep.Exceptions
open Newtonsoft.Json
open Sweep.Model.Listener
open Sweep.Model.ListenerTemplate
open Sweep.Model.Message
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

  let saveUser = Sweep.Data.User.save
  let getUser = Sweep.Data.User.get
  let findUserByApiKey = Sweep.Data.User.findByApiKey

  // Organizations

  let saveOrganization id =
    let ctx = GetDataContext()
    let org = ctx.SweepDb.Organization.Create()
    org.Id <- id
    ctx.SubmitUpdates()

  // Listeners
  let addListener  = Sweep.Data.Listener.add
  let deleteListener = Sweep.Data.Listener.delete
  let listListeners = Sweep.Data.Listener.list
  let getListener = Sweep.Data.Listener.get
  let updateListener = Sweep.Data.Listener.update
  let getListenerAction = Sweep.Data.ListenerAction.get
      
  // Messages
  let getMessage = Sweep.Data.Message.get
  let listMessages = Sweep.Data.Message.list
  let listMessagesForListenerAction = Sweep.Data.Message.listForListenerAction
  
  // ListenerTemplates  
  let listListenerTemplates = Sweep.Data.ListenerTemplate.list 
  let createListenerTemplate = Sweep.Data.ListenerTemplate.create 
  let deleteListenerTemplate = Sweep.Data.ListenerTemplate.delete 
  let listTemplatesForListener = Sweep.Data.ListenerTemplate.listTemplatesForListener