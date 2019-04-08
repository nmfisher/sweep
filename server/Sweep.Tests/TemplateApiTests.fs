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
open TemplateApiHandlerTestsHelper
open Sweep.TemplateApiHandler
open Sweep.TemplateApiHandlerParams
open Sweep.Model.TemplateRequestBody
open Sweep.Model.Template
open Newtonsoft.Json

module TemplateApiHandlerTests =

  // ---------------------------------
  // Tests
  // ---------------------------------
  let getTemplate () = 
    {
      TemplateRequestBody.Content="Hello";
      SendTo=[|"foo@bar"|];
      Subject="Some subject";
      FromAddress="baz@qux";
      FromName="Baz";
    }  
      
  [<Fact>]
  let ``AddTemplate - Create a new Template returns 200 where Success`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()

      let path = "/1.0.0/templates"
      getTemplate()
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore

      {
        TemplateRequestBody.Content="Hello {{recipient}}";
        SendTo=[|"{{recipient}}"|];
        Subject="Some subject";
        FromAddress="baz@qux";
        FromName="Baz";
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

      let path = "/1.0.0/templates"

      // missing content
      {
          TemplateRequestBody.Content="";
          SendTo=[|"foo@bar"|];
          Subject="Some subject";
          FromAddress="baz@qux";
          FromName="Baz";
      } 
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore
      
      // missing sendTo
      {
          Content="Some content";
          SendTo=[||];
          Subject="Some subject";
          FromAddress="baz@qux";
          FromName="Baz";
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=Some(false);
      } 
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore

      // malformed sendTo
      {
          TemplateRequestBody.Content="Some content";
          SendTo=[|"not an email"|];
          Subject="Some subject";
          FromAddress="baz@qux";
          FromName="Baz";
      } 
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore

      // malformed sendTo
      {
          Content="Some content";
          SendTo=[|"{{missing}}"|];
          Subject="Some subject";
          FromAddress="baz@qux";
          FromName="Baz";
          Id="";
          OrganizationId="";
          UserId="";
          Deleted=Some(false);
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

      let path = "/1.0.0/templates"
      getTemplate()
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore
      
      let template = 
        HttpGet client "/1.0.0/templates"
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head

      let path = "/1.0.0/templates/" + template.Id

      HttpDelete client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> ignore

      HttpGet client "/1.0.0/templates"          
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

      let path = "/1.0.0/templates/{templateId}"

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

      let path = "/1.0.0/templates"
      getTemplate()
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore
      
      let template = 
        HttpGet client "/1.0.0/templates"
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head

      let path = "/1.0.0/templates/" + template.Id

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template>
        |> (fun x ->
          x.Content |> shouldEqual "Hello" |> ignore
          x.SendTo |> shouldBeLength 1 |> ignore
          x.SendTo.[0] |> shouldEqual "foo@bar" |> ignore
          x.Subject |> shouldEqual "Some subject" |> ignore
          x.FromAddress |> shouldEqual "baz@qux" |> ignore
          x.FromName |> shouldEqual "Baz" |> ignore
          )        
      }

  [<Fact>]
  let ``GetTemplateById - Find Template by ID returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()

      let path = "/1.0.0/templates/{templateId}"

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

      let path = "/1.0.0/templates"
      getTemplate()
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore

      {
          TemplateRequestBody.Content="Hello Again";
          SendTo=[|"baz@qux"|];
          Subject="Some subject";
          FromAddress="baz@qux";
          FromName="Baz";
      } 
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore
      
      let path = "/1.0.0/templates"

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
      getTemplate()
      |> encode
      |> HttpPost client "/1.0.0/templates"
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore

      let template = 
        HttpGet client "/1.0.0/templates"
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head
      // update the template with some valid data
      {
          TemplateRequestBody.Content="Hello Again";
          SendTo=[|"foo@bar";"baz@qux"|];
          Subject="Some other subject";
          FromAddress="me@you";
          FromName="Gaz";
      } 
        |> encode
        |> HttpPut client ("/1.0.0/templates/" + template.Id)
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore

      // refetch the template
      HttpGet client ("/1.0.0/templates/" + template.Id)
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText 
        |> JsonConvert.DeserializeObject<Template>
        |> (fun x -> 
              x.Content |> shouldEqual "Hello Again" |> ignore
              x.SendTo |> shouldBeLength 2 |> ignore
              x.SendTo |> Seq.head |> shouldEqual "foo@bar" |> ignore
              x.SendTo |> Seq.last |> shouldEqual "baz@qux" |> ignore
              x.Subject |> shouldEqual "Some other subject" |> ignore
              x.FromAddress |> shouldEqual "me@you" |> ignore
              x.FromName |> shouldEqual "Gaz" |> ignore
              )
        |> ignore
    }

  [<Fact>]
  let ``UpdateTemplate - Update an existing Template returns 404 where Template not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()
      let path = "/1.0.0/templates/{templateId}"
      getTemplate()
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

      getTemplate()
      |> encode
      |> HttpPost client "/1.0.0/templates"
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore

      let template = 
        HttpGet client "/1.0.0/templates"
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head

      {
          TemplateRequestBody.Content="";
          SendTo=[|"baz@qux"|];
          Subject="Some subject";
          FromAddress="baz@qux";
          FromName="Baz";
      }
        |> encode
        |> HttpPut client ("/1.0.0/templates/" + template.Id)
        |> isStatus (enum<HttpStatusCode>(422))
        |> ignore

      {
        TemplateRequestBody.Content="Hello Again";
        SendTo=[||];
        Subject="Some subject";
        FromAddress="baz@qux";
        FromName="Baz";
      }
      |> encode
      |> HttpPut client ("/1.0.0/templates/" + template.Id)
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore
    }

