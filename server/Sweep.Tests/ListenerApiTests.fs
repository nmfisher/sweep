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
open Newtonsoft.Json
open Sweep
open Sweep.TemplateModel
open Sweep.ListenerTemplateModel

module ListenerApiHandlerTests =

  let dbLock = obj()

  // ---------------------------------
  // Tests
  // ---------------------------------
  [<Fact>]
  let ``AddListener - Create a new Listener returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      lock(dbLock) (fun () ->
        initialize() |> ignore
      )

      let path = "/listeners"
      {
          EventName="some_event";
          UserId="";
          OrganizationId="";
          Id="";
          Deleted=false;
      } 
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore
    }

  [<Fact>]
  let ``AddListener - Create a new Listener returns 405 where Invalid input`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      lock(dbLock) (fun () ->
        initialize() |> ignore
      )

      let path = "/listeners"
      {
          EventName="";
          UserId="";
          OrganizationId="";
          Id="";
          Deleted=false;
      } 
      |> encode
      |> HttpPost client path
      |> isStatus (enum<HttpStatusCode>(405))
      |> ignore
    }

  [<Fact>]
  let ``AddListenerTemplate - Associates a Template to a Listener returns 200 where Successfully associated`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore

      // create a listener
      ``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously
      
      let listener = 
        "/listeners" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head
      
      // create a template
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
        "/templates" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head
       
      let path = "/listeners/" + listener.Id + "/templates/" + template.Id

      HttpPost client path null
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore

      }

  [<Fact>]
  let ``AddListenerTemplate - Associates a Template to a Listener returns 404 where Listener or Template not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      let path = "/listeners/{listenerId}/templates/{templateId}"

      HttpPost client path null
        |> isStatus (enum<HttpStatusCode>(404))
        |> ignore
      }

  [<Fact>]
  let ``DeleteListener - Deletes a Listener returns 200 where Successfully deleted`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore

      ``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously

      let listener = 
        "/listeners" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head

      "/listeners/" + listener.Id
        |> HttpDelete client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> ignore

      "/listeners" + listener.Id
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(404))
        |> ignore

      "/listeners" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> shouldBeLength 0
        |> ignore

      }

  [<Fact>]
  let ``DeleteListener - Deletes a Listener returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore

      let path = "/listeners/{listenerId}"

      HttpDelete client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> ignore
    }

  [<Fact>]
  let ``DeleteListenerTemplate - Disassociates a Template from a Listener returns 200 where Successfully deleted`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize()

      // create a listener
      ``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously
      let listener = 
        "/listeners" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head
      
      // create a template
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
        "/templates" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head
       
      let path = "/listeners/" + listener.Id + "/templates/" + template.Id

      HttpPost client path null
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore

      HttpDelete client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> ignore

      HttpGet client ("/listeners/" + listener.Id + "/templates")
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<ListenerTemplate[]>
        |> shouldBeLength 0
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
        |> ignore
      }

  [<Fact>]
  let ``GetListenerById - Find Listener by ID returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore

      // add your setup code here
      ``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously
      let listener = 
        "/listeners" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head

      let path = "/listeners/" + listener.Id

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener>
        |> (fun x -> 
              x.EventName |> shouldEqual("some_event") |> ignore
              x.Deleted |> shouldEqual false |> ignore)
      }

  [<Fact>]
  let ``GetListenerById - Find Listener by ID returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize() |> ignore
      let path = "/listeners/{listenerId}"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> readText
        |> ignore
    }

  [<Fact>]
  let ``ListListenerTemplates - List Templates for Listener returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // create a listener
      ``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously
      let listener = 
        "/listeners" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head
      
      // create a template
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
        "/templates" 
        |> HttpGet client
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Template[]>
        |> Seq.head

      let path = "/listeners/" + listener.Id + "/templates"

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

      let path = "/listeners/{listenerId}/templates"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> readText
        |> ignore
      }

  [<Fact>]
  let ``ListListeners - List all Listeners returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      ``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously

      let path = "/listeners"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head
        |> (fun x ->
            x.EventName |> shouldEqual ("some_event"))
        |> ignore
      }

  [<Fact>]
  let ``UpdateListener - Update an existing Listener returns 404 where Listener not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      // add your setup code here

      let path = "/listeners/{listenerId}"
      {
          EventName="some event name";
          UserId="";
          OrganizationId="";
          Id="";
          Deleted=false;
      } 
      |> encode
      |> HttpPut client path
      |> isStatus (enum<HttpStatusCode>(404))
      |> ignore
    }

  [<Fact>]
  let ``UpdateListener - Update an existing Listener returns 422 where Validation exception`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      ``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously

      let listener = HttpGet client "/listeners"
                      |> isStatus (enum<HttpStatusCode>(200))
                      |> readText
                      |> JsonConvert.DeserializeObject<Listener[]>
                      |> Seq.head

      let path = "/listeners/" + listener.Id
      {
          EventName="";
          UserId="";
          OrganizationId="";
          Id="";
          Deleted=false;
      } 
      |> encode
      |> HttpPut client path 
      |> isStatus (enum<HttpStatusCode>(422))
      |> ignore
    }

  [<Fact>]
  let ``UpdateListener - Update an existing Listener returns 200 where Successfully updated`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      ``AddListener - Create a new Listener returns 200 where successful operation``() |> Async.AwaitTask |> Async.RunSynchronously

      let listener = HttpGet client "/listeners"
                      |> isStatus (enum<HttpStatusCode>(200))
                      |> readText
                      |> JsonConvert.DeserializeObject<Listener[]>
                      |> Seq.head

      let path = "/listeners/" + listener.Id
      {
          EventName="some_new_name";
          UserId="";
          OrganizationId="";
          Id="";
          Deleted=false;
      } 
      |> encode 
      |> HttpPut client path 
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore

      "/listeners/" + listener.Id
      |> HttpGet client 
      |> isStatus (enum<HttpStatusCode>(200))
      |> readText
      |> JsonConvert.DeserializeObject<Listener>
      |> (fun x -> x.EventName |> shouldEqual "some_new_name")
      |> ignore

    }