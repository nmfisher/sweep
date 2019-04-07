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
open Sweep.Model.Listener
open Sweep.Model.Event
open SendGrid
open System.Text
open Microsoft.AspNetCore.Http.Headers

module EventQueueTests =

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
       let defaults = {
          FromAddress = "default@co";
          FromName = "Default"; 
          Subject ="";
      }

      getSubject defaults event "foo" |> shouldEqual "foo" |> ignore
    }
  
  [<Fact>]
  let ``Dequeue and inspect generated SQL``() =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      TestHelper.initialize() |> ignore
      
      // create a template
      {
          Content="Hello";
          SendTo=[|"foo@bar"|];
          Subject="Some subject";
          FromAddress="baz@qux";
          FromName="Baz";
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> encode
      |> HttpPost client "/1.0.0/templates"
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore

      let template = 
        "/1.0.0/templates" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head

      // create a listener
      {
          EventName="some_event";
          OrganizationId="";
          Id="";
      } 
        |> encode
        |> HttpPost client "/1.0.0/listeners"
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore
      
      let listener = 
        HttpGet client "/1.0.0/listeners" 
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head

      // create an event
      { EventName = "some_event"; Params=dict ["key","val" :> obj] ; Id = ""; ReceivedOn=DateTime.Now; ProcessedOn=DateTime.Now; Error=""; OrganizationId="" }
      |> encode
      |> HttpPost client "/1.0.0/events"
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore 

      let dequeued = Sweep.EventQueue.dequeue() |> Seq.toArray
      dequeued 
      |> shouldBeLength 1
      |> Seq.head 
      |> (fun (x, y) -> 
        x.EventName |> shouldEqual "some_event" |> ignore
        y.Content |> shouldEqual "Hello" |> ignore)
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

      let template =   {
        Content="Hello";
        SendTo=[|"nick.fisher@avinium.com"|];
        Subject="Some subject";
        FromAddress="baz@qux";
        FromName="Baz";
        Id="";
        OrganizationId="";
        UserId="";
        Deleted=false;
      } 

      let defaults = {
          FromAddress = "default@co";
          FromName = "Default"; 
          Subject ="Default Subject!";
      }

      let mutable success = false
      let onSuccess evt msg = 
        success <- true
      
      let onError evt err = 
        ()

      Sweep.EventQueue.handle client defaults onSuccess onError (event, template)

      Assert.True(success)
      
    }
