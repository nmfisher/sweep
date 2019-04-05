namespace Sweep.Tests

open System
open System.Collections.Generic
open System.Net
open System.Net.Http
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.TestHost
open TestHelper
open FSharp.Control.Tasks.V2.ContextInsensitive
open Xunit
open System.Text
open Sweep.EventQueue
open Sweep.Model.Template
open Sweep.Model.Listener
open Sweep.Model.Event

module EventQueueTests =

  // ---------------------------------
  // Tests
  // ---------------------------------
  [<Fact>]
  let ``Render default subject``() =
    task {
      let template = {
        Subject="";
        Content = "";
        FromAddress="foo@bar";
        FromName="Foo";
        SendTo=[|"baz@qux"|];
        UserId="";
        OrganizationId="";
        Deleted=false;
        Id="";
      }

      let event = {
         EventName="SOMEEVENT";
         Params=dict ["key1","val1" :> obj];
         Id = "";
         ReceivedOn = DateTime.Now;
         ProcessedOn = DateTime.Now;
         OrganizationId = "123";
         Error = "";
      }
      getSubject template event "foo" |> shouldEqual "foo" |> ignore
    }
  
  [<Fact>]
  let ``Dequeue and inspect generated SQL``() =
    task {
      // create a listener
      let listener =
      {
          EventName="some_event";
          UserId="";
          OrganizationId="";
          Id="";
          Deleted=false;
      } 
      |> encode
      |> HttpPost client "/listeners"
      |> isStatus (enum<HttpStatusCode>(200))
      
      let listener = 
        |> HttpGet client "/listeners" 
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Listener[]>
        |> Seq.head
      
      // create a template
      {
          Content="Hello";
          SendTo=[|"foo@bar"|];
          Subject="Some subject";
          FromAddress="baz@qux";
          FromName="Baz";
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

      // associate the template with the listenery       
      let path = "/listeners/" + listener.Id + "/templates/" + template.Id

      HttpPost client path null
        |> isStatus (enum<HttpStatusCode>(200))
      let dequeued = Sweep.EventQueue.dequeue()
    }
      
