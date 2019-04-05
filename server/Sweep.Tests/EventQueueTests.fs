namespace Sweep.Tests

open System
open System.Collections.Generic
open System.Net
open System.Net.Http
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.TestHost
open TestHelper
open FSharp.Control.Tasks.V2.ContextInsensitive
open Xunit
open System.Text
open Newtonsoft.Json
open Sweep.EventQueue
open Sweep.Model.Template
open Sweep.Model.Listener
open Sweep.Model.Event

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
      getSubject template event "foo" |> shouldEqual "foo" |> ignore
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
      |> HttpPost client "/templates"
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore

      let template = 
        "/templates" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head

      // create a listener
      {
          EventName="some_event";
          TemplateId=template.Id;
          OrganizationId="";
          Id="";
      } 
        |> encode
        |> HttpPost client "/listeners"
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore
      
      let listener = 
        HttpGet client "/listeners" 
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head

      // create an event
      { EventName = "some_event"; Params=dict ["key","val" :> obj] ; Id = ""; ReceivedOn=DateTime.Now; ProcessedOn=DateTime.Now; Error=""; OrganizationId="" }
      |> encode
      |> HttpPost client "/events"
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
      let event = { EventName = "some_event"; Params=dict ["key","val" :> obj] ; Id = ""; ReceivedOn=DateTime.Now; ProcessedOn=DateTime.Now; Error=""; OrganizationId="" }
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
      Sweep.EventQueue.handle "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" "default@co" "Default" "Default Subject!" (event, template)
    }
