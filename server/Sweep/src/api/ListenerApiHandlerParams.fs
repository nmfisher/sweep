namespace Sweep

open ListenerModel
open ListenerTemplateModel
open System.Collections.Generic

module ListenerApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddListenerBodyParams = Listener
    //#endregion

    
    type AddListenerDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type AddListenerStatusCode405Response = {
      content:string;
      
    }
    type AddListenerResult = AddListenerDefaultStatusCode of AddListenerDefaultStatusCodeResponse|AddListenerStatusCode405 of AddListenerStatusCode405Response

    type AddListenerArgs = {
      bodyParams:AddListenerBodyParams
    }
    //#region Path parameters
    [<CLIMutable>]
    type AddListenerTemplatePathParams = {
      listenerId : string;
    //#endregion
      templateId : string;
    }
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type AddListenerTemplateHeaderParams = {
      apiKey : Option<string>;
    }
    //#endregion

    
    type AddListenerTemplateDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type AddListenerTemplateStatusCode404Response = {
      content:string;
      
    }
    
    type AddListenerTemplateStatusCode500Response = {
      content:string;
      
    }
    type AddListenerTemplateResult = AddListenerTemplateDefaultStatusCode of AddListenerTemplateDefaultStatusCodeResponse|AddListenerTemplateStatusCode404 of AddListenerTemplateStatusCode404Response|AddListenerTemplateStatusCode500 of AddListenerTemplateStatusCode500Response

    type AddListenerTemplateArgs = {
      pathParams:AddListenerTemplatePathParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type DeleteListenerPathParams = {
      listenerId : string;
    }
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type DeleteListenerHeaderParams = {
      apiKey : Option<string>;
    }
    //#endregion

    
    type DeleteListenerDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type DeleteListenerStatusCode404Response = {
      content:string;
      
    }
    
    type DeleteListenerStatusCode500Response = {
      content:string;
      
    }
    type DeleteListenerResult = DeleteListenerDefaultStatusCode of DeleteListenerDefaultStatusCodeResponse|DeleteListenerStatusCode404 of DeleteListenerStatusCode404Response|DeleteListenerStatusCode500 of DeleteListenerStatusCode500Response

    type DeleteListenerArgs = {
      pathParams:DeleteListenerPathParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type DeleteListenerTemplatePathParams = {
      listenerId : string;
    //#endregion
      templateId : string;
    }
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type DeleteListenerTemplateHeaderParams = {
      apiKey : Option<string>;
    }
    //#endregion

    
    type DeleteListenerTemplateDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type DeleteListenerTemplateStatusCode404Response = {
      content:string;
      
    }
    
    type DeleteListenerTemplateStatusCode500Response = {
      content:string;
      
    }
    type DeleteListenerTemplateResult = DeleteListenerTemplateDefaultStatusCode of DeleteListenerTemplateDefaultStatusCodeResponse|DeleteListenerTemplateStatusCode404 of DeleteListenerTemplateStatusCode404Response|DeleteListenerTemplateStatusCode500 of DeleteListenerTemplateStatusCode500Response

    type DeleteListenerTemplateArgs = {
      pathParams:DeleteListenerTemplatePathParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type GetListenerByIdPathParams = {
      listenerId : string;
    }
    //#endregion

    
    type GetListenerByIdDefaultStatusCodeResponse = {
      content:Listener;
      
    }
    
    type GetListenerByIdStatusCode404Response = {
      content:string;
      
    }
    type GetListenerByIdResult = GetListenerByIdDefaultStatusCode of GetListenerByIdDefaultStatusCodeResponse|GetListenerByIdStatusCode404 of GetListenerByIdStatusCode404Response

    type GetListenerByIdArgs = {
      pathParams:GetListenerByIdPathParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type ListListenerTemplatesPathParams = {
      listenerId : string;
    }
    //#endregion

    
    type ListListenerTemplatesDefaultStatusCodeResponse = {
      content:ListenerTemplate[];
      
    }
    
    type ListListenerTemplatesStatusCode404Response = {
      content:string;
      
    }
    type ListListenerTemplatesResult = ListListenerTemplatesDefaultStatusCode of ListListenerTemplatesDefaultStatusCodeResponse|ListListenerTemplatesStatusCode404 of ListListenerTemplatesStatusCode404Response

    type ListListenerTemplatesArgs = {
      pathParams:ListListenerTemplatesPathParams;
    }

    
    type ListListenersDefaultStatusCodeResponse = {
      content:Listener[];
      
    }
    type ListListenersResult = ListListenersDefaultStatusCode of ListListenersDefaultStatusCodeResponse

    //#region Path parameters
    [<CLIMutable>]
    type UpdateListenerPathParams = {
      listenerId : string;
    }
    //#endregion

    //#region Body parameters
    [<CLIMutable>]
    type UpdateListenerBodyParams = Listener
    //#endregion

    
    type UpdateListenerDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type UpdateListenerStatusCode404Response = {
      content:string;
      
    }
    
    type UpdateListenerStatusCode422Response = {
      content:string;
      
    }
    type UpdateListenerResult = UpdateListenerDefaultStatusCode of UpdateListenerDefaultStatusCodeResponse|UpdateListenerStatusCode404 of UpdateListenerStatusCode404Response|UpdateListenerStatusCode422 of UpdateListenerStatusCode422Response

    type UpdateListenerArgs = {
      pathParams:UpdateListenerPathParams;
      bodyParams:UpdateListenerBodyParams
    }
    