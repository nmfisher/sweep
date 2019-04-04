namespace Sweep
open System
open System.Collections.Generic

module TemplateModel = 

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
  