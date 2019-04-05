namespace Sweep.Model

open System
open System.Collections.Generic

module Listener = 

  //#region Listener


  type Listener = {
    Id : string;
    TemplateId : string;
    EventName : string;
    OrganizationId : string;
  }
  //#endregion
  