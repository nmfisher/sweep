namespace Sweep
open OrganizationApiHandlerParams
open System
open Giraffe
open Microsoft.AspNetCore.Http


module OrganizationApiServiceInterface =
    
    //#region Service interface
    type IOrganizationApiService = 
      abstract member GetOrganizationInfo:HttpContext ->GetOrganizationInfoResult
    //#endregion