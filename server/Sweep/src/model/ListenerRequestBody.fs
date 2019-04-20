namespace Sweep.Model

open System
open System.Collections.Generic

module ListenerRequestBody = 

  //#region ListenerRequestBody


  type ListenerRequestBody = {
    EventName : string;
    TriggerEvent : string;
    TriggerNumber : decimal;
    TriggerPeriod : string;
    TriggerMatch : string;
    EventParams : string[];
  }
  //#endregion
  