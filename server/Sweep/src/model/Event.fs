namespace Sweep
open System

module EventModel = 

  //#region Event


  type Event = {
    Id : string;
    EventName : string;
    Params : obj;
    ReceivedOn : DateTime;
    ProcessedOn : DateTime;
    Error : string;
    OrganizationId : string;
  }
  //#endregion
  