namespace Sweep

open ListenerModel
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
    type DeleteListenerPathParams = {
      listenerId : string
    }
    //#endregion

    //#region Header parameters
    [<CLIMutable>]
    type DeleteListenerHeaderParams = {
      apiKey : Option<string>
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
    type GetListenerByIdPathParams = {
      listenerId : string
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

    
    type ListListenersDefaultStatusCodeResponse = {
      content:Listener[];
      
    }
    type ListListenersResult = ListListenersDefaultStatusCode of ListListenersDefaultStatusCodeResponse

    //#region Path parameters
    [<CLIMutable>]
    type UpdateListenerPathParams = {
      listenerId : string
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
    