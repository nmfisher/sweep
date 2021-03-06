namespace Sweep.Model

open System
open System.Collections.Generic

module Listener = 

  //#region Listener


  type Listener = {
    Id : string;
    EventName : string;
    EventParams : string[];
    OrganizationId : string;
    TriggerEvent : string;
    TriggerNumber : int;
    TriggerPeriod : string;
    TriggerMatch : string;
  }
  //#endregion
  