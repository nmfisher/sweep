namespace Sweep
open System
open System.Collections.Generic

module ListenerModel = 

  //#region Listener


  type Listener = {
    Id : string;
    EventName : string;
    UserId : string;
    OrganizationId : string;
    Deleted : bool;
  }
  //#endregion
  