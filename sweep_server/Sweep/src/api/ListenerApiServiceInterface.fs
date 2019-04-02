namespace Sweep
open ListenerApiHandlerParams
open System
open Giraffe
open Microsoft.AspNetCore.Http


module ListenerApiServiceInterface =
    
    //#region Service interface
    type IListenerApiService = 
      abstract member AddListener:HttpContext -> AddListenerArgs->AddListenerResult
      abstract member DeleteListener:HttpContext -> DeleteListenerArgs->DeleteListenerResult
      abstract member GetListenerById:HttpContext -> GetListenerByIdArgs->GetListenerByIdResult
      abstract member ListListeners:HttpContext ->ListListenersResult
      abstract member UpdateListener:HttpContext -> UpdateListenerArgs->UpdateListenerResult
    //#endregion