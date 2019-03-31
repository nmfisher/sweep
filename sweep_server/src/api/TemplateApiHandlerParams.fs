namespace Sweep

open TemplateModel
open System.Collections.Generic

module TemplateApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddTemplateBodyParams = {
      template : Template
    }
    //#endregion

    
    type AddTemplateStatusCode405Response = {
      content:string;
      
    }
    type AddTemplateResult = AddTemplateStatusCode405 of AddTemplateStatusCode405Response

    type AddTemplateArgs = {
      bodyParams:AddTemplateBodyParams
    }
    //#region Path parameters
    [<CLIMutable>]
    type DeleteTemplatePathParams = {
      templateId : int64
    }
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type DeleteTemplateHeaderParams = {
      apiKey : Option<string>
    }
    //#endregion

    
    type DeleteTemplateStatusCode400Response = {
      content:string;
      
    }
    
    type DeleteTemplateStatusCode404Response = {
      content:string;
      
    }
    type DeleteTemplateResult = DeleteTemplateStatusCode400 of DeleteTemplateStatusCode400Response|DeleteTemplateStatusCode404 of DeleteTemplateStatusCode404Response

    type DeleteTemplateArgs = {
      pathParams:DeleteTemplatePathParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type GetTemplateByIdPathParams = {
      templateId : int64
    }
    //#endregion

    
    type GetTemplateByIdDefaultStatusCodeResponse = {
      content:Template;
      
    }
    
    type GetTemplateByIdStatusCode400Response = {
      content:string;
      
    }
    
    type GetTemplateByIdStatusCode404Response = {
      content:string;
      
    }
    type GetTemplateByIdResult = GetTemplateByIdDefaultStatusCode of GetTemplateByIdDefaultStatusCodeResponse|GetTemplateByIdStatusCode400 of GetTemplateByIdStatusCode400Response|GetTemplateByIdStatusCode404 of GetTemplateByIdStatusCode404Response

    type GetTemplateByIdArgs = {
      pathParams:GetTemplateByIdPathParams;
    }

    
    type ListTemplateDefaultStatusCodeResponse = {
      content:Template;
      
    }
    type ListTemplateResult = ListTemplateDefaultStatusCode of ListTemplateDefaultStatusCodeResponse


    //#region Body parameters
    [<CLIMutable>]
    type UpdateTemplateBodyParams = {
      template : Template
    }
    //#endregion

    
    type UpdateTemplateStatusCode400Response = {
      content:string;
      
    }
    
    type UpdateTemplateStatusCode404Response = {
      content:string;
      
    }
    
    type UpdateTemplateStatusCode405Response = {
      content:string;
      
    }
    type UpdateTemplateResult = UpdateTemplateStatusCode400 of UpdateTemplateStatusCode400Response|UpdateTemplateStatusCode404 of UpdateTemplateStatusCode404Response|UpdateTemplateStatusCode405 of UpdateTemplateStatusCode405Response

    type UpdateTemplateArgs = {
      bodyParams:UpdateTemplateBodyParams
    }
    