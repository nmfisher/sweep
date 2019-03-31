namespace Sweep
open System

module EventModel = 

  //#region Event



  type Event = {
    EventName : string;
    Params : obj[];
    UserId : string;
  }
  //#endregion
  