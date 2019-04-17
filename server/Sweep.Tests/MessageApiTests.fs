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
open MessageApiHandlerTestsHelper
open Sweep.MessageApiHandler
open Sweep.MessageApiHandlerParams
open Sweep.Model.Message
open Newtonsoft.Json

module MessageApiHandlerTests =

  // ---------------------------------
  // Tests
  // ---------------------------------

  [<Fact>]
  let ``GetMessageById - Find message by ID returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()
      initialize()

      let id = (Guid.NewGuid().ToString())
      sprintf "INSERT INTO message (id,subject,content,sendTo,fromAddress,fromName,organizationId,listenerActionId) VALUES('%s', '%s', '%s', '%s', '%s', '%s','%s','%s')" 
        id
        ""
        "some content"
        "[\"user@foo.com\"]"
        "me@you.com"
        "Me"
        TestHelper.orgId
        (Guid.NewGuid().ToString())
        |> TestHelper.execute 
      
      "/1.0.0/messages/" + id
      |> HttpGet client
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore
    }

  [<Fact>]
  let ``GetMessageById - Find message by ID returns 404 where message not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize()

      let path = "/1.0.0/messages/{messageId}".Replace("messageId", "ADDME")

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(404))
        |> ignore
    }

  [<Fact>]
  let ``ListMessages - List all messages returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      let id = (Guid.NewGuid().ToString())

      sprintf "INSERT INTO message (id,subject,content,sendTo,fromAddress,fromName,organizationId,listenerActionId,sentOn) VALUES('%s', '%s', '%s', '%s', '%s', '%s','%s','%s','%s')" 
        id
        ""
        "some content"
        "[\"user@foo.com\"]"
        "me@you.com"
        "Me"
        TestHelper.orgId
        (Guid.NewGuid().ToString())
        (DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"))
        |> TestHelper.execute 

      let path = "/1.0.0/messages"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Message[]>
        |> Seq.head
        |> (fun x -> 
          x.Content |> shouldEqual "some content" |> ignore
          x.SendTo |> shouldBeLength 1 |> Seq.head |> shouldEqual "user@foo.com" |> ignore)
    }


  [<Fact>]
  let ``ListMessages - List all messages by startDate and endDate returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      sprintf "INSERT INTO message (id,subject,content,sendTo,fromAddress,fromName,organizationId,listenerActionId,sentOn) VALUES('%s', '%s', '%s', '%s', '%s', '%s','%s','%s','%s')" 
        (Guid.NewGuid().ToString())
        ""
        "today"
        "[\"user@foo.com\"]"
        "me@you.com"
        "Me"
        TestHelper.orgId
        (Guid.NewGuid().ToString())
        (DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"))
        |> TestHelper.execute 

      sprintf "INSERT INTO message (id,subject,content,sendTo,fromAddress,fromName,organizationId,listenerActionId,sentOn) VALUES('%s', '%s', '%s', '%s', '%s', '%s','%s','%s','%s')" 
        (Guid.NewGuid().ToString())
        ""
        "7days"
        "[\"user@foo.com\"]"
        "me@you.com"
        "Me"
        TestHelper.orgId
        (Guid.NewGuid().ToString())
        (DateTime.Now.AddDays(float(-7)).ToString("yyyy-MM-dd H:mm:ss"))
        |> TestHelper.execute 

      sprintf "INSERT INTO message (id,subject,content,sendTo,fromAddress,fromName,organizationId,listenerActionId,sentOn) VALUES('%s', '%s', '%s', '%s', '%s', '%s','%s','%s','%s')" 
        (Guid.NewGuid().ToString())
        ""
        "3days"
        "[\"user@foo.com\"]"
        "me@you.com"
        "Me"
        TestHelper.orgId
        (Guid.NewGuid().ToString())
        (DateTime.Now.AddDays(float(-3)).ToString("yyyy-MM-dd H:mm:ss"))
        |> TestHelper.execute 

      let path = "/1.0.0/messages?startDate=" + (DateTime.Now.AddDays(float(-3)).ToString("yyyy-MM-dd H:mm:ss")) + "&endDate=" + (DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"))

      HttpGet client path
          |> isStatus (enum<HttpStatusCode>(200))
          |> readText
          |> JsonConvert.DeserializeObject<Message[]>
          |> (fun x -> 
            x |> Seq.find (fun y  -> y.Content = "today") |> ignore
            x |> Seq.find (fun y  -> y.Content = "3days") |> ignore
          ) |> ignore
    }

