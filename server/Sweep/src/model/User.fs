namespace Sweep.Model

open System
open System.Collections.Generic

module User = 

  //#region User


  type User = {
    Id : string;
    Username : string;
    Password : string;
    ApiKey : string;
    OrganizationId : string;
  }
  //#endregion
  