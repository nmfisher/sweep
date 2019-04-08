namespace Sweep

open Stubble.Core.Builders
open SendGrid
open SendGrid.Helpers.Mail
open System.Net
open Sweep.Model.Template
open Sweep.Model.Event
open Sweep.Data.Message
open System

module MailHandler = 
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
  
  let send (client:ISendGridClient) (message:Sweep.Model.Message.Message)  =
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

  let toMessage mailDefaults (event:Event) (template:Template) =
      {
          Id = Guid.NewGuid().ToString();
          FromAddress = (<=>) template.FromAddress  mailDefaults.FromAddress;
          FromName = (<=>) template.FromName  mailDefaults.FromName;
          Subject = (<=>) template.Subject  mailDefaults.Subject;
          Content = getHtmlContent template event;
          SendTo = template.SendTo;
          OrganizationId = template.OrganizationId
      } : Sweep.Model.Message.Message
    
    
  let handle client mailDefaults saver event templates = 
    let messages = templates |> Seq.map (toMessage mailDefaults event)
    
    for message in messages do
      let resp = send client message 
      match resp.StatusCode with 
      | HttpStatusCode.Accepted ->
          saver message
      | _ ->
        let error = 
          resp.Body.ReadAsStringAsync()
            |> Async.AwaitTask
            |> Async.RunSynchronously
        raise (Exception(error))