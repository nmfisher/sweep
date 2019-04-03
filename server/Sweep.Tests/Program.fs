namespace Sweep.Tests

open System.Threading.Tasks

// F#
module Program =
  [<EntryPoint>]
  let main(args: string[]) = 
    let tasks = [EventApiHandlerTests.``AddEvent - Raise an event returns 200 where An event has been successfully created.``() :> Task;
                // EventApiHandlerTests.``ListEvents - List all received events returns 200 where successful operation``() :> Task;
                // EventApiHandlerTests.``GetEventById - Find raised event by ID returns 200 where successful operation``() :> Task;
                // EventApiHandlerTests.``AddEvent - Raise an event returns 405 where Invalid input``() :> Task;
                // ListenerApiHandlerTests.``AddListener - Create a new Listener returns 405 where Invalid input``() :> Task;
                // ListenerApiHandlerTests.``AddListener - Create a new Listener returns 200 where successful operation``() :> Task;
                // ListenerApiHandlerTests.``DeleteListener - Deletes a Listener returns 404 where Listener not found``() :> Task;
                // ListenerApiHandlerTests.``DeleteListener - Deletes a Listener returns 200 where Successfully deleted``() :> Task;
                // ListenerApiHandlerTests.``GetListenerById - Find Listener by ID returns 200 where successful operation``() :> Task;
                // ListenerApiHandlerTests.``GetListenerById - Find Listener by ID returns 404 where Listener not found``() :> Task
                MessageApiHandlerTests.``GetMessageById - Find message by ID returns 200 where successful operation``() :> Task;
                MessageApiHandlerTests.``GetMessageById - Find message by ID returns 404 where message not found``() :> Task;
                MessageApiHandlerTests.``ListMessages - List all messages returns 200 where successful operation``() :> Task
                ]
                |> List.toArray
    Task.WaitAll(tasks)
    0