namespace Sweep.Tests

open System
open System.Collections.Generic
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
open Newtonsoft.Json
open TestHelper
open EventApiHandlerTestsHelper
open Sweep.EventApiHandler
open Sweep.EventApiHandlerParams
open Sweep.Model.Event
open Sweep.Model.EventRequestBody
open Microsoft.AspNetCore.Hosting
open FSharp.Data.Sql
open FSharp.Data.Sql.Providers

module EventApiHandlerTests =

  // ---------------------------------
  // Tests
  // ---------------------------------

  [<Fact>]
  let ``AddEvent - Raise an event returns 200 where An event has been successfully created.`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore

      let path = "/1.0.0/events"

      {
          EventRequestBody.EventName="some_event";
          Params=None
      } 
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> readText
      |> shouldEqual "OK"
      |> ignore
  }

  [<Fact>]
  let ``AddEvent - Raise an event returns 422 where Invalid input`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      let path = "/1.0.0/events"

      { 
        EventRequestBody.EventName="";
        Params=None
      }
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore
    }

  [<Fact>]
  let ``GetEventById - Find raised event by ID returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here
      { 
        EventRequestBody.EventName="some_event";
        Params=Some(dict ["key1","value1" :>obj])
      } 
      |> encode
      |> HttpPost client "/1.0.0/events"
      |> ignore
      
      let eventId = 
        HttpGet client "/1.0.0/events"
          |> isStatus (enum<HttpStatusCode>(200))
          |> readText
          |> JsonConvert.DeserializeObject<Event[]>
          |> Seq.head
          |> (fun x-> x.Id)

      let path = "/1.0.0/events/" + eventId

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Event>
        |> (fun x -> 
            x.EventName |> shouldEqual "some_event" |> ignore
            x.Params.Value.Keys.Count |> shouldEqual 1 |> ignore
            x.Params.Value.["key1"] |> shouldEqual "value1" |> ignore)
      }

  [<Fact>]
  let ``GetEventById - Find raised event by ID returns 404 where Order not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/1.0.0/events/111"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> ignore
      }

  [<Fact>]
  let ``ListEvents - List all received events returns 200 where successful operation`` () =
    task {
      
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here
      initialize() |> ignore

      let path = "/1.0.0/events"

      HttpGet client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> readText
      |> JsonConvert.DeserializeObject<Event[]>
      |> shouldBeLength 0
      |> ignore

      // add an event for our organization so we can ensure it's returned properly
      { 
        EventRequestBody.EventName="some_event";
        Params=Some(dict ["param1","value1":>obj])
      } 
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore

      // fetch again
      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Event[]>
        |> shouldBeLength 1
        |> Seq.head
        |> (fun x -> 
              x.EventName |> shouldEqual "some_event" |> ignore
              x.Params.Value.["param1"] |> shouldEqual "value1")
        |> ignore

    }

  [<Fact>]
  let ``ListIncomplete lists events where ProcessedOn is null`` () =
    task {
      
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here
      initialize() |> ignore

      TestHelper.execute (sprintf "INSERT INTO event (id,eventName,receivedOn,organizationId) VALUES('%s','%s','%s','%s') " (Guid.NewGuid().ToString()) "some_event" (DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")) "some_org")

      Sweep.Data.Event.listAllUnprocessed() |> Seq.toArray |> shouldBeLength 1 |> ignore
    }

  [<Fact>]
  let ``List all events after the ReceivedOn date of the provided event`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize() |> ignore
      TestHelper.execute (sprintf "INSERT INTO event (id,eventName,receivedOn,organizationId) VALUES('%s','%s','%s','%s') " (Guid.NewGuid().ToString()) "some_event" (DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")) TestHelper.orgId)
      let event = Sweep.Data.Event.list TestHelper.orgId |> Seq.head
      TestHelper.execute (sprintf "INSERT INTO event (id,eventName,receivedOn,organizationId) VALUES('%s','%s','%s','%s') " (Guid.NewGuid().ToString()) "some_event" (DateTime.Now.AddDays(float 1).ToString("yyyy-MM-dd H:mm:ss")) TestHelper.orgId)
      TestHelper.execute (sprintf "INSERT INTO event (id,eventName,receivedOn,organizationId) VALUES('%s','%s','%s','%s') " (Guid.NewGuid().ToString()) "some_event" (DateTime.Now.AddDays(float 1).ToString("yyyy-MM-dd H:mm:ss")) TestHelper.orgId)
      Sweep.Data.Event.listAllAfter event.Id |> Seq.toArray |> shouldBeLength 3 |> ignore
    }