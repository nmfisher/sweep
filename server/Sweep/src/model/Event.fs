namespace Sweep.Model

open System
open System.Collections.Generic
open System.Collections.Generic

module Event = 

  //#region Event


  type Event = {
    Id : string;
    EventName : string;
    Params : IDictionary<string, obj>;
    ReceivedOn : DateTime;
    ProcessedOn : Nullable<DateTime>;
    Error : string;
    OrganizationId : string;
  }
  //#endregion
  