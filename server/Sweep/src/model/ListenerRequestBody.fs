namespace Sweep.Model

open System
open System.Collections.Generic

module ListenerRequestBody = 

  //#region ListenerRequestBody


  type ListenerRequestBody = {
    EventName : string ;
    Trigger : string option;
  }
  //#endregion
  