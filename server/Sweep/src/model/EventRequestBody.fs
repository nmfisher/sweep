namespace Sweep.Model

open System
open System.Collections.Generic
open System.Collections.Generic

module EventRequestBody = 

  //#region EventRequestBody


  type EventRequestBody = {
    EventName : string ;
    Params : IDictionary<string, obj> option;
  }
  //#endregion
  