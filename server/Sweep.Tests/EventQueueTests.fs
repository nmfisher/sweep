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
open Sweep.Model.TemplateRequestBody
open Sweep.Model.Listener
open Sweep.Model.Event
open Sweep.Model.EventRequestBody
open Sweep.Data.Listener
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
      let event = 
        {
          Id="some_id";
          EventName="some_event";
          ReceivedOn=DateTime.Now.Subtract(TimeSpan(7,0,0,0))
          ProcessedOn=  None;
          Params=None;
          Error=None;
          OrganizationId="some_id"
        }
      let condition = 
        {
          EventName="some_event";
          Key=None;
          Duration=TimeSpan(5,0,0,0)
        } : ListenerCondition
      Assert.True(noLongerApplies event condition)
    }

  [<Fact>]
  let ``Listener condition doesn't expire when duration has not exceeded time elapsed since original event``() =
    task {
      let event = 
        {
          Id="some_id";
          EventName="some_event";
          ReceivedOn=DateTime.Now.Subtract(TimeSpan(5,0,0,0))
          ProcessedOn=None;
          Params=None;
          Error=None;
          OrganizationId="some_id"
        } : Event
      let condition = 
        {
          EventName="some_event";
          Key=None;
          Duration=TimeSpan(7,0,0,0)
        } : ListenerCondition
      Assert.False(noLongerApplies event condition)
    }

  [<Fact>]
  let ``Listener condition is met by events matching event name with no key within duration ``() =
    task {

      let parent = 
        {
          Id="1";
          EventName="some_event";
          ReceivedOn=DateTime.Now.Subtract(TimeSpan(5,0,0,0))
          ProcessedOn=None;
          Params=None;
          Error=None;
          OrganizationId="some_id"
        }

      let events = 
        [{
          Id="1";
          EventName="some_event";
          ReceivedOn=DateTime.Now.Subtract(TimeSpan(5,0,0,0))
          ProcessedOn=None;
          Params=None;
          Error=None;
          OrganizationId="some_id"
        };
        {
          Id="2";
          EventName="some_other_event";
          ReceivedOn=DateTime.Now.Subtract(TimeSpan(5,0,0,0))
          ProcessedOn=None;
          Params=None;
          Error=None;
          OrganizationId="some_id"
        };]

      let condition = 
        {
          EventName="some_other_event";
          Key=None;
          Duration=TimeSpan(7,0,0,0)
        } : ListenerCondition
      Assert.True(isMetBy events parent condition)
    }

  [<Fact>]
  let ``Listener condition is met by events matching event name and key within duration ``() =
    task {

      let parent = 
        {
          Id="1";
          EventName="some_event";
          ReceivedOn=DateTime.Now.Subtract(TimeSpan(5,0,0,0))
          ProcessedOn=None;
          Params=Some(dict ["key1","val1" :> obj]);
          Error=None;
          OrganizationId="some_id"
        }

      let events = 
        [{
          Id="1";
          EventName="some_event";
          ReceivedOn=DateTime.Now.Subtract(TimeSpan(5,0,0,0))
          ProcessedOn=None;
          Params=None;
          Error=None;
          OrganizationId="some_id"
        };
        {
          Id="2";
          EventName="some_other_event";
          ReceivedOn=DateTime.Now.Subtract(TimeSpan(5,0,0,0))
          ProcessedOn=None;
          Params=Some(dict ["key1","val1" :> obj]);
          Error=None;
          OrganizationId="some_id"
        };]

      let condition = 
        {
          EventName="some_other_event";
          Key=Some("key1");
          Duration=TimeSpan(7,0,0,0)
        } : ListenerCondition
      Assert.True(isMetBy events parent condition)
    }
 

  [<Fact>]
  let ``sendConditional only invokes mailer if trigger is matched``() =
    task {
      let mutable mailFlag = false
      let mutable successFlag = false

      sendConditional (fun () -> true) (fun () -> true) () (fun () -> mailFlag <- true) (fun () -> successFlag <- true) (fun () -> raise (Exception()))

      Assert.True(mailFlag)
      Assert.True(successFlag)

      mailFlag <- false
      successFlag <- false

      sendConditional (fun () -> false) (fun () -> true) () (fun () -> mailFlag <- true) (fun () -> successFlag <- true) (fun () -> raise (Exception()))

      Assert.False(mailFlag)
      Assert.True(successFlag)

      mailFlag <- false
      successFlag <- false

      sendConditional (fun () -> false) (fun () -> false) () (fun () -> mailFlag <- true) (fun () -> successFlag <- true) (fun () -> raise (Exception()))

      Assert.False(mailFlag)
      Assert.False(successFlag)

    }

  [<Fact>]
  let ``Dequeue and inspect generated SQL``() =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      TestHelper.initialize() |> ignore
      
      // create a template
      {
        Sweep.Model.TemplateRequestBody.TemplateRequestBody.Content="Hello";
        SendTo=[|"foo@bar"|];
        Subject="Some subject";
        FromAddress="baz@qux";
        FromName="Baz";
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
          Trigger=None;
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
      { EventName = "some_event"; Params=None; } : EventRequestBody
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
