namespace Sweep.Model

open System
open System.Collections.Generic

module Listener = 

  //#region Listener


  type Listener = {
    Id : string;
    EventName : string;
    EventParams : string[] option;
    OrganizationId : string;
    Trigger : string option;
  }
  //#endregion
  