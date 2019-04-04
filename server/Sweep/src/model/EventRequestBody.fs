namespace Sweep
open System
open System.Collections.Generic

module EventRequestBodyModel = 

  //#region EventRequestBody


  type EventRequestBody = {
    EventName : string;
    Params : Dictionary<string, obj>;
  }
  //#endregion
  