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
open MessageApiHandlerTestsHelper
open Sweep.MessageApiHandler
open Sweep.MessageApiHandlerParams
open Sweep.MessageModel
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
      let id = (Guid.NewGuid().ToString())
      sprintf "INSERT INTO message (id,content,sentTo,userId,organizationId) VALUES('%s', '%s', '%s', '%s', '%s')" 
        id
        "some content"
        "[\"user@foo.com\"]"
        "userId"
        "orgId"
        |> TestHelper.execute 
      
      "/messages/" + id
      |> HttpGet client
      |> isStatus (enum<HttpStatusCode>(200))
      |> ignore
    }

  [<Fact>]
  let ``GetMessageById - Find message by ID returns 404 where message not found`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      let path = "/messages/{messageId}".Replace("messageId", "ADDME")

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

      sprintf "INSERT INTO message (id,content,sentTo,userId,organizationId) VALUES('%s', '%s', '%s', '%s', '%s')" 
        id
        "some content"
        "[\"user@foo.com\"]"
        "userId"
        "orgId"
        |> TestHelper.execute 

      let path = "/messages"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Message[]>
        |> Seq.head
        |> (fun x -> 
          x.Content |> shouldEqual "some content" |> ignore
          x.SentTo |> shouldBeLength 1 |> Seq.head |> shouldEqual "user@foo.com" |> ignore)
    }

