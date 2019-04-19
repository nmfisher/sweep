namespace Sweep.Model

open System
open System.Collections.Generic

module Organization = 

  //#region Organization


  type Organization = {
    Id : string;
    PrimaryApiKey : string;
    SecondaryApiKey : string;
  }
  //#endregion
  