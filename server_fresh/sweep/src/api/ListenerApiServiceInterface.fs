namespace Sweep
open ListenerApiHandlerParams
open System
open Giraffe
open Microsoft.AspNetCore.Http


module ListenerApiServiceInterface =
    
    //#region Service interface
    type IListenerApiService = 
      abstract member AddListener:HttpContext -> AddListenerArgs->AddListenerResult
      abstract member AddListenerTemplate:HttpContext -> AddListenerTemplateArgs->AddListenerTemplateResult
      abstract member DeleteListener:HttpContext -> DeleteListenerArgs->DeleteListenerResult
      abstract member DeleteListenerTemplate:HttpContext -> DeleteListenerTemplateArgs->DeleteListenerTemplateResult
      abstract member GetListener:HttpContext -> GetListenerArgs->GetListenerResult
      abstract member ListListenerTemplates:HttpContext -> ListListenerTemplatesArgs->ListListenerTemplatesResult
      abstract member ListListeners:HttpContext -> ListListenersArgs->ListListenersResult
      abstract member UpdateListener:HttpContext -> UpdateListenerArgs->UpdateListenerResult
    //#endregion