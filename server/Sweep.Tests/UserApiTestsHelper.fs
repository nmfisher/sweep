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
open Sweep.UserApiHandler
open Sweep.UserApiHandlerParams

module UserApiHandlerTestsHelper =

  let mutable CreateUserExamples = Map.empty
  let mutable CreateUserBody = ""

  CreateUserBody <- WebUtility.HtmlDecode "{
  &quot;organizationId&quot; : &quot;organizationId&quot;,
  &quot;password&quot; : &quot;password&quot;,
  &quot;apiKey&quot; : &quot;apiKey&quot;,
  &quot;id&quot; : &quot;id&quot;,
  &quot;username&quot; : &quot;username&quot;
}"
  CreateUserExamples <- CreateUserExamples.Add("application/json", CreateUserBody)

  let getCreateUserExample mediaType =
    CreateUserExamples.[mediaType]
      |> getConverter mediaType
  let mutable UpdateUserExamples = Map.empty
  let mutable UpdateUserBody = ""

  UpdateUserBody <- WebUtility.HtmlDecode "{
  &quot;organizationId&quot; : &quot;organizationId&quot;,
  &quot;password&quot; : &quot;password&quot;,
  &quot;apiKey&quot; : &quot;apiKey&quot;,
  &quot;id&quot; : &quot;id&quot;,
  &quot;username&quot; : &quot;username&quot;
}"
  UpdateUserExamples <- UpdateUserExamples.Add("application/json", UpdateUserBody)

  let getUpdateUserExample mediaType =
    UpdateUserExamples.[mediaType]
      |> getConverter mediaType
