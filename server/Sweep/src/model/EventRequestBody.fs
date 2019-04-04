namespace Sweep
open System

module EventRequestBodyModel = 

  //#region EventRequestBody


  type EventRequestBody = {
    EventName : string;
    Params : obj[];
  }
  //#endregion
  