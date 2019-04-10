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
open ListenerApiHandlerTestsHelper
open Sweep.ListenerApiHandler
open Sweep.ListenerApiHandlerParams
open Sweep.ListenerModel
open Sweep.ListenerRequestBodyModel
open Sweep.ListenerTemplateModel

module ListenerApiHandlerTests =

  // ---------------------------------
  // Tests
  // ---------------------------------

  [<Fact>]
  let ``AddListener - Create a new Listener returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners"

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getAddListenerExample "application/json")
      // or pass a body of type ListenerRequestBody
      let body = obj() :?> ListenerRequestBody |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

      body
        |> HttpPost client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
      }

  [<Fact>]
  let ``AddListener - Create a new Listener returns 422 where Invalid input`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners"

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getAddListenerExample "application/json")
      // or pass a body of type ListenerRequestBody
      let body = obj() :?> ListenerRequestBody |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

      body
        |> HttpPost client path
        |> isStatus (enum<HttpStatusCode>(422))
        |> readText
        |> shouldEqual "TESTME"
      }

  [<Fact>]
  let ``AddListenerTemplate - Associates a Template to a Listener returns 200 where Successfully added`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners/{listenerId}/templates/{templateId}".Replace("listenerId", "ADDME").Replace("templateId", "ADDME")

      HttpPost client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``AddListenerTemplate - Associates a Template to a Listener returns 404 where Listener or template not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners/{listenerId}/templates/{templateId}".Replace("listenerId", "ADDME").Replace("templateId", "ADDME")

      HttpPost client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``DeleteListener - Deletes a Listener returns 200 where Successfully deleted`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners/{listenerId}".Replace("listenerId", "ADDME")

      HttpDelete client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``DeleteListener - Deletes a Listener returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners/{listenerId}".Replace("listenerId", "ADDME")

      HttpDelete client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``DeleteListenerTemplate - Disassociates a Template from a Listener returns 200 where Successfully deleted`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners/{listenerId}/templates/{templateId}".Replace("listenerId", "ADDME").Replace("templateId", "ADDME")

      HttpDelete client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``DeleteListenerTemplate - Disassociates a Template from a Listener returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners/{listenerId}/templates/{templateId}".Replace("listenerId", "ADDME").Replace("templateId", "ADDME")

      HttpDelete client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``ListListenerTemplates - List Templates for Listener returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners/{listenerId}/templates".Replace("listenerId", "ADDME")

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``ListListenerTemplates - List Templates for Listener returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners/{listenerId}/templates".Replace("listenerId", "ADDME")

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``ListListeners - List all Listeners returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``UpdateListener - Updates a Listener returns 200 where Successfully deleted`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners/{listenerId}".Replace("listenerId", "ADDME")

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getUpdateListenerExample "application/json")
      // or pass a body of type ListenerRequestBody
      let body = obj() :?> ListenerRequestBody |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

      body
        |> HttpPut client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
      }

  [<Fact>]
  let ``UpdateListener - Updates a Listener returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners/{listenerId}".Replace("listenerId", "ADDME")

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getUpdateListenerExample "application/json")
      // or pass a body of type ListenerRequestBody
      let body = obj() :?> ListenerRequestBody |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

      body
        |> HttpPut client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> readText
        |> shouldEqual "TESTME"
      }

