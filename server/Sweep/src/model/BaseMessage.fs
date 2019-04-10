namespace Sweep.Model

open System
open System.Collections.Generic

module BaseMessage = 

  //#region BaseMessage


  type BaseMessage = {
    Id : string;
    Content : string;
    Subject : string;
    FromAddress : string;
    FromName : string;
    SendTo : string[];
    OrganizationId : string;
  }
  //#endregion
  