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


module EventQueue = 
  
  let send apiKey from subject recipients getPlainTextContent getHtmlContent content  =
    let client = SendGridClient(apiKey)
    MailHelper.CreateSingleEmailToMultipleRecipients(from, recipients, subject, getPlainTextContent(), getHtmlContent(), true)
    |> client.SendEmailAsync
    |> Async.AwaitTask

  let getFrom (template:Template) defaultFromName defaultFromAddress =
    let fromAddress = (fun () -> 
        match template.FromAddress with 
          | null ->
            defaultFromAddress
          | _ ->
            template.FromAddress)

    let fromName = (fun () ->
        match template.FromName with 
          | null ->
            defaultFromName
          | _ ->
            template.FromName)
    EmailAddress(fromAddress(), fromName())

  let getRecipients (template:Template) =
    template.SendTo
    |> Seq.map (fun x -> EmailAddress(x))
    |> ResizeArray<EmailAddress>

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

  let handle apiKey defaultFromAddress defaultFromName defaultSubject (event, template) =
    let from = getFrom template defaultFromAddress defaultFromAddress
    let subject = getSubject template event defaultSubject
    let recipients = getRecipients template

    send apiKey from subject recipients

  type EventListener = {
    eventId: string;
    listenerId: string
  }

  FSharp.Data.Sql.Common.QueryEvents.SqlQueryEvent |> Microsoft.FSharp.Control.Event.add (printfn "Executing SQL: %O") |> ignore
    
  let dequeue () = 
    let ctx = Sweep.Data.Sql.Sql.GetDataContext()

    query {      
      for template in ctx.SweepDevelopment.Template do
      join listenertemplate in ctx.SweepDevelopment.Listenertemplate on (template.Id = listenertemplate.TemplateId)
      join listener in ctx.SweepDevelopment.Listener on (listenertemplate.ListenerId = listener.Id)
      join event in ctx.SweepDevelopment.Event on (listener.EventName = event.EventName)
      where (template.OrganizationId = listener.OrganizationId && (template.Deleted.IsNone || template.Deleted.Value = sbyte(0)))
      select (event, template)
    } 
    |> Seq.map  (fun (x,y) -> x.MapTo<Event>(Event.deserializeEvent), x.MapTo<Template>(Template.deserializeTemplate))
    |> Seq.toList
  
  let initialize delay apiKey defaultFromAddress defaultFromName defaultSubject = 
    let timer = new Timers.Timer(60000.)
    printfn "%A" DateTime.Now
    timer.Start()
    printfn "%A" "A-OK"
    while true do
      Async.AwaitEvent (timer.Elapsed)
      dequeue() |> Seq.map (handle apiKey defaultFromAddress defaultFromName defaultSubject)
      printfn "%A" DateTime.Now