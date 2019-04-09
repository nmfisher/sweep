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
open Sweep.ListenerApiHandler
open Sweep.ListenerApiHandlerParams
open Sweep.Model.Listener
open Newtonsoft.Json
open Sweep
open Sweep.Model.Template
open Sweep.Model.TemplateRequestBody
open Sweep.Model.ListenerTemplate

module ListenerTemplateApiHandlerTests =

  [<Fact>]
  let ``AddListenerTemplate - Associates a Template to a Listener returns 200 where Successfully associated`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore

      // create a listener
      ListenerApiHandlerTests.``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously
      
      let listener = 
        "/1.0.0/listeners" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head
      
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
       
      let path = "/1.0.0/listeners/" + listener.Id + "/templates/" + template.Id

      HttpPost client path null
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore

      }

  [<Fact>]
  let ``AddListenerTemplate - Associates a Template to a Listener returns 404 where Listener or Template not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      let path = "/1.0.0/listeners/{listenerId}/templates/{templateId}"

      HttpPost client path null
        |> isStatus (enum<HttpStatusCode>(404))
        |> ignore
    }

  [<Fact>]
  let ``ListListenerTemplates - List Templates for Listener returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // create a listener
      ListenerApiHandlerTests.``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously
      let listener = 
        "/1.0.0/listeners" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head
      
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

      let path = "/1.0.0/listeners/" + listener.Id + "/templates"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<ListenerTemplate[]>
        |> shouldBeLength 0
        |> ignore

      HttpPost client (path + "/" + template.Id) null
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<ListenerTemplate[]>
        |> Seq.head
        |> (fun x -> x.TemplateId |> shouldEqual template.Id)
        |> ignore
      }

  [<Fact>]
  let ``ListListenerTemplates - List Templates for Listener returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      let path = "/1.0.0/listeners/{listenerId}/templates"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> readText
        |> ignore
    }

  [<Fact>]
  let ``listTemplatesForListener returns list of templates`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // create a listener
      ListenerApiHandlerTests.``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously
      let listener = 
        "/1.0.0/listeners" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head
      
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

      let path = "/1.0.0/listeners/" + listener.Id + "/templates"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<ListenerTemplate[]>
        |> shouldBeLength 0
        |> ignore

      HttpPost client (path + "/" + template.Id) null
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore

      let templates = Sweep.Data.ListenerTemplate.listTemplatesForListener listener.Id TestHelper.orgId |> Seq.toArray

      templates |> shouldBeLength 1 |> Seq.head |> (fun x -> x.Content |> shouldEqual "Hello") |> ignore
    }
