namespace Sweep

open Microsoft.Extensions.Configuration
open System.Collections.Generic
open Sweep.Data
open Sweep.Model.Template
open Sweep.Model.Listener
open Sweep.Model.ListenerAction
open System
open FSharp.Data.Sql
open System.Net
open Sweep.Model.Message
open SendGrid
open Sweep.Data.Listener


module EventQueue = 

  let noLongerApplies (event:Event) (condition:ListenerCondition) =
    event.ReceivedOn.Add(condition.Duration) <= DateTime.Now

  let isMetBy (events: seq<Event>) (parent:Event) (condition:ListenerCondition)  =
    events 
    |> Seq.where (fun x ->
      let matchDuration = x.ReceivedOn.Subtract(parent.ReceivedOn) <= condition.Duration
      let matching =  matchDuration && (x.EventName = condition.EventName)
      match condition.Key with
        | None -> matching
        | Some key -> matching && (x.Params.[key].Equals(parent.Params.[key])))
    |> Seq.isEmpty
    |> not

  let findEvent (events:seq<Event>) eventId = 
    events |> Seq.where (fun x -> x.Id = eventId) |> Seq.head
  
  let fetchTemplates (mailer:seq<Template> -> Event -> unit) listenerAction  =
    let templates = CompositionRoot.listTemplatesForListener listenerAction.ListenerId listenerAction.OrganizationId
    mailer templates, listenerAction

  let fetchInvokingEvent events (mailer:Event -> unit, listenerAction) =
    let invokingEvent = findEvent events listenerAction.EventId
    (fun () -> mailer invokingEvent), listenerAction, invokingEvent

  let sendUnconditional mailer success failure = 
    try 
      mailer()
      success()
    with 
    | e -> 
      failure e

  let sendConditional triggerMatcher expiryMatcher trigger mailer success failure  =
    if (trigger |> triggerMatcher) then 
      mailer()

    if (trigger |> expiryMatcher) then
      success()

  let getSender listenerAction triggerMatcher expiryMatcher = 
    let listener = Listener.get listenerAction.ListenerId listenerAction.OrganizationId
    match Listener.parse listener.Trigger with 
    | None ->
      sendUnconditional
    | Some triggerString ->
      sendConditional triggerMatcher expiryMatcher triggerString

  let onSuccess (listenerAction:ListenerAction) =
    ListenerAction.markAsComplete listenerAction.Id None

  let onFailure (listenerAction:ListenerAction) e =
      ListenerAction.markAsComplete listenerAction.Id (Some(e.ToString()))

  let dequeue mailer = 
    // dequeue all events and create ListenerActions for all listeners that match
    let events = Event.listAllUnprocessed() |> Seq.toList
    events |> Seq.map ListenerAction.createFromEvent |> Seq.toList 
    events |> Seq.map Event.markAsProcessed |> Seq.toList |> ignore

    // dequeue all incomplete listenerActions
    let incomplete = ListenerAction.listIncomplete() 
    match Seq.isEmpty incomplete with 
    | true ->
      ()
    | false ->    
      // get all events since the first action
      let events = incomplete |> Seq.head |> (fun x -> Event.listAllAfter x.EventId) 

      // fetch all templates associated with the incomplete ListenerActions
      incomplete 
      |> Seq.map (fetchTemplates mailer 
                  // extract the original event that created the ListenerAction
                  >> fetchInvokingEvent events 
                  >> (fun (mailer, listenerAction, event) -> 
                    // get a sender function that depends on whether the ListenerCondition needs checking 
                    let sender = getSender listenerAction (isMetBy events event) (noLongerApplies event)
                    // invoke the sender function, with success/error handlers to update the status of the ListenerAction
                    sender mailer (fun () -> onSuccess listenerAction) (onFailure listenerAction)))
      |> Seq.toList                
      |> ignore

  let initialize (apiKey:string) defaults = 
      let timer = new Timers.Timer(10000.)
      timer.AutoReset <- true
      let client = SendGridClient(apiKey)
      let mailer = MailHandler.handle client defaults (Sweep.Data.Message.save)
      
      timer.Elapsed.Add (fun _ -> dequeue mailer)
      timer.Start()

    