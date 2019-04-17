namespace Sweep.Model

open System
open System.Collections.Generic
open Sweep.Model.BaseMessage

module Message = 

  //#region Message


  type Message = {
    Id : string;
    Content : string;
    Subject : string;
    FromAddress : string;
    FromName : string;
    SendTo : string[];
    OrganizationId : string;
    ListenerActionId : string;
    SentOn : Nullable<DateTime>;
  }
  //#endregion
  