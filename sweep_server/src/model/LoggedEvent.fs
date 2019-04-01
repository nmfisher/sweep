namespace Sweep
open System

module LoggedEventModel = 

  //#region LoggedEvent


  type LoggedEvent = {
    Id : string;
    EventName : string;
    Params : obj;
    UserId : string;
  }
  //#endregion
  