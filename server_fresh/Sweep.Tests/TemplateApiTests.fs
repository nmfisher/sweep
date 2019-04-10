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
open TemplateApiHandlerTestsHelper
open Sweep.TemplateApiHandler
open Sweep.TemplateApiHandlerParams
open Sweep.TemplateModel
open Sweep.TemplateRequestBodyModel

module TemplateApiHandlerTests =

  // ---------------------------------
  // Tests
  // ---------------------------------

  [<Fact>]
  let ``AddTemplate - Create a new Template returns 200 where Success`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/templates"

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getAddTemplateExample "application/json")
      // or pass a body of type TemplateRequestBody
      let body = obj() :?> TemplateRequestBody |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

      body
        |> HttpPost client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
      }

  [<Fact>]
  let ``AddTemplate - Create a new Template returns 422 where Invalid input`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/templates"

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getAddTemplateExample "application/json")
      // or pass a body of type TemplateRequestBody
      let body = obj() :?> TemplateRequestBody |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

      body
        |> HttpPost client path
        |> isStatus (enum<HttpStatusCode>(422))
        |> readText
        |> shouldEqual "TESTME"
      }

  [<Fact>]
  let ``DeleteTemplate - Deletes a Template returns 200 where Successful deletion`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/templates/{templateId}".Replace("templateId", "ADDME")

      HttpDelete client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``DeleteTemplate - Deletes a Template returns 404 where Template not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/templates/{templateId}".Replace("templateId", "ADDME")

      HttpDelete client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``GetTemplateById - Find Template by ID returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/templates/{templateId}".Replace("templateId", "ADDME")

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``GetTemplateById - Find Template by ID returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/templates/{templateId}".Replace("templateId", "ADDME")

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``ListTemplate - List all Templates returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/templates"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
        |> ignore
      }

  [<Fact>]
  let ``UpdateTemplate - Update an existing Template returns 200 where Successfully updated`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/templates/{templateId}".Replace("templateId", "ADDME")

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getUpdateTemplateExample "application/json")
      // or pass a body of type TemplateRequestBody
      let body = obj() :?> TemplateRequestBody |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

      body
        |> HttpPut client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> shouldEqual "TESTME"
      }

  [<Fact>]
  let ``UpdateTemplate - Update an existing Template returns 404 where Template not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/templates/{templateId}".Replace("templateId", "ADDME")

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getUpdateTemplateExample "application/json")
      // or pass a body of type TemplateRequestBody
      let body = obj() :?> TemplateRequestBody |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

      body
        |> HttpPut client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> readText
        |> shouldEqual "TESTME"
      }

  [<Fact>]
  let ``UpdateTemplate - Update an existing Template returns 422 where Validation exception`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/templates/{templateId}".Replace("templateId", "ADDME")

      // use an example requestBody provided by the spec
      let examples = Map.empty.Add("application/json", getUpdateTemplateExample "application/json")
      // or pass a body of type TemplateRequestBody
      let body = obj() :?> TemplateRequestBody |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

      body
        |> HttpPut client path
        |> isStatus (enum<HttpStatusCode>(422))
        |> readText
        |> shouldEqual "TESTME"
      }

