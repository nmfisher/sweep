namespace Sweep

open Sweep.Model.Organization
open System.Collections.Generic
open System

module OrganizationApiHandlerParams = 


    
    type GetOrganizationInfoDefaultStatusCodeResponse = {
      content:Organization;
      
    }
    type GetOrganizationInfoResult = GetOrganizationInfoDefaultStatusCode of GetOrganizationInfoDefaultStatusCodeResponse

    