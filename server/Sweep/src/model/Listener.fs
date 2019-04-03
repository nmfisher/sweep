namespace Sweep
open System

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
  