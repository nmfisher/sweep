namespace Sweep
open Sweep.Model.Listener
open Sweep.Model.ListenerRequestBody
open Sweep.Model.ListenerTemplate
open ListenerApiHandlerParams
open ListenerApiServiceInterface
open System.Collections.Generic
open System
open Giraffe

module ListenerApiServiceImplementation =
    
    //#region Service implementation
    type ListenerApiServiceImpl() = 
      interface IListenerApiService with
      
        member this.AddListener ctx args =
          if true then 
            let content = "successful operation" :> obj :?> Listener // this cast is obviously wrong, and is only intended to allow generated project to compile   
            AddListenerDefaultStatusCode { content = content }
          else
            let content = "Invalid input" 
            AddListenerStatusCode422 { content = content }

        member this.AddListenerTemplate ctx args =
          if true then 
            let content = "Successfully added" 
            AddListenerTemplateDefaultStatusCode { content = content }
          else
            let content = "Listener or template not found" 
            AddListenerTemplateStatusCode404 { content = content }

        member this.DeleteListener ctx args =
          if true then 
            let content = "Successfully deleted" 
            DeleteListenerDefaultStatusCode { content = content }
          else
            let content = "Listener not found" 
            DeleteListenerStatusCode404 { content = content }

        member this.DeleteListenerTemplate ctx args =
          if true then 
            let content = "Successfully deleted" 
            DeleteListenerTemplateDefaultStatusCode { content = content }
          else
            let content = "Listener not found" 
            DeleteListenerTemplateStatusCode404 { content = content }

        member this.GetListener ctx args =
          if true then 
            let content = "successful operation" :> obj :?> Listener // this cast is obviously wrong, and is only intended to allow generated project to compile   
            GetListenerDefaultStatusCode { content = content }
          else
            let content = "Listener not found" 
            GetListenerStatusCode404 { content = content }

        member this.ListListenerTemplates ctx args =
          if true then 
            let content = "successful operation" :> obj :?> ListenerTemplate[] // this cast is obviously wrong, and is only intended to allow generated project to compile   
            ListListenerTemplatesDefaultStatusCode { content = content }
          else
            let content = "Listener not found" 
            ListListenerTemplatesStatusCode404 { content = content }

        member this.ListListeners ctx args =
            let content = "successful operation" :> obj :?> Listener[] // this cast is obviously wrong, and is only intended to allow generated project to compile   
            ListListenersDefaultStatusCode { content = content }

        member this.UpdateListener ctx args =
          if true then 
            let content = "Successfully updated" :> obj :?> Listener // this cast is obviously wrong, and is only intended to allow generated project to compile   
            UpdateListenerDefaultStatusCode { content = content }
          else if true then 
            let content = "Listener not found" 
            UpdateListenerStatusCode404 { content = content }
          else
            let content = "Invalid input" 
            UpdateListenerStatusCode422 { content = content }

      //#endregion

    let ListenerApiService = ListenerApiServiceImpl() :> IListenerApiService