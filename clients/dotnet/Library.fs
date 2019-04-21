module Sweep

open System
open Sweep.Api
open Sweep.Model

let eventApi = EventApi()
let mutable SWEEP_API_KEY:string = null
let SetApiKey key = 
  SWEEP_API_KEY <- key

let RaiseEvent eventName eventParams = 
  match isNull SWEEP_API_KEY with
  | true ->
    raise (Exception("API key not set"))
  | false ->
    eventApi.AddEvent (EventRequestBody(eventName, eventParams))


