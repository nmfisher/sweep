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
  let ``Render default subject``() =
    task {
      TestHelper.initialize() |> ignore
      let template = {
        Subject="";
        Content = "";
        FromAddress="foo@bar";
        FromName="Foo";
        SendTo=[|"baz@qux"|];
        UserId="";
        OrganizationId="";
        Deleted=false;
        Id="";
      }

      let event = {
         EventName="SOMEEVENT";
         Params=dict ["key1","val1" :> obj];
         Id = "";
         ReceivedOn = DateTime.Now;
         ProcessedOn = DateTime.Now;
         OrganizationId = "123";
         Error = "";
      }
      let defaults = 
        {
            FromAddress = "default@co";
            FromName = "Default"; 
            Subject ="";
        } : MailDefaults

      getSubject defaults event "foo" |> shouldEqual "foo" |> ignore
    }
  
  [<Fact>]
  let ``Send test email``() =
    task {
      let resp = new Response(statusCode=HttpStatusCode.Accepted, responseBody=(encode "OK"), responseHeaders=null)

      let client = Mock<ISendGridClient>.With(fun c -> <@ c.SendEmailAsync(any()) --> Task.FromResult(resp) @>)

      let event = { 
        EventName = "some_event"; 
        Params=dict ["key","val" :> obj] ; 
        Id = ""; 
        ReceivedOn=DateTime.Now; 
        ProcessedOn=DateTime.Now; 
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

      let defaults = {
          FromAddress = "default@co";
          FromName = "Default"; 
          Subject ="Default Subject!";
      }
      let mutable success = false
      let saver = (fun x -> success <- true)
      MailHandler.handle client defaults saver event templates 
      Assert.True(success)
      
    }