namespace Sweep

open SendGrid
open SendGrid.Helpers.Mail
open System.Net
open Sweep.Model.Template
open Sweep.Model.Event
open Sweep.Data.Message
open Sweep.TemplateRenderer
open System

module MailHandler = 

  let send (client:ISendGridClient) (message:Sweep.Model.Message.Message)  =
    let from = EmailAddress(message.FromAddress, message.FromName)
    let recipients = message.SendTo |> Seq.map EmailAddress |> ResizeArray<EmailAddress>
    let message = MailHelper.CreateSingleEmailToMultipleRecipients(from, recipients, message.Subject, message.Content, message.Content, true)
    message
    |> client.SendEmailAsync
    |> Async.AwaitTask
    |> Async.RunSynchronously

    
  let handle client defaults saver event templates  = 
    let messages = templates |> Seq.map (renderTemplate defaults event.Params event.Id)
    
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