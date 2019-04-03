namespace Sweep
open System

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
  