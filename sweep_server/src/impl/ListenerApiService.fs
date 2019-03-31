namespace Sweep
open ListenerModel
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
            let content = "Invalid input" 
            AddListenerStatusCode405 { content = content }

        member this.DeleteListener ctx args =
          if true then 
            let content = "Invalid ID supplied" 
            DeleteListenerStatusCode400 { content = content }
          else
            let content = "Listener not found" 
            DeleteListenerStatusCode404 { content = content }

        member this.GetListenerById ctx args =
          if true then 
            let content = "successful operation" :> obj :?> Listener // this cast is obviously wrong, and is only intended to allow generated project to compile   
            GetListenerByIdDefaultStatusCode { content = content }
          else if true then 
            let content = "Invalid ID supplied" 
            GetListenerByIdStatusCode400 { content = content }
          else
            let content = "Listener not found" 
            GetListenerByIdStatusCode404 { content = content }

        member this.ListListeners ctx  =
            let content = "successful operation" :> obj :?> Listener // this cast is obviously wrong, and is only intended to allow generated project to compile   
            ListListenersDefaultStatusCode { content = content }

        member this.UpdateListener ctx args =
          if true then 
            let content = "Invalid ID supplied" 
            UpdateListenerStatusCode400 { content = content }
          else if true then 
            let content = "Event not found" 
            UpdateListenerStatusCode404 { content = content }
          else
            let content = "Validation exception" 
            UpdateListenerStatusCode405 { content = content }

      //#endregion

    let ListenerApiService = ListenerApiServiceImpl() :> IListenerApiService