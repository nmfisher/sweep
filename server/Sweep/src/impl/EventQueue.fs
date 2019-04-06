namespace Sweep

open Microsoft.Extensions.Configuration
open System.Collections.Generic
open Sweep.Data
open Stubble.Core.Builders
open SendGrid
open SendGrid.Helpers.Mail
open Sweep.Model.Template
open Sweep.Model.Listener
open System
open FSharp.Data.Sql
open System.Net
open Sweep.Model.Message


module EventQueue = 

  type MailDefaults = {
    FromAddress:string;
    FromName:string;
    Subject:string;
  }    

  let (<=>) a b = 
    match String.IsNullOrWhiteSpace(a) with
    | true ->
      b 
    | false ->
      a

  FSharp.Data.Sql.Common.QueryEvents.SqlQueryEvent |> Microsoft.FSharp.Control.Event.add (printfn "Executing SQL: %O") |> ignore
  
  let send (client:ISendGridClient) (message:Message)  =
    let from = EmailAddress(message.FromAddress, message.FromName)
    let recipients = message.SendTo |> Seq.map EmailAddress |> ResizeArray<EmailAddress>
    let message = MailHelper.CreateSingleEmailToMultipleRecipients(from, recipients, message.Subject, message.Content, message.Content, true)
    message
    |> client.SendEmailAsync
    |> Async.AwaitTask
    |> Async.RunSynchronously

  let getHtmlContent (template:Template) (event:Event) =
    StubbleBuilder().Build().Render(template.Content, event.Params)

  let getPlainTextContent (template:Template) (event:Event) =
    StubbleBuilder().Build().Render(template.Content, event.Params)

  let getSubject template (event:Event) defaultSubject  = 
    match String.IsNullOrWhiteSpace(template.Subject) with
    | true -> 
      defaultSubject
    | false ->
      StubbleBuilder().Build().Render(template.Subject, event.Params)    

  let handle client mailDefaults onSuccess onError (event:Event, template:Template) =
    let message = 
      {
          Id = Guid.NewGuid().ToString();
          FromAddress = (<=>) template.FromAddress  mailDefaults.FromAddress;
          FromName = (<=>) template.FromName  mailDefaults.FromName;
          Subject = (<=>) template.Subject  mailDefaults.Subject;
          Content = getHtmlContent template event;
          SendTo = template.SendTo;
          OrganizationId = template.OrganizationId
      } : Message
    
    let resp = send client message 
    
    match resp.StatusCode with 
    | HttpStatusCode.Accepted ->
        onSuccess event message
    | _ ->
        resp.Body.ReadAsStringAsync()
          |> Async.AwaitTask
          |> Async.RunSynchronously
          |> onError event 

  let update (event:Event) error =
    let ctx = Sweep.Data.Sql.Sql.GetDataContext()
    let row = 
      query {      
        for row in ctx.SweepDevelopment.Event do
        where (row.Id = event.Id)
        select (row)
        exactlyOneOrDefault
      }     
    row.ProcessedOn <- Some(DateTime.Now)
    row.Error <- Some(error)
    ctx.SubmitUpdates()    
    
  let dequeue () = 
    let ctx = Sweep.Data.Sql.Sql.GetDataContext()
    query {      
      for template in ctx.SweepDevelopment.Template do
      join listener in ctx.SweepDevelopment.Listener on (template.Id = listener.TemplateId)
      join event in ctx.SweepDevelopment.Event on (listener.EventName = event.EventName)
      where (template.OrganizationId = listener.OrganizationId)
      select (event, template)
    } 
    |> Seq.map (fun (x,y) -> x.MapTo<Event>(Event.deserializeEvent), y.MapTo<Template>(Template.deserializeTemplate))
    |> Seq.toList
  
  let initialize delay apiKey defaultFromAddress defaultFromName defaultSubject = 
    let timer = new Timers.Timer(60000.)
    printfn "%A" DateTime.Now
    timer.Start()
    printfn "%A" "A-OK"
    let client = SendGridClient(apiKey)
    let mailDefaults = {
      FromAddress=defaultFromAddress;
      FromName=defaultFromName;
      Subject=defaultSubject;
    }
    
    let onSuccess (event:Event) (message:Message) = 
      update event null
      Message.save message

    let onError event error =
      update event error
    
    while true do
      Async.AwaitEvent (timer.Elapsed) |> ignore
      dequeue() |> Seq.map (handle client mailDefaults onSuccess onError) 
      printfn "%A" DateTime.Now