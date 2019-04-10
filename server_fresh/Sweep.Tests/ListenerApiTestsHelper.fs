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
open Sweep.ListenerApiHandler
open Sweep.ListenerApiHandlerParams

module ListenerApiHandlerTestsHelper =

  let mutable AddListenerExamples = Map.empty
  let mutable AddListenerBody = ""

  AddListenerBody <- WebUtility.HtmlDecode "{
  &quot;eventParams&quot; : [ &quot;eventParams&quot;, &quot;eventParams&quot; ],
  &quot;eventName&quot; : &quot;eventName&quot;,
  &quot;trigger&quot; : &quot;trigger&quot;
}"
  AddListenerExamples <- AddListenerExamples.Add("application/json", AddListenerBody)

  let getAddListenerExample mediaType =
    AddListenerExamples.[mediaType]
      |> getConverter mediaType
  let mutable UpdateListenerExamples = Map.empty
  let mutable UpdateListenerBody = ""

  UpdateListenerBody <- WebUtility.HtmlDecode "{
  &quot;eventParams&quot; : [ &quot;eventParams&quot;, &quot;eventParams&quot; ],
  &quot;eventName&quot; : &quot;eventName&quot;,
  &quot;trigger&quot; : &quot;trigger&quot;
}"
  UpdateListenerExamples <- UpdateListenerExamples.Add("application/json", UpdateListenerBody)

  let getUpdateListenerExample mediaType =
    UpdateListenerExamples.[mediaType]
      |> getConverter mediaType
