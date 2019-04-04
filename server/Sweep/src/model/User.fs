namespace Sweep
open System
open System.Collections.Generic

module UserModel = 

  //#region User


  type User = {
    Id : string;
    Username : string;
    Password : string;
    ApiKey : string;
    OrganizationId : string;
  }
  //#endregion
  