namespace Sweep

open System.Collections.Generic
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open OrganizationApiHandlerParams
open OrganizationApiServiceInterface
open OrganizationApiServiceImplementation
open Sweep.Model.Organization

module OrganizationApiHandler = 

    /// <summary>
    /// 
    /// </summary>

    //#region GetOrganizationInfo
    /// <summary>
    /// Get organization info for the currently authenticated context
    /// </summary>

    let GetOrganizationInfo  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let result = OrganizationApiService.GetOrganizationInfo ctx 
          return! (match result with 
                      | GetOrganizationInfoDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
          ) next ctx
        }
    //#endregion


    
