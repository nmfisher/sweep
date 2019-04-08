namespace Sweep.Tests

open System
open System.Collections.Generic
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
open Newtonsoft.Json
open TestHelper
open EventApiHandlerTestsHelper
open Sweep.EventApiHandler
open Sweep.EventApiHandlerParams
open Sweep.Model.Event
open Sweep.Model.EventRequestBody
open Microsoft.AspNetCore.Hosting
open FSharp.Data.Sql
open FSharp.Data.Sql.Providers

module ListenerActionTests =

  // ---------------------------------
  // Tests
  // ---------------------------------

  [<Fact>]
  let ``Validate Listener Condition`` () =
    task {
      let condition = "AND FOO_EVENT WITHIN 12 DAYS"  
      let parse = ListenerAction.parse condition
      Assert.True(parse.IsSome)
      Assert.Equal("FOO_EVENT", parse.Value.EventName)
      Assert.Equal(12, parse.Value.Duration.Days)
    }