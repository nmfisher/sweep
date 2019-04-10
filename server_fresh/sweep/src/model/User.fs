namespace Sweep.Model

open System
open System.Collections.Generic

module User = 

  //#region User


  type User = {
    Id : string;
    Username : string option;
    Password : string option;
    ApiKey : string option;
    OrganizationId : string;
  }
  //#endregion
  