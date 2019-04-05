namespace Sweep

open Sweep.Model.Template
open System.Collections.Generic

module TemplateApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddTemplateBodyParams = Template
    //#endregion

    
    type AddTemplateDefaultStatusCodeResponse = {
      content:string;
      
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
      templateId : string;
    }
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type DeleteTemplateHeaderParams = {
      apiKey : Option<string>;
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
      templateId : string;
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
    type UpdateTemplatePathParams = {
      templateId : string;
    }
    //#endregion

    //#region Body parameters
    [<CLIMutable>]
    type UpdateTemplateBodyParams = Template
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
      pathParams:UpdateTemplatePathParams;
      bodyParams:UpdateTemplateBodyParams
    }
    