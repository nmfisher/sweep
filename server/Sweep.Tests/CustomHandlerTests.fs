namespace Sweep.Tests

open TestHelper
open Microsoft.Extensions.Logging
open FSharp.Control.Tasks.V2.ContextInsensitive
open Xunit
open Giraffe
open Foq
open Sweep

module CustomHandlerTests =
  // ---------------------------------
  // Tests
  // ---------------------------------

  [<Fact>]
  let ``AddEvent - Raise an event returns 200 where An event has been successfully created.`` () =
    task {
      initialize() 
      let logger = Mock<ILogger>().Create()
      let user = CustomHandlers.fetchOrCreateUser "12345" "foo@foo.com" logger
      user.Id |> shouldEqual "12345"
      user.Username |> shouldEqual "foo@foo.com"

    }