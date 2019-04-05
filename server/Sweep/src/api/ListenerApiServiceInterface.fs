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
      abstract member ListListeners:HttpContext ->ListListenersResult
    //#endregion