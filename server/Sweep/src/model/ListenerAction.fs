namespace Sweep.Model

open System
open System.Collections.Generic

module ListenerAction = 

  //#region ListenerAction


  type ListenerAction = {
    Id : string ;
    EventId : string ;
    ListenerId : string ;
    OrganizationId : string ;
    Completed : bool ;
    Error : string option;
  }
  //#endregion
  