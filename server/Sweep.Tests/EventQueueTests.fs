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
  let ``Listener condition expires when duration exceeds time elapsed since original event``() =
    task {
      let event = {
        Id="some_id";
        EventName="some_event";
        ReceivedOn=DateTime.Now.Subtract(TimeSpan(7,0,0,0))
        ProcessedOn=  None;
        Params= dict [||]
      }
      let condition = {
        EventName="some_event";
        Key=None;
        Duration=TimeSpan(5,0,0,0)
      } : ListenerCondition
      Assert.True(noLongerApplies event condition)
    }

  [<Fact>]
  let ``Listener condition doesn't expire when duration has not exceeded time elapsed since original event``() =
    task {
      let event = {
        Id="some_id";
        EventName="some_event";
        ReceivedOn=DateTime.Now.Subtract(TimeSpan(5,0,0,0))
        ProcessedOn=None;
        Params=dict [||]
      } : Event
      let condition = {
        EventName="some_event";
        Key=None;
        Duration=TimeSpan(7,0,0,0)
      } : ListenerCondition
      Assert.False(noLongerApplies event condition)
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
          Condition="";
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

      let dequeued = Sweep.Data.Event.listAllUnprocessed() |> Seq.toArray
      dequeued 
      |> shouldBeLength 1
      |> Seq.head 
      |> (fun x -> x.EventName |> shouldEqual "some_event" |> ignore)
    }
