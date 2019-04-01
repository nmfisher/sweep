namespace Sweep
open System

module MessageModel = 

  //#region Message


  type Message = {
    Id : string;
    Content : string;
    To : string[];
    UserId : string;
  }
  //#endregion
  