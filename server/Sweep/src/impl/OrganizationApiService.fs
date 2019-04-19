namespace Sweep
open Sweep.Model.Organization
open OrganizationApiHandlerParams
open OrganizationApiServiceInterface
open System.Collections.Generic
open System
open UserContext
open Giraffe

module OrganizationApiServiceImplementation =
    
    //#region Service implementation
    type OrganizationApiServiceImpl() = 
      interface IOrganizationApiService with
      
        member this.GetOrganizationInfo ctx  =
          let orgId = getOrgId ctx.User.Claims
          let org = CompositionRoot.getOrganization orgId
          GetOrganizationInfoDefaultStatusCode { content =  org }


      //#endregion

    let OrganizationApiService = OrganizationApiServiceImpl() :> IOrganizationApiService