namespace Sweep.Tests

// F#
module Program =
  [<EntryPoint>]
  let main(args: string[]) = 
    EventApiHandlerTests.``AddEvent - Raise an event returns 200 where An event has been successfully created.``()
    // EventApiHandlerTests.``ListEvents - List all received events returns 200 where successful operation``()
    |> Async.AwaitTask
    |> Async.RunSynchronously
    0