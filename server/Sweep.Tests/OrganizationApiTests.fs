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
open OrganizationApiHandlerTestsHelper
open Sweep.OrganizationApiHandler
open Sweep.OrganizationApiHandlerParams
open Sweep.Model.Organization
open Newtonsoft.Json

module OrganizationApiHandlerTests =

  // ---------------------------------
  // Tests
  // ---------------------------------

  [<Fact>]
  let ``GetOrganizationInfo - Get organization info for the currently authenticated context returns 200 where successful operation`` () =
    task {
      use server = new TestServer(createHost())
      use client = server.CreateClient()

      initialize()

      let path = "/1.0.0/organization"

      HttpGet client path
        |> isStatus (enum<HttpStatusCode>(200))
        |> readText
        |> JsonConvert.DeserializeObject<Organization>
        |> (fun x -> 
              x.PrimaryApiKey |> shouldNotBeNull
              x.SecondaryApiKey |> shouldNotBeNull)
      }

