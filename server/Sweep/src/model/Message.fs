namespace Sweep
open System
open IdModel

module MessageModel = 

  //#region Message


  type Message = {
    Id : string;
    Content : string;
    To : string[];
    UserId : Id;
    OrganizationId : string;
  }
  //#endregion
  