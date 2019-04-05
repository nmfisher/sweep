namespace Sweep.Model

open System
open System.Collections.Generic

module Template = 

  //#region Template


  type Template = {
    Id : string;
    Content : string;
    SendTo : string[];
    UserId : string;
    OrganizationId : string;
    Deleted : bool;
  }
  //#endregion
  