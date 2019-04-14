namespace Sweep.Tests

open System
open System.Collections.Generic
open System.Net
open System.Net.Http
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.TestHost
open System.Threading.Tasks
open TestHelper
open FSharp.Control.Tasks.V2.ContextInsensitive
open Xunit
open Giraffe
open Foq
open Newtonsoft.Json
open Sweep.EventQueue
open Sweep.Model.Template
open Sweep.Model.Event
open Sweep.TemplateRenderer
open Sweep.MailHandler
open SendGrid
open System.Text
open Microsoft.AspNetCore.Http.Headers
open Sweep

module MailHandlerTests =

  // ---------------------------------
  // Tests
  // ---------------------------------
  [<Fact>]
  let ``Send test email``() =
    task {
      let resp = new Response(statusCode=HttpStatusCode.Accepted, responseBody=(encode "OK"), responseHeaders=null)

      let client = Mock<ISendGridClient>.With(fun c -> <@ c.SendEmailAsync(any()) --> Task.FromResult(resp) @>)

      let event = { 
        EventName = "some_event"; 
        Params=null;
        Id = ""; 
        ReceivedOn=DateTime.Now; 
        ProcessedOn=Nullable<DateTime>();
        Error=""; 
        OrganizationId=""
      }

      let templates = [{
        Content="Hello";
        SendTo=[|"nick.fisher@avinium.com"|];
        Subject="Some subject";
        FromAddress="baz@qux";
        FromName="Baz";
        Id="";
        OrganizationId="";
        UserId="";
        Deleted=false;
      }]

      let defaults = 
        {
            MailDefaults.FromAddress = "default@co";
            FromName = "Default"; 
            Subject ="Default Subject!";
        }
      let mutable success = false
      let saver = (fun x -> success <- true)
      MailHandler.handle client defaults saver templates event 
      Assert.True(success)
      
    }
