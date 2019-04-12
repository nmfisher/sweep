namespace Sweep

open Sweep.Model.Message
open Sweep.Model.RenderTemplateRequestBody
open Sweep.Model.Template
open Sweep.Model.TemplateRequestBody
open System.Collections.Generic

module TemplateApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddTemplateBodyParams = TemplateRequestBody
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type AddTemplateHeaderParams = {
      apiKey : string option;
    }
    //#endregion

    
    type AddTemplateDefaultStatusCodeResponse = {
      content:Template;
      
    }
    
    type AddTemplateStatusCode422Response = {
      content:string;
      
    }
    type AddTemplateResult = AddTemplateDefaultStatusCode of AddTemplateDefaultStatusCodeResponse|AddTemplateStatusCode422 of AddTemplateStatusCode422Response

    type AddTemplateArgs = {
      headerParams:AddTemplateHeaderParams;
      bodyParams:AddTemplateBodyParams
    }
    //#region Path parameters
    [<CLIMutable>]
    type DeleteTemplatePathParams = {
      templateId : string;
    }
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type DeleteTemplateHeaderParams = {
      apiKey : string option;
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
      headerParams:DeleteTemplateHeaderParams;
      pathParams:DeleteTemplatePathParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type GetTemplateByIdPathParams = {
      templateId : string;
    }
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type GetTemplateByIdHeaderParams = {
      apiKey : string option;
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
      headerParams:GetTemplateByIdHeaderParams;
      pathParams:GetTemplateByIdPathParams;
    }

    //#region Header parameters
    [<CLIMutable>]
    type ListTemplateHeaderParams = {
      apiKey : string option;
    }
    //#endregion

    
    type ListTemplateDefaultStatusCodeResponse = {
      content:Template[];
      
    }
    type ListTemplateResult = ListTemplateDefaultStatusCode of ListTemplateDefaultStatusCodeResponse

    type ListTemplateArgs = {
      headerParams:ListTemplateHeaderParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type RenderTemplatePathParams = {
      templateId : string;
    }
    //#endregion

    //#region Body parameters
    [<CLIMutable>]
    type RenderTemplateBodyParams = RenderTemplateRequestBody
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type RenderTemplateHeaderParams = {
      apiKey : string option;
    }
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
      headerParams:RenderTemplateHeaderParams;
      pathParams:RenderTemplatePathParams;
      bodyParams:RenderTemplateBodyParams
    }
    //#region Path parameters
    [<CLIMutable>]
    type UpdateTemplatePathParams = {
      templateId : string;
    }
    //#endregion

    //#region Body parameters
    [<CLIMutable>]
    type UpdateTemplateBodyParams = TemplateRequestBody
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type UpdateTemplateHeaderParams = {
      apiKey : string option;
    }
    //#endregion

    
    type UpdateTemplateDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type UpdateTemplateStatusCode404Response = {
      content:string;
      
    }
    
    type UpdateTemplateStatusCode422Response = {
      content:string;
      
    }
    type UpdateTemplateResult = UpdateTemplateDefaultStatusCode of UpdateTemplateDefaultStatusCodeResponse|UpdateTemplateStatusCode404 of UpdateTemplateStatusCode404Response|UpdateTemplateStatusCode422 of UpdateTemplateStatusCode422Response

    type UpdateTemplateArgs = {
      headerParams:UpdateTemplateHeaderParams;
      pathParams:UpdateTemplatePathParams;
      bodyParams:UpdateTemplateBodyParams
    }
    