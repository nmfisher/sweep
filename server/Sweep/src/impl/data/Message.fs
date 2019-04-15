namespace Sweep.Data

open Sweep.Model.Message
open Sql
open Sweep.Exceptions
open Newtonsoft.Json
open System

module Message = 

  let deserializeMessage (prop,value) =
    match prop with
     | "Id" ->
        value.ToString() :> obj
      | "EventId" ->
        value.ToString() :> obj
     | "SendTo" ->   
        JsonConvert.DeserializeObject<string[]> (value.ToString()) :> obj
     | _ -> 
        value

  let get id organizationId = 
    let ctx = GetDataContext()
    let row = query {
      for message in ctx.SweepDb.Message do
      where (message.OrganizationId = organizationId && message.Id = id)
      select message
      exactlyOneOrDefault
    } 
    match isNull row with
    | true ->
      raise (NotFoundException("Not found"))
    | false ->        
      row.MapTo<Message>(deserializeMessage)
      
  let list organizationId =
    let ctx = GetDataContext()
    query {      
      for message in ctx.SweepDb.Message do
      where (message.OrganizationId = organizationId)
      select (message)
    } 
    |> Seq.map (fun x -> x.MapTo<Message>(deserializeMessage))
    |> Seq.toArray

  let save message =
    let ctx = GetDataContext()
    let row = ctx.SweepDb.Message.Create()
    row.Content <- message.Content
    row.Subject <- message.Subject
    row.FromName <- message.FromName
    row.FromAddress <- message.FromAddress
    row.SendTo <- JsonConvert.SerializeObject message.SendTo
    row.OrganizationId <- message.OrganizationId
    row.EventId <- message.EventId
    row.Id <- message.Id
    ctx.SubmitUpdates()
