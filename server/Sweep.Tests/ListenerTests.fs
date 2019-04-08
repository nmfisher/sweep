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
open Sweep.Data

module ListenerTests =

  // ---------------------------------
  // Tests
  // ---------------------------------

  [<Fact>]
  let ``Validate Listener Condition`` () =
    task {
      let parse1 = Listener.parse "AND FOO_EVENT WITHIN 12 DAYS MATCH ON BAZ" 
      Assert.Equal("FOO_EVENT", parse1.Value.EventName)
      Assert.Equal("BAZ", parse1.Value.Key.Value)
      Assert.Equal(12, parse1.Value.Duration.Days)

      let parse2 = Listener.parse "AND FOO_EVENT WITHIN 999 MINUTES MATCH ON BAZ"  
      Assert.Equal("FOO_EVENT", parse2.Value.EventName)
      Assert.Equal("BAZ", parse2.Value.Key.Value)
      Assert.Equal(double 999, parse2.Value.Duration.TotalMinutes)

      let parse3 = Listener.parse "AND FOO_EVENT WITHIN 29 HOURS MATCH ON BAZ"  
      Assert.Equal("FOO_EVENT", parse3.Value.EventName)
      Assert.Equal("BAZ", parse3.Value.Key.Value)
      Assert.Equal(double 29, parse3.Value.Duration.TotalHours)

      let parse4 = Listener.parse "AND FOO_EVENT WITHIN 29 HOURS MATCH ON NULL"  
      Assert.Equal("FOO_EVENT", parse4.Value.EventName)
      Assert.True(parse4.Value.Key.IsNone)
      Assert.Equal(double 29, parse4.Value.Duration.TotalHours)

      Assert.Equal(None, Listener.parse "")
      Assert.Equal(None, Listener.parse null)

      Assert.Throws<Exception>(Action(fun () -> 
        Listener.parse "AND FOO_EVENT WITHIN 999 DAYS MATCH ON BAZ"  |> ignore
      )) |> ignore

      Assert.Throws<Exception>(Action(fun () -> 
        Listener.parse "BLAH FOO_EVENT WITHIN 999 DAYS MATCH ON BAZ"  |> ignore
      )) |> ignore

      Assert.Throws<Exception>(Action(fun () -> 
        Listener.parse "AND FOO_EVENT INSIDE 999 DAYS MATCH ON BAZ"  |> ignore
      )) |> ignore

      Assert.Throws<Exception>(Action(fun () -> 
        Listener.parse "AND FOO_EVENT INSIDE 999 DAYS MATCH ON MATCH"  |> ignore
      )) |> ignore

      Assert.Throws<Exception>(Action(fun () -> 
        Listener.parse "AND FOO_EVENT WITHIN 999 SECONDS MATCH ON BAZ"  |> ignore
      )) |> ignore

    }