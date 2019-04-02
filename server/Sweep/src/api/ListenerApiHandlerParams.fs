namespace Sweep

open ListenerModel
open System.Collections.Generic

module ListenerApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type AddListenerBodyParams = Listener
    //#endregion

    
    type AddListenerStatusCode405Response = {
      content:string;
      
    }
    type AddListenerResult = AddListenerStatusCode405 of AddListenerStatusCode405Response

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

    
    type DeleteListenerStatusCode400Response = {
      content:string;
      
    }
    
    type DeleteListenerStatusCode404Response = {
      content:string;
      
    }
    type DeleteListenerResult = DeleteListenerStatusCode400 of DeleteListenerStatusCode400Response|DeleteListenerStatusCode404 of DeleteListenerStatusCode404Response

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
    
    type GetListenerByIdStatusCode400Response = {
      content:string;
      
    }
    
    type GetListenerByIdStatusCode404Response = {
      content:string;
      
    }
    type GetListenerByIdResult = GetListenerByIdDefaultStatusCode of GetListenerByIdDefaultStatusCodeResponse|GetListenerByIdStatusCode400 of GetListenerByIdStatusCode400Response|GetListenerByIdStatusCode404 of GetListenerByIdStatusCode404Response

    type GetListenerByIdArgs = {
      pathParams:GetListenerByIdPathParams;
    }

    
    type ListListenersDefaultStatusCodeResponse = {
      content:Listener;
      
    }
    type ListListenersResult = ListListenersDefaultStatusCode of ListListenersDefaultStatusCodeResponse


    //#region Body parameters
    [<CLIMutable>]
    type UpdateListenerBodyParams = Listener
    //#endregion

    
    type UpdateListenerStatusCode400Response = {
      content:string;
      
    }
    
    type UpdateListenerStatusCode404Response = {
      content:string;
      
    }
    
    type UpdateListenerStatusCode405Response = {
      content:string;
      
    }
    type UpdateListenerResult = UpdateListenerStatusCode400 of UpdateListenerStatusCode400Response|UpdateListenerStatusCode404 of UpdateListenerStatusCode404Response|UpdateListenerStatusCode405 of UpdateListenerStatusCode405Response

    type UpdateListenerArgs = {
      bodyParams:UpdateListenerBodyParams
    }
    