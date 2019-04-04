namespace Sweep
open System
open System.Collections.Generic

module EventModel = 

  //#region Event


  type Event = {
    Id : string;
    EventName : string;
    Params : IDictionary<string, obj>;
    ReceivedOn : DateTime;
    ProcessedOn : DateTime;
    Error : string;
    OrganizationId : string;
  }
  //#endregion
  