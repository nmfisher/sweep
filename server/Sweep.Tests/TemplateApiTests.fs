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
open Newtonsoft.Json

module TemplateApiHandlerTests =

  // ---------------------------------
  // Tests
  // ---------------------------------

  [<Fact>]
  let ``AddTemplate - Create a new Template returns 200 where Success`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()

      let path = "/templates"
      {
          Content="Hello";
          SendTo=[|"foo@bar"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> Newtonsoft.Json.JsonConvert.SerializeObject 
      |> Encoding.UTF8.GetBytes 
      |> MemoryStream 
      |> StreamContent
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore

      {
          Content="Hello {{recipient}}";
          SendTo=[|"{{recipient}}"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> Newtonsoft.Json.JsonConvert.SerializeObject 
      |> Encoding.UTF8.GetBytes 
      |> MemoryStream 
      |> StreamContent
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore
    }

  [<Fact>]
  let ``AddTemplate - Create a new Template returns 422 where Invalid input`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()

      let path = "/templates"
      {
          Content="";
          SendTo=[|"foo@bar"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> Newtonsoft.Json.JsonConvert.SerializeObject 
      |> Encoding.UTF8.GetBytes 
      |> MemoryStream 
      |> StreamContent
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore

      {
          Content="Some content";
          SendTo=[||];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> Newtonsoft.Json.JsonConvert.SerializeObject 
      |> Encoding.UTF8.GetBytes 
      |> MemoryStream 
      |> StreamContent
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore

      {
          Content="Some content";
          SendTo=[|"not an email"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> Newtonsoft.Json.JsonConvert.SerializeObject 
      |> Encoding.UTF8.GetBytes 
      |> MemoryStream 
      |> StreamContent
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore

      {
          Content="Some content";
          SendTo=[|"{{missing}}"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> Newtonsoft.Json.JsonConvert.SerializeObject 
      |> Encoding.UTF8.GetBytes 
      |> MemoryStream 
      |> StreamContent
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore
    }

  [<Fact>]
  let ``DeleteTemplate - Deletes a Template returns 200 where Successful deletion`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()

      let path = "/templates"
      {
          Content="Hello";
          SendTo=[|"foo@bar"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> Newtonsoft.Json.JsonConvert.SerializeObject 
      |> Encoding.UTF8.GetBytes 
      |> MemoryStream 
      |> StreamContent
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore
      
      let template = 
        HttpGet client "/templates"
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head

      let path = "/templates/" + template.Id

      HttpDelete client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore
    }

  [<Fact>]
  let ``DeleteTemplate - Deletes a Template returns 404 where Template not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()

      // add your setup code here

      let path = "/templates/{templateId}"

      HttpDelete client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> ignore
      }
  
  [<Fact>]
  let ``GetTemplateById - Find Template by ID returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()

      let path = "/templates"
      {
          Content="Hello";
          SendTo=[|"foo@bar"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> Newtonsoft.Json.JsonConvert.SerializeObject 
      |> Encoding.UTF8.GetBytes 
      |> MemoryStream 
      |> StreamContent
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore
      
      let template = 
        HttpGet client "/templates"
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head

      let path = "/templates/" + template.Id

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template>
        |> (fun x ->
          x.Content |> shouldEqual "Hello" |> ignore
          x.SendTo |> shouldBeLength 1 |> ignore
          x.SendTo.[0] |> shouldEqual "foo@bar" |> ignore)        
      }

  [<Fact>]
  let ``GetTemplateById - Find Template by ID returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()

      let path = "/templates/{templateId}"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> ignore
      }

  [<Fact>]
  let ``ListTemplate - List all Templates returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()

      let path = "/templates"
      {
          Content="Hello";
          SendTo=[|"foo@bar"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> Newtonsoft.Json.JsonConvert.SerializeObject 
      |> Encoding.UTF8.GetBytes 
      |> MemoryStream 
      |> StreamContent
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore

      {
          Content="Hello Again";
          SendTo=[|"baz@qux"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> Newtonsoft.Json.JsonConvert.SerializeObject 
      |> Encoding.UTF8.GetBytes 
      |> MemoryStream 
      |> StreamContent
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore
      
      let path = "/templates"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> shouldBeLength 2
        |> ignore
      }

  // [<Fact>]
  // let ``UpdateTemplate - Update an existing Template returns 400 where Invalid ID supplied`` () =
  //   task {
  //     use server = new TestServer(createHost())
  //     use client = server.CreateClient()

  //     // add your setup code here

  //     let path = "/templates"

  //     // use an example requestBody provided by the spec
  //     let examples = Map.empty.Add("application/json", getUpdateTemplateExample "application/json")
  //     // or pass a body of type Template
  //     let body = obj() :?> Template |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

  //     body
  //       |> HttpPut client path
  //       |> isStatus (enum<HttpStatusCode>(400))
  //       |> readText
  //       |> shouldEqual "TESTME"
  //     }

  // [<Fact>]
  // let ``UpdateTemplate - Update an existing Template returns 404 where Template not found`` () =
  //   task {
  //     use server = new TestServer(createHost())
  //     use client = server.CreateClient()

  //     // add your setup code here

  //     let path = "/templates"

  //     // use an example requestBody provided by the spec
  //     let examples = Map.empty.Add("application/json", getUpdateTemplateExample "application/json")
  //     // or pass a body of type Template
  //     let body = obj() :?> Template |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

  //     body
  //       |> HttpPut client path
  //       |> isStatus (enum<HttpStatusCode>(404))
  //       |> readText
  //       |> shouldEqual "TESTME"
  //     }

  // [<Fact>]
  // let ``UpdateTemplate - Update an existing Template returns 422 where Validation exception`` () =
  //   task {
  //     use server = new TestServer(createHost())
  //     use client = server.CreateClient()

  //     // add your setup code here

  //     let path = "/templates"

  //     // use an example requestBody provided by the spec
  //     let examples = Map.empty.Add("application/json", getUpdateTemplateExample "application/json")
  //     // or pass a body of type Template
  //     let body = obj() :?> Template |> Newtonsoft.Json.JsonConvert.SerializeObject |> Encoding.UTF8.GetBytes |> MemoryStream |> StreamContent

  //     body
  //       |> HttpPut client path
  //       |> isStatus (enum<HttpStatusCode>(422))
  //       |> readText
  //       |> shouldEqual "TESTME"
  //     }

