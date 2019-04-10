namespace Sweep.Tests

open System
open System.Net
open System.Net.Http
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.TestHost
open Microsoft.Extensions.DependencyInjection
open FSharp.Control.Tasks.V2.ContextInsensitive
open Xunit
open System.Text
open Newtonsoft
open TestHelper
open EventApiHandlerTestsHelper
open Sweep.EventApiHandler
open Sweep.EventApiHandlerParams
open Sweep.EventModel
open Sweep.EventRequestBodyModel

module EventApiHandlerTests =

  // ---------------------------------
  // Tests
  // ---------------------------------

  [<Fact>]
  let ``AddEvent - Raise an event returns 200 where An event has been successfully created.`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/events"

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getAddEventExample "application/json")
      // or pass a body of type EventRequestBody
      let body = obj() :?> EventRequestBody |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

      body
        |> HttpPost client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
      }

  [<Fact>]
  let ``AddEvent - Raise an event returns 422 where Invalid input`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/events"

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getAddEventExample "application/json")
      // or pass a body of type EventRequestBody
      let body = obj() :?> EventRequestBody |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

      body
        |> HttpPost client path
        |> isStatus (enum<HttpStatusCode>(422))
        |> readText
        |> shouldEqual "TESTME"
      }

  [<Fact>]
  let ``GetEventById - Find raised event by ID returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/events/{eventId}".Replace("eventId", "ADDME")

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``GetEventById - Find raised event by ID returns 404 where Order not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/events/{eventId}".Replace("eventId", "ADDME")

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``ListEvents - List all received events returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/events"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

