namespace Sweep.Tests

open System.Threading.Tasks
open Sweep

// F#
module Program =
  [<EntryPoint>]
  let main(args: string[]) = 
    let tasks = [
      // EventApiHandlerTests.``AddEvent - Raise an event returns 200 where An event has been successfully created.``() :> Task;
                // EventApiHandlerTests.``ListEvents - List all received events returns 200 where successful operation``() :> Task;
                // EventApiHandlerTests.``GetEventById - Find raised event by ID returns 200 where successful operation``() :> Task;
                // EventApiHandlerTests.``AddEvent - Raise an event returns 422 where Invalid input``() :> Task;
                // ListenerApiHandlerTests.``AddListener - Create a new Listener returns 200 where successful operation``() :> Task;
                // ListenerApiHandlerTests.``AddListener - Create a new Listener returns 422 where Invalid input``() :> Task;
                //ListenerApiHandlerTests.``ListListeners - List all Listeners returns 200 where successful operation``() :> Task;
                ListenerApiHandlerTests.``UpdateListener - Updates a Listener returns 200 where Successfully updated``() :> Task;
                // MessageApiHandlerTests.``GetMessageById - Find message by ID returns 200 where successful operation``() :> Task;
                // MessageApiHandlerTests.``GetMessageById - Find message by ID returns 404 where message not found``() :> Task;
                // MessageApiHandlerTests.``ListMessages - List all messages returns 200 where successful operation``() :> Task
                //TemplateApiHandlerTests.``AddTemplate - Create a new Template returns 200 where Success``() :> Task
                // TemplateApiHandlerTests.``DeleteTemplate - Deletes a Template returns 200 where Successful deletion``() :> Task;
                //TemplateApiHandlerTests.``DeleteTemplate - Deletes a Template returns 404 where Template not found``() :> Task;
                //TemplateApiHandlerTests.``GetTemplateById - Find Template by ID returns 200 where successful operation``() :> Task;
                //TemplateApiHandlerTests.``GetTemplateById - Find Template by ID returns 404 where Listener not found``() :> Task;
                // TemplateApiHandlerTests.``ListTemplate - List all Templates returns 200 where successful operation``() :> Task
                //TemplateApiHandlerTests.``UpdateTemplate - Update an existing Template returns 404 where Template not found``() :> Task;
                //TemplateApiHandlerTests.``UpdateTemplate - Update an existing Template returns 200 where Successfully updated``() :> Task;
                //TemplateApiHandlerTests.``UpdateTemplate - Update an existing Template returns 422 where Validation exception``() :> Task;

                // MailHandlerTests.``Send test email``() :> Task
                //ListenerTests.``Validate Listener Condition``() :> Task
                //EventQueueTests.``Listener condition expires when duration exceeds time elapsed since original event``() :> Task
                // EventApiHandlerTests.``ListEvents - List all received events returns 200 where successful operation``() :> Task
                // EventApiHandlerTests.``List with start and end dates``() :> Task
                // EventApiHandlerTests.``GetEventById - Find raised event by ID returns 200 where successful operation``() :> Task
                // EventQueueTests.``Listener condition is met by events matching event name and key within duration ``() :> Task
                //EventQueueTests.``sendConditional only invokes mailer if trigger is matched``() :> Task
                // EventQueueTests.``Render default subject``() :> Task;
                // EventQueueTests.``Dequeue and send mail for an event with no trigger``() :> Task
                // EventQueueTests.``Dequeue and send mail for an event with a matched trigger``() :> Task
                // EventQueueTests.``Dequeue and mark event as completed with an expired trigger``() :> Task
                //  ListenerApiHandlerTests.``ListListeners - List all Listeners returns 200 where successful operation``() :> Task
                //EventApiHandlerTests.``ListIncomplete lists events where ProcessedOn is null``() :> Task
                // ListenerApiHandlerTests.``createFromEvent successfully creates ListenerAction where a Listener exists for an event name and an organization id``() :> Task
                // ListenerTemplateApiHandlerTests.``ListListenerTemplates - List Templates for Listener returns 200 where successful operation``() :> Task
                // ListenerTemplateApiHandlerTests.``listTemplatesForListener returns list of templates`` () :> Task
                // EventApiHandlerTests.``List all events after the ReceivedOn date of the provided event``() :> Task
                //TemplateApiHandlerTests.``RenderTemplate - Renders a template using the provided event parameters returns 200 where successful operation``() :> Task
                //TemplateApiHandlerTests.``GetTemplateById - Find Template by ID returns 404 where Listener not found``() :> Task
                //TemplateApiHandlerTests.``RenderTemplate - Renders a template using the provided event parameters returns 422 where Template could not be rendered``() :> Task
                //MailHandlerTests.``Send test email``() :> Task
                //ListenerApiHandlerTests.``ListListeners - List all Listeners returns 200 where successful operation`` ()  :> Task
                  // ListenerApiHandlerTests.``ListMessagesForAction - List all messages returns 200 where successful operation`` () :> Task
                // OrganizationApiHandlerTests.``GetOrganizationInfo - Get organization info for the currently authenticated context returns 200 where successful operation`` () :> Task
                // CustomHandlerTests.``fetchOrCreateUser successfully creates and returns user where one does not exist``() :> Task
                ]
                |> List.toArray
    Task.WaitAll(tasks)
    0