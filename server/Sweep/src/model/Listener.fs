namespace Sweep.Model

open System
open System.Collections.Generic

module Listener = 

  //#region Listener


  type Listener = {
    Id : string;
    EventName : string;
    OrganizationId : string;
    Condition : string;
  }
  //#endregion
  