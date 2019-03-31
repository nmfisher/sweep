namespace Sweep
open System

module MessageModel = 

  //#region Message



  type Message = {
    Id : int64;
    Content : string;
    To : string[];
    UserId : string;
  }
  //#endregion
  