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
open Sweep.TemplateApiHandler
open Sweep.TemplateApiHandlerParams

module TemplateApiHandlerTestsHelper =

  let mutable AddTemplateExamples = Map.empty
  let mutable AddTemplateBody = ""

  AddTemplateBody <- WebUtility.HtmlDecode "{
  &quot;sendTo&quot; : [ &quot;sendTo&quot;, &quot;sendTo&quot; ],
  &quot;subject&quot; : &quot;subject&quot;,
  &quot;fromName&quot; : &quot;fromName&quot;,
  &quot;fromAddress&quot; : &quot;fromAddress&quot;,
  &quot;content&quot; : &quot;content&quot;
}"
  AddTemplateExamples <- AddTemplateExamples.Add("application/json", AddTemplateBody)

  let getAddTemplateExample mediaType =
    AddTemplateExamples.[mediaType]
      |> getConverter mediaType
  let mutable UpdateTemplateExamples = Map.empty
  let mutable UpdateTemplateBody = ""

  UpdateTemplateBody <- WebUtility.HtmlDecode "{
  &quot;sendTo&quot; : [ &quot;sendTo&quot;, &quot;sendTo&quot; ],
  &quot;subject&quot; : &quot;subject&quot;,
  &quot;fromName&quot; : &quot;fromName&quot;,
  &quot;fromAddress&quot; : &quot;fromAddress&quot;,
  &quot;content&quot; : &quot;content&quot;
}"
  UpdateTemplateExamples <- UpdateTemplateExamples.Add("application/json", UpdateTemplateBody)

  let getUpdateTemplateExample mediaType =
    UpdateTemplateExamples.[mediaType]
      |> getConverter mediaType
