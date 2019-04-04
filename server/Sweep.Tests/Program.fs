namespace Sweep.Tests

open System.Threading.Tasks

// F#
module Program =
  [<EntryPoint>]
  let main(args: string[]) = 
    let tasks = [
      // EventApiHandlerTests.``AddEvent - Raise an event returns 200 where An event has been successfully created.``() :> Task;
                // EventApiHandlerTests.``ListEvents - List all received events returns 200 where successful operation``() :> Task;
                // EventApiHandlerTests.``GetEventById - Find raised event by ID returns 200 where successful operation``() :> Task;
                // EventApiHandlerTests.``AddEvent - Raise an event returns 405 where Invalid input``() :> Task;
                // ListenerApiHandlerTests.``AddListener - Create a new Listener returns 405 where Invalid input``() :> Task;
                // ListenerApiHandlerTests.``AddListener - Create a new Listener returns 200 where successful operation``() :> Task;
                // ListenerApiHandlerTests.``DeleteListener - Deletes a Listener returns 404 where Listener not found``() :> Task;
                // ListenerApiHandlerTests.``DeleteListener - Deletes a Listener returns 200 where Successfully deleted``() :> Task;
                // ListenerApiHandlerTests.``GetListenerById - Find Listener by ID returns 200 where successful operation``() :> Task;
                // ListenerApiHandlerTests.``GetListenerById - Find Listener by ID returns 404 where Listener not found``() :> Task
                ListenerApiHandlerTests.``ListListenerTemplates - List Templates for Listener returns 200 where successful operation``() :> Task;
                ListenerApiHandlerTests.``ListListenerTemplates - List Templates for Listener returns 404 where Listener not found``() :> Task;
                ListenerApiHandlerTests.``DeleteListenerTemplate - Disassociates a Template from a Listener returns 200 where Successfully deleted``() :> Task;
                ListenerApiHandlerTests.``DeleteListenerTemplate - Disassociates a Template from a Listener returns 404 where Listener not found``() :> Task;
                ListenerApiHandlerTests.``AddListenerTemplate - Associates a Template to a Listener returns 200 where Successfully associated``() :> Task;
                ListenerApiHandlerTests.``AddListenerTemplate - Associates a Template to a Listener returns 404 where Listener or Template not found``() :> Task;
                // MessageApiHandlerTests.``GetMessageById - Find message by ID returns 200 where successful operation``() :> Task;
                // MessageApiHandlerTests.``GetMessageById - Find message by ID returns 404 where message not found``() :> Task;
                // MessageApiHandlerTests.``ListMessages - List all messages returns 200 where successful operation``() :> Task
                //TemplateApiHandlerTests.``AddTemplate - Create a new Template returns 200 where Success``() :> Task
                // TemplateApiHandlerTests.``DeleteTemplate - Deletes a Template returns 200 where Successful deletion``() :> Task;
                //TemplateApiHandlerTests.``DeleteTemplate - Deletes a Template returns 404 where Template not found``() :> Task;
                //TemplateApiHandlerTests.``GetTemplateById - Find Template by ID returns 200 where successful operation``() :> Task;
                //TemplateApiHandlerTests.``GetTemplateById - Find Template by ID returns 404 where Listener not found``() :> Task;
                //TemplateApiHandlerTests.``ListTemplate - List all Templates returns 200 where successful operation``() :> Task
                //TemplateApiHandlerTests.``UpdateTemplate - Update an existing Template returns 404 where Template not found``() :> Task;
                //TemplateApiHandlerTests.``UpdateTemplate - Update an existing Template returns 200 where Successfully updated``() :> Task;
                //TemplateApiHandlerTests.``UpdateTemplate - Update an existing Template returns 422 where Validation exception``() :> Task;
                ]
                |> List.toArray
    Task.WaitAll(tasks)
    0