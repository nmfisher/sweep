namespace Sweep

open Sweep.Model.Message
open Sweep.Model.RenderTemplateRequestBody
open Sweep.Model.Template
open Sweep.Model.TemplateRequestBody
open System.Collections.Generic
open System

module TemplateApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddTemplateBodyParams = TemplateRequestBody 
    //#endregion

    
    type AddTemplateDefaultStatusCodeResponse = {
      content:Template;
      
    }
    
    type AddTemplateStatusCode422Response = {
      content:string;
      
    }
    type AddTemplateResult = AddTemplateDefaultStatusCode of AddTemplateDefaultStatusCodeResponse|AddTemplateStatusCode422 of AddTemplateStatusCode422Response

    type AddTemplateArgs = {
      bodyParams:AddTemplateBodyParams
    }
    //#region Path parameters
    [<CLIMutable>]
    type DeleteTemplatePathParams = {
      templateId : string ;
    }
    //#endregion

    
    type DeleteTemplateDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type DeleteTemplateStatusCode404Response = {
      content:string;
      
    }
    type DeleteTemplateResult = DeleteTemplateDefaultStatusCode of DeleteTemplateDefaultStatusCodeResponse|DeleteTemplateStatusCode404 of DeleteTemplateStatusCode404Response

    type DeleteTemplateArgs = {
      pathParams:DeleteTemplatePathParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type GetTemplateByIdPathParams = {
      templateId : string ;
    }
    //#endregion

    
    type GetTemplateByIdDefaultStatusCodeResponse = {
      content:Template;
      
    }
    
    type GetTemplateByIdStatusCode404Response = {
      content:string;
      
    }
    type GetTemplateByIdResult = GetTemplateByIdDefaultStatusCode of GetTemplateByIdDefaultStatusCodeResponse|GetTemplateByIdStatusCode404 of GetTemplateByIdStatusCode404Response

    type GetTemplateByIdArgs = {
      pathParams:GetTemplateByIdPathParams;
    }

    
    type ListTemplateDefaultStatusCodeResponse = {
      content:Template[];
      
    }
    type ListTemplateResult = ListTemplateDefaultStatusCode of ListTemplateDefaultStatusCodeResponse

    //#region Path parameters
    [<CLIMutable>]
    type RenderTemplatePathParams = {
      templateId : string ;
    }
    //#endregion

    //#region Body parameters
    [<CLIMutable>]
    type RenderTemplateBodyParams = RenderTemplateRequestBody 
    //#endregion

    
    type RenderTemplateDefaultStatusCodeResponse = {
      content:Message;
      
    }
    
    type RenderTemplateStatusCode422Response = {
      content:string;
      
    }
    
    type RenderTemplateStatusCode404Response = {
      content:string;
      
    }
    type RenderTemplateResult = RenderTemplateDefaultStatusCode of RenderTemplateDefaultStatusCodeResponse|RenderTemplateStatusCode422 of RenderTemplateStatusCode422Response|RenderTemplateStatusCode404 of RenderTemplateStatusCode404Response

    type RenderTemplateArgs = {
      pathParams:RenderTemplatePathParams;
      bodyParams:RenderTemplateBodyParams
    }
    //#region Path parameters
    [<CLIMutable>]
    type UpdateTemplatePathParams = {
      templateId : string ;
    }
    //#endregion

    //#region Body parameters
    [<CLIMutable>]
    type UpdateTemplateBodyParams = TemplateRequestBody 
    //#endregion

    
    type UpdateTemplateDefaultStatusCodeResponse = {
      content:Template;
      
    }
    
    type UpdateTemplateStatusCode404Response = {
      content:string;
      
    }
    
    type UpdateTemplateStatusCode422Response = {
      content:string;
      
    }
    type UpdateTemplateResult = UpdateTemplateDefaultStatusCode of UpdateTemplateDefaultStatusCodeResponse|UpdateTemplateStatusCode404 of UpdateTemplateStatusCode404Response|UpdateTemplateStatusCode422 of UpdateTemplateStatusCode422Response

    type UpdateTemplateArgs = {
      pathParams:UpdateTemplatePathParams;
      bodyParams:UpdateTemplateBodyParams
    }
    