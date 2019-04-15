namespace Sweep.Tests

open System
open System.Net
open System.Net.Http
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.TestHost
open FSharp.Control.Tasks.V2.ContextInsensitive
open Xunit
open System.Text
open Newtonsoft
open TestHelper
open ListenerApiHandlerTestsHelper
open Sweep.ListenerApiHandler
open Sweep.ListenerApiHandlerParams
open Sweep.Model.Listener
open Sweep.Model.ListenerRequestBody
open Newtonsoft.Json
open Sweep
open Sweep.Model.Template
open Sweep.Model.TemplateRequestBody
open Sweep.Model.Event
open Sweep.Model.ListenerTemplate

module ListenerApiHandlerTests =

  // ---------------------------------
  // Tests
  // ---------------------------------
  [<Fact>]
  let ``AddListener - Create a new Listener returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore

      // create a template
      {
          TemplateRequestBody.Content="Hello";
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
          ListenerRequestBody.Trigger="";
          EventName="some_event";
          EventParams=[||];
      }
        |> encode
        |> HttpPost client "/1.0.0/listeners" 
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore

      // create a listener with some params
      {
          ListenerRequestBody.Trigger="";
          EventName="some_event";
          EventParams=[|"key1"|]
      }
        |> encode
        |> HttpPost client "/1.0.0/listeners" 
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener>
        |> (fun x -> x.EventParams |> shouldBeLength 1 |> Seq.head |> shouldEqual "key1" |> ignore)
        |> ignore
    }

  [<Fact>]
  let ``AddListener - Create a new Listener returns 422 where Invalid input`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore

      let path = "/1.0.0/listeners"
      {
          Trigger="";
          EventName="";
          EventParams=[||];
      } 
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore

      let path = "/1.0.0/listeners"
      {
          Trigger="INVALID CONDITION";
          EventName="some_event";
          EventParams=[||];
      } 
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore
    }


  [<Fact>]
  let ``DeleteListener - Deletes a Listener returns 200 where Successfully deleted`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore

      ``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously

      let listeners = 
        "/1.0.0/listeners" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>

      listeners
      |> Seq.map (fun listener -> 
        "/1.0.0/listeners/" + listener.Id
          |> HttpDelete client
          |> isStatus (enum<HttpStatusCode>(200))
          |> readText
          |> ignore)
      |> Seq.toList        
      |> ignore

      "/1.0.0/listeners" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> shouldBeLength 0
        |> ignore

      }

  [<Fact>]
  let ``DeleteListener - Deletes a Listener returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore

      let path = "/1.0.0/listeners/{listenerId}"

      HttpDelete client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> ignore
    }

  [<Fact>]
  let ``UpdateListener - Updates a Listener returns 200 where Successfully updated`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore

      ``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously

      let listener = 
        "/1.0.0/listeners" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head

      {
        ListenerRequestBody.EventName="some_updated_event"
        EventParams=[|"updated1";"updated2"|]
        Trigger=""
      }
        |> encode
        |> HttpPut client ("/1.0.0/listeners/" + listener.Id)
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore

      "/1.0.0/listeners/" + listener.Id
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener>
        |> (fun x -> 
            x.EventName |> shouldEqual "some_updated_event" |> ignore
            x.EventParams |> Seq.head |> shouldEqual "updated1"  |> ignore
            x.EventParams |> Seq.last |> shouldEqual "updated2" |> ignore
            isNull x.Trigger |> shouldEqual true |> ignore)
        |> ignore
      }


  [<Fact>]
  let ``UpdateListener - Updates a Listener returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore

      let path = "/1.0.0/listeners/{listenerId}"
      {
        ListenerRequestBody.EventName="some_updated_event"
        EventParams=[|"updated1";"updated2"|]
        Trigger=""
      } 
      |> encode
      |> HttpPut client path
      |> isStatus (enum<HttpStatusCode>(404))
      |> ignore
    }

  [<Fact>]
  let ``ListListeners - List all Listeners returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize()

      ``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously

      let path = "/1.0.0/listeners"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head
        |> (fun x ->
            x.EventName |> shouldEqual ("some_event"))
        |> ignore

         
       // create anothoer listener
      {
          ListenerRequestBody.Trigger="AND some_other_event WITHIN 7 DAYS MATCH ON NULL";
          EventName="some_event";
          EventParams=[|"key1"|]
      }
        |> encode
        |> HttpPost client "/1.0.0/listeners" 
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener>
        |> (fun x -> x.EventParams |> shouldBeLength 1 |> Seq.head |> shouldEqual "key1" |> ignore)
        |> ignore

     // and another listener without any params
      {
          ListenerRequestBody.Trigger="AND some_other_event WITHIN 7 DAYS MATCH ON NULL";
          EventName="some_new_event";
          EventParams=[||]
      }
        |> encode
        |> HttpPost client "/1.0.0/listeners" 
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener>
        |> ignore

      let listeners = 
        HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> shouldBeLength 4
      
      listeners
        |> Seq.find (fun x -> x.EventName = "some_new_event")
        |> (fun x -> x.EventParams |> shouldBeLength 0 |> ignore)
        |> ignore
    }

  [<Fact>]
  let ``createFromEvent successfully creates ListenerAction where a Listener exists for an event name and an organization id`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize()
      let orgId = Guid.NewGuid().ToString()
      let event = 
        {
          Event.Id=Guid.NewGuid().ToString();
          EventName="foo";
          Params=null;
          OrganizationId=orgId;
          ReceivedOn=DateTime.Now
          ProcessedOn=Nullable<DateTime>();
          Error="";
        }

      Sweep.Data.Listener.add "foo" [||] null "some_user_id" orgId

      Sweep.Data.ListenerAction.createFromEvent event |> ignore

      Sweep.Data.ListenerAction.list orgId |> Seq.toArray |> shouldBeLength 1 |> ignore
    }