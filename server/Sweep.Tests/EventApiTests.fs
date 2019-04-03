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
open Newtonsoft.Json
open TestHelper
open EventApiHandlerTestsHelper
open Sweep.EventApiHandler
open Sweep.EventApiHandlerParams
open Sweep.EventModel
open Sweep.LoggedEventModel
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

      lock(dbLock) (fun () ->
        initialize() |> ignore
      )

      // add your setup code here
      let path = "/events"

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getAddEventExample "application/json")
      // or pass a body of type Event
      let body = 
        {
          EventName="some_event";
          Params=[|"param1"|];
          OrganizationId="UNUSED"
        } |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

      let resp = body |> HttpPost client path
      let content = resp.Content.ReadAsStringAsync() |> Async.AwaitTask |> Async.RunSynchronously
      resp 
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

      // add your setup code here

      let path = "/events"

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getAddEventExample "application/json")
      // or pass a body of type Event
      let body = 
        { EventName="";
          Params=[|"param1"|];
          OrganizationId="" } 
        |> Newtonsoft.Json.JsonConvert.SerializeObject 
        |> Encoding.UTF8.GetBytes 
        |> MemoryStream 
        |> StreamContent

      body
        |> HttpPost client path
        |> isStatus (enum<HttpStatusCode>(405))
        |> readText
      }

  [<Fact>]
  let ``GetEventById - Find raised event by ID returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here
      { EventName="some_event";Params=[|"param1"|];OrganizationId="" } 
        |> Newtonsoft.Json.JsonConvert.SerializeObject 
        |> Encoding.UTF8.GetBytes 
        |> MemoryStream 
        |> StreamContent
        |> HttpPost client "/events"
        |> ignore
      
      let eventId = 
        HttpGet client "/events"
          |> isStatus (enum<HttpStatusCode>(200))
          |> readText
          |> JsonConvert.DeserializeObject<LoggedEvent[]>
          |> Seq.head
          |> (fun x-> x.Id)

      let path = "/events/" + eventId

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<LoggedEvent>
        |> (fun x -> 
            x.EventName |> shouldEqual "some_event" |> ignore
            x.Params |> shouldBeLength 1 |> ignore
            x.Params.[0].ToString() |> shouldEqual "param1" |> ignore)
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
      |> JsonConvert.DeserializeObject<LoggedEvent[]>
      |> shouldBeLength 0
      |> ignore

      // add an event for our organization so we can ensure it's returned properly
      { EventName="some_event";Params=[|"param1"|];OrganizationId="" } 
        |> Newtonsoft.Json.JsonConvert.SerializeObject 
        |> Encoding.UTF8.GetBytes 
        |> MemoryStream 
        |> StreamContent
        |> HttpPost client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore

      // fetch again
      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<LoggedEvent[]>
        |> shouldBeLength 1
        |> Seq.head
        |> (fun x -> 
              x.EventName |> shouldEqual "some_event" |> ignore
              x.Params |> Seq.head |> (fun x -> x.ToString()) |> shouldEqual "param1"  |> ignore)
    }

