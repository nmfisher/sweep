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
  let ``fetchOrCreateUser successfully creates and returns user where one does not exist`` () =
    task {
      initialize() 
      let logger = Mock<ILogger>().Create()
      let user = CustomHandlers.fetchOrCreateUser "12345" "foo@foo.com" logger
      user.Id |> shouldEqual "12345" |> ignore
      user.Username |> shouldEqual "foo@foo.com" |> ignore

      // if user exists with a different ID but same username, the user is retrieved 
      // (e.g. if user created their initial account with Google but logs in with GitHub attached to the same e-mail address)
      let byId = CustomHandlers.fetchOrCreateUser "54321" "foo@foo.com" logger
      user.Id |> shouldEqual "12345" |> ignore
      user.Username |> shouldEqual "foo@foo.com" |> ignore

    }