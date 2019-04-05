namespace Sweep.Model

open System
open System.Collections.Generic

module Listener = 

  //#region Listener


  type Listener = {
    Id : string;
    EventName : string;
    UserId : string;
    OrganizationId : string;
    Deleted : bool;
  }
  //#endregion
  