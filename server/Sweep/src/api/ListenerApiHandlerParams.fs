namespace Sweep

open Sweep.Model.Listener
open Sweep.Model.ListenerTemplate
open System.Collections.Generic

module ListenerApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddListenerBodyParams = Listener
    //#endregion

    
    type AddListenerDefaultStatusCodeResponse = {
      content:string;
      
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
    type AddListenerTemplateResult = AddListenerTemplateDefaultStatusCode of AddListenerTemplateDefaultStatusCodeResponse|AddListenerTemplateStatusCode404 of AddListenerTemplateStatusCode404Response

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
    type DeleteListenerResult = DeleteListenerDefaultStatusCode of DeleteListenerDefaultStatusCodeResponse|DeleteListenerStatusCode404 of DeleteListenerStatusCode404Response

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
    type DeleteListenerTemplateResult = DeleteListenerTemplateDefaultStatusCode of DeleteListenerTemplateDefaultStatusCodeResponse|DeleteListenerTemplateStatusCode404 of DeleteListenerTemplateStatusCode404Response

    type DeleteListenerTemplateArgs = {
      pathParams:DeleteListenerTemplatePathParams;
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

    