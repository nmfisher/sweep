namespace Sweep

open Sweep.Model.Listener
open ListenerApiHandlerParams
open ListenerApiServiceInterface
open System.Collections.Generic
open System
open Giraffe
open UserContext
open CompositionRoot
open Exceptions

module ListenerApiServiceImplementation =
    
    //#region Service implementation
    type ListenerApiServiceImpl() = 
      interface IListenerApiService with
      
        member this.AddListener ctx args =
          try
            if String.IsNullOrEmpty(args.bodyParams.EventName) then
              AddListenerStatusCode405 { content = "Event name must not be empty"  }
            else             
              let userId = getUserId ctx.User.Claims
              let orgId = getOrgId ctx.User.Claims
              addListener args.bodyParams.EventName args.bodyParams.TemplateId userId orgId
              AddListenerDefaultStatusCode { content = "OK" }
          with
          | e ->           
            AddListenerStatusCode405 { content = e.ToString()  }

        member this.DeleteListener ctx args =
          try
            let userId = getUserId ctx.User.Claims
            let orgId = getOrgId ctx.User.Claims
            deleteListener args.pathParams.listenerId userId orgId
            DeleteListenerDefaultStatusCode { content = "OK" }
          with
          | NotFoundException(msg) ->
            DeleteListenerStatusCode404 { content = "Not Found" }

        member this.ListListeners ctx  =
          let userId = getUserId ctx.User.Claims
          let orgId = getOrgId ctx.User.Claims
          let listeners = listListeners orgId
          ListListenersDefaultStatusCode { content = listeners }

      //#endregion

    let ListenerApiService = ListenerApiServiceImpl() :> IListenerApiService