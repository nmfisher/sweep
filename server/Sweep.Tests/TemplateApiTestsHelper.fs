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
open TestHelper
open Sweep.TemplateApiHandler
open Sweep.TemplateApiHandlerParams

module TemplateApiHandlerTestsHelper =

  let mutable AddTemplateExamples = Map.empty
  let mutable AddTemplateBody = ""

  AddTemplateBody <- WebUtility.HtmlDecode "{
  &quot;template&quot; : &quot;template&quot;,
  &quot;organizationId&quot; : &quot;organizationId&quot;,
  &quot;id&quot; : &quot;id&quot;,
  &quot;to&quot; : [ &quot;to&quot;, &quot;to&quot; ],
  &quot;userId&quot; : &quot;userId&quot;
}"
  AddTemplateExamples <- AddTemplateExamples.Add("application/json", AddTemplateBody)

  let getAddTemplateExample mediaType =
    AddTemplateExamples.[mediaType]
      |> getConverter mediaType
  let mutable UpdateTemplateExamples = Map.empty
  let mutable UpdateTemplateBody = ""

  UpdateTemplateBody <- WebUtility.HtmlDecode "{
  &quot;template&quot; : &quot;template&quot;,
  &quot;organizationId&quot; : &quot;organizationId&quot;,
  &quot;id&quot; : &quot;id&quot;,
  &quot;to&quot; : [ &quot;to&quot;, &quot;to&quot; ],
  &quot;userId&quot; : &quot;userId&quot;
}"
  UpdateTemplateExamples <- UpdateTemplateExamples.Add("application/json", UpdateTemplateBody)

  let getUpdateTemplateExample mediaType =
    UpdateTemplateExamples.[mediaType]
      |> getConverter mediaType
