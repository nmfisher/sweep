namespace Sweep.Model

open System
open System.Collections.Generic
open Sweep.Model.BaseMessage

module Template = 

  //#region Template


  type Template = {
    Id : string;
    Content : string;
    Subject : string;
    FromAddress : string;
    FromName : string;
    SendTo : string[];
    OrganizationId : string;
    Deleted : bool;
    UserId : string;
  }
  //#endregion
  