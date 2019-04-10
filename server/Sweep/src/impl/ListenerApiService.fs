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
            Sweep.Data.Listener.parse args.bodyParams.Trigger |> ignore // just to validate the condition string
            if String.IsNullOrEmpty(args.bodyParams.EventName) then
              AddListenerStatusCode422 { content = "Event name must not be empty"  }
            else             
              let userId = getUserId ctx.User.Claims
              let orgId = getOrgId ctx.User.Claims
              let listener = addListener args.bodyParams.EventName args.bodyParams.EventParams args.bodyParams.Trigger userId orgId
              AddListenerDefaultStatusCode { content = listener }
          with
          | e ->           
            AddListenerStatusCode422 { content = e.ToString()  }

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

        member this.AddListenerTemplate ctx args =
          try
            let userId = getUserId ctx.User.Claims
            let orgId = getOrgId ctx.User.Claims
            CompositionRoot.getListener args.pathParams.listenerId orgId |> ignore // throws NotFoundException
            CompositionRoot.createListenerTemplate args.pathParams.listenerId args.pathParams.templateId orgId
            AddListenerTemplateDefaultStatusCode { content = "OK" }
          with
          | NotFoundException(msg) ->
            AddListenerTemplateStatusCode404 { content = msg }
            
        member this.DeleteListenerTemplate ctx args =
          try
            let userId = getUserId ctx.User.Claims
            let orgId = getOrgId ctx.User.Claims
            CompositionRoot.getListener args.pathParams.listenerId orgId |> ignore // throws NotFoundException
            CompositionRoot.deleteListenerTemplate args.pathParams.listenerId args.pathParams.templateId orgId |> ignore
            DeleteListenerTemplateDefaultStatusCode { content = "OK" }
          with
          | NotFoundException(msg) ->
            DeleteListenerTemplateStatusCode404 { content = msg }

        member this.ListListenerTemplates ctx args =
          try
            let userId = getUserId ctx.User.Claims
            let orgId = getOrgId ctx.User.Claims
            CompositionRoot.getListener args.pathParams.listenerId orgId |> ignore // throws NotFoundException
            let listeners = CompositionRoot.listListenerTemplates args.pathParams.listenerId orgId
            ListListenerTemplatesDefaultStatusCode { content = listeners }
          with
          | NotFoundException(msg) ->
              ListListenerTemplatesStatusCode404 { content = msg }

      //#endregion

    let ListenerApiService = ListenerApiServiceImpl() :> IListenerApiService