namespace Sweep
open System

module LoggedEventModel = 

  //#region LoggedEvent



  type LoggedEvent = {
    Id : int64;
    EventName : string;
    Params : obj;
    UserId : string;
  }
  //#endregion
  