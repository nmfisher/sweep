namespace Sweep
open System

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
  