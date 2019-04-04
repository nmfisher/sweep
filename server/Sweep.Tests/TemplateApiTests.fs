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
  let encode x= 
    x 
    |> Newtonsoft.Json.JsonConvert.SerializeObject 
    |> Encoding.UTF8.GetBytes 
    |> MemoryStream 
    |> StreamContent

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
      |> encode
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
      |> encode
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
      |> encode
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
      |> encode
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
      |> encode
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
      |> encode
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
      |> encode
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

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> ignore

      HttpGet client "/templates"          
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> shouldBeLength 0
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
      |> encode
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
      |> encode
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
      |> encode
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

  [<Fact>]
  let ``UpdateTemplate - Update an existing Template returns 200 where Successfully updated`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()
      // create a template then fetch its id
      {
          Content="Hello";
          SendTo=[|"foo@bar"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> encode
      |> HttpPost client "/templates"
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore

      let template = 
        HttpGet client "/templates"
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head
      // update the template with some valid data
      {
          Content="Hello Again";
          SendTo=[|"foo@bar";"baz@qux"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
        |> encode
        |> HttpPut client ("/templates/" + template.Id)
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore

      // refetch the template
      HttpGet client ("/templates/" + template.Id)
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText 
        |> JsonConvert.DeserializeObject<Template>
        |> (fun x -> 
              x.Content |> shouldEqual "Hello Again" |> ignore
              x.SendTo |> shouldBeLength 2 |> ignore
              x.SendTo |> Seq.head |> shouldEqual "foo@bar" |> ignore
              x.SendTo |> Seq.last |> shouldEqual "baz@qux" |> ignore
              )
        |> ignore
    }

  [<Fact>]
  let ``UpdateTemplate - Update an existing Template returns 404 where Template not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()
      let path = "/templates/{templateId}"
      {
          Content="Hello";
          SendTo=[|"foo@bar"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> encode
      |> HttpPut client path
      |> isStatus (enum<HttpStatusCode>(404))
      |> ignore
    }

  [<Fact>]
  let ``UpdateTemplate - Update an existing Template returns 422 where Validation exception`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()

      {
          Content="Hello";
          SendTo=[|"foo@bar"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
      |> encode
      |> HttpPost client "/templates"
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore

      let template = 
        HttpGet client "/templates"
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head

      {
          Content="";
          SendTo=[|"baz@qux"|];
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=false;
      } 
        |> encode
        |> HttpPut client ("/templates/" + template.Id)
        |> isStatus (enum<HttpStatusCode>(422))
        |> ignore

      {
        Content="Hello Again";
        SendTo=[||];
        Id="";
        OrganizationId="";
        UserId="";
        Deleted=false;
      }  
      |> encode
      |> HttpPut client ("/templates/" + template.Id)
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore
    }

