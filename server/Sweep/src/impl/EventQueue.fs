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
    event.ReceivedOn.Add(condition.Duration) >= DateTime.Now

  let isMetBy (events: seq<Event>) (parent:Event) (condition:ListenerCondition)  =
    events 
    |> Seq.map (fun x ->
      let matching = x.ReceivedOn.Subtract(condition.Duration) >= parent.ReceivedOn && x.EventName = condition.EventName
      match condition.Key with
        | None -> matching
        | Some key -> matching && (x.Params.[key].Equals(parent.Params.[key])))
    |> Seq.isEmpty
    |> not

  let parseCondition listener =
    match listener.Condition with 
      | null -> None
      | _ -> match Listener.parse listener.Condition with
                | None -> raise (Exception("Invalid condition"))
                | Some parse -> Some(parse)

  let handle (events:seq<Event>) mailer (listenerAction:Sweep.Model.ListenerAction.ListenerAction) =
    let listener = Listener.get listenerAction.ListenerId listenerAction.OrganizationId : Listener
    let templates = CompositionRoot.listTemplatesForListener listener.Id listener.OrganizationId
    let parent = events |> Seq.where (fun x -> x.Id = listenerAction.EventId) |> Seq.head : Event

    match parseCondition listener with
    | None ->
        try 
          let event = events |> Seq.where (fun x -> x.Id = listenerAction.EventId) |> Seq.head
          mailer event templates
          ListenerAction.markAsComplete listenerAction.Id None
        with 
        | e -> 
          ListenerAction.markAsComplete listenerAction.Id (Some(e.ToString()))
        
    | Some condition ->
        if (condition |> isMetBy events parent) then 
          let event = events |> Seq.where (fun x -> x.Id = listenerAction.EventId) |> Seq.head
          mailer event templates

        if (condition |> noLongerApplies parent) then
          ListenerAction.markAsComplete listenerAction.Id None


  let initialize delay apiKey defaultFromAddress defaultFromName defaultSubject = 
    let timer = new Timers.Timer(60000.)
    printfn "%A" DateTime.Now
    timer.Start()
    printfn "%A" "A-OK"
    let mailer = 
      MailHandler.handle 
        (SendGridClient(apiKey)) 
        { 
          FromAddress=defaultFromAddress;
          FromName=defaultFromName;
          Subject=defaultSubject;
        }
    
    while true do
      Async.AwaitEvent (timer.Elapsed) |> ignore
      // dequeue all events and create ListenerActions for all listeners that match
      Event.listAllUnprocessed() |> Seq.map ListenerAction.createFromEvent |> Seq.map Event.markAsProcessed |> ignore

      // dequeue all incomplete listenerActions
      let incomplete = ListenerAction.listIncomplete() 
      // get all events since the first action
      let events = incomplete |> Seq.head |> (fun x -> Event.listAllAfter x.EventId)
      incomplete |> Seq.map (handle events mailer) |> ignore
      printfn "%A" DateTime.Now