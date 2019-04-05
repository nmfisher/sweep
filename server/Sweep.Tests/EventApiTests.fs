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

      let path = "/events"

      {
          EventRequestBody.EventName="some_event";
          Params=dict ["param1","value1" :> obj]
      } 
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> readText
      |> shouldEqual "OK"
      |> ignore
  }

  [<Fact>]
  let ``AddEvent - Raise an event returns 405 where Invalid input`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      let path = "/events"

      { 
        EventRequestBody.EventName="";
        Params=dict [ "param1","value1" :> obj]
      }
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(405))
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
        Params=dict [ "param1","value1":>obj];
      } 
      |> encode
      |> HttpPost client "/events"
      |> ignore
      
      let eventId = 
        HttpGet client "/events"
          |> isStatus (enum<HttpStatusCode>(200))
          |> readText
          |> JsonConvert.DeserializeObject<Event[]>
          |> Seq.head
          |> (fun x-> x.Id)

      let path = "/events/" + eventId

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Event>
        |> (fun x -> 
            x.EventName |> shouldEqual "some_event" |> ignore
            x.Params.Keys.Count |> shouldEqual 1 |> ignore
            x.Params.["param1"] |> shouldEqual "value1" |> ignore)
      }

  [<Fact>]
  let ``GetEventById - Find raised event by ID returns 404 where Order not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/events/111"

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
      lock(dbLock) (fun () ->
        initialize() |> ignore
      )

      let path = "/events"

      HttpGet client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> readText
      |> JsonConvert.DeserializeObject<Event[]>
      |> shouldBeLength 0
      |> ignore

      // add an event for our organization so we can ensure it's returned properly
      { 
        EventRequestBody.EventName="some_event";
        Params= dict ["param1","value1" :> obj];
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
              x.Params.["param1"] |> shouldEqual "value1")
        |> ignore

    }

