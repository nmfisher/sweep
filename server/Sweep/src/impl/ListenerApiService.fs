namespace Sweep
open ListenerModel
open ListenerApiHandlerParams
open ListenerApiServiceInterface
open System.Collections.Generic
open System
open Giraffe
open UserContext
open CompositionRoot

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
              addListener args.bodyParams.EventName userId orgId
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
          | e ->
            DeleteListenerStatusCode500 { content = e.ToString() }

        member this.GetListenerById ctx args =
          try
            let userId = getUserId ctx.User.Claims
            let orgId = getOrgId ctx.User.Claims
            let listener = getListener args.pathParams.listenerId orgId
            GetListenerByIdDefaultStatusCode { content = listener }
          with
          | NotFoundException(msg) ->
            GetListenerByIdStatusCode404 { content = "Listener not found"  }
          | e -> 
            raise e

        member this.ListListeners ctx  =
          let userId = getUserId ctx.User.Claims
          let orgId = getOrgId ctx.User.Claims
          let listeners = listListeners orgId
          ListListenersDefaultStatusCode { content = listeners }

        member this.UpdateListener ctx args =
          try 
            let userId = getUserId ctx.User.Claims
            let orgId = getOrgId ctx.User.Claims
            match (String.IsNullOrWhiteSpace(args.bodyParams.EventName)) with
            | true ->
              UpdateListenerStatusCode422 {content = "Event name cannot be empty" }  
            | false ->
              updateListener args.pathParams.listenerId args.bodyParams.EventName orgId |> ignore
              UpdateListenerDefaultStatusCode {content = "OK"}
          with 
          | NotFoundException(msg) ->
              UpdateListenerStatusCode404 { content = msg }
          | e ->
            raise (e)
      //#endregion

    let ListenerApiService = ListenerApiServiceImpl() :> IListenerApiService