namespace Sweep
open System
open System.Collections.Generic

module MessageModel = 

  //#region Message


  type Message = {
    Id : string;
    Content : string;
    SentTo : string[];
    UserId : string;
    OrganizationId : string;
  }
  //#endregion
  