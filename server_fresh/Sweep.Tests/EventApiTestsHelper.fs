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
open TestHelper
open Sweep.EventApiHandler
open Sweep.EventApiHandlerParams

module EventApiHandlerTestsHelper =

  let mutable AddEventExamples = Map.empty
  let mutable AddEventBody = ""

  AddEventBody <- WebUtility.HtmlDecode "{
  &quot;eventName&quot; : &quot;eventName&quot;,
  &quot;params&quot; : {
    &quot;key&quot; : &quot;{}&quot;
  }
}"
  AddEventExamples <- AddEventExamples.Add("application/json", AddEventBody)

  let getAddEventExample mediaType =
    AddEventExamples.[mediaType]
      |> getConverter mediaType
