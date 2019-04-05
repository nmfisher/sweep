namespace Sweep.Model

open System
open System.Collections.Generic

module Message = 

  //#region Message


  type Message = {
    Id : string;
    Content : string;
    SentTo : string[];
    UserId : string;
    OrganizationId : string;
  }
  //#endregion
  