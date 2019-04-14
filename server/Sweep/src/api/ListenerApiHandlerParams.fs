namespace Sweep

open Sweep.Model.Listener
open Sweep.Model.ListenerRequestBody
open Sweep.Model.ListenerTemplate
open System.Collections.Generic

module ListenerApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddListenerBodyParams = ListenerRequestBody 
    //#endregion

    
    type AddListenerDefaultStatusCodeResponse = {
      content:Listener;
      
    }
    
    type AddListenerStatusCode422Response = {
      content:string;
      
    }
    type AddListenerResult = AddListenerDefaultStatusCode of AddListenerDefaultStatusCodeResponse|AddListenerStatusCode422 of AddListenerStatusCode422Response

    type AddListenerArgs = {
      bodyParams:AddListenerBodyParams
    }
    //#region Path parameters
    [<CLIMutable>]
    type AddListenerTemplatePathParams = {
      listenerId : string ;
    //#endregion
      templateId : string ;
    }
    //#endregion

    
    type AddListenerTemplateDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type AddListenerTemplateStatusCode404Response = {
      content:string;
      
    }
    type AddListenerTemplateResult = AddListenerTemplateDefaultStatusCode of AddListenerTemplateDefaultStatusCodeResponse|AddListenerTemplateStatusCode404 of AddListenerTemplateStatusCode404Response

    type AddListenerTemplateArgs = {
      pathParams:AddListenerTemplatePathParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type DeleteListenerPathParams = {
      listenerId : string ;
    }
    //#endregion

    
    type DeleteListenerDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type DeleteListenerStatusCode404Response = {
      content:string;
      
    }
    type DeleteListenerResult = DeleteListenerDefaultStatusCode of DeleteListenerDefaultStatusCodeResponse|DeleteListenerStatusCode404 of DeleteListenerStatusCode404Response

    type DeleteListenerArgs = {
      pathParams:DeleteListenerPathParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type DeleteListenerTemplatePathParams = {
      listenerId : string ;
    //#endregion
      templateId : string ;
    }
    //#endregion

    
    type DeleteListenerTemplateDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type DeleteListenerTemplateStatusCode404Response = {
      content:string;
      
    }
    type DeleteListenerTemplateResult = DeleteListenerTemplateDefaultStatusCode of DeleteListenerTemplateDefaultStatusCodeResponse|DeleteListenerTemplateStatusCode404 of DeleteListenerTemplateStatusCode404Response

    type DeleteListenerTemplateArgs = {
      pathParams:DeleteListenerTemplatePathParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type GetListenerPathParams = {
      listenerId : string ;
    }
    //#endregion

    
    type GetListenerDefaultStatusCodeResponse = {
      content:Listener;
      
    }
    
    type GetListenerStatusCode404Response = {
      content:string;
      
    }
    type GetListenerResult = GetListenerDefaultStatusCode of GetListenerDefaultStatusCodeResponse|GetListenerStatusCode404 of GetListenerStatusCode404Response

    type GetListenerArgs = {
      pathParams:GetListenerPathParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type ListListenerTemplatesPathParams = {
      listenerId : string ;
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
      listenerId : string ;
    }
    //#endregion

    //#region Body parameters
    [<CLIMutable>]
    type UpdateListenerBodyParams = ListenerRequestBody 
    //#endregion

    
    type UpdateListenerDefaultStatusCodeResponse = {
      content:Listener;
      
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
    