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
open Sweep.Model.ListenerRequestBody

module ListenerApiServiceImplementation =
    
    //#region Service implementation
    type ListenerApiServiceImpl() = 

      let validate (bodyParams:ListenerRequestBody) = 
        Sweep.Data.Listener.parse bodyParams.Trigger |> ignore // just to validate the condition string
        if (String.IsNullOrEmpty(bodyParams.EventName)) then
          raise (Exception("Event name must not be empty"))
      interface IListenerApiService with
      
        member this.AddListener ctx args =
          try
            validate args.bodyParams
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

        member this.GetListener ctx args =
          try
            let userId = getUserId ctx.User.Claims
            let orgId = getOrgId ctx.User.Claims
            let listener = CompositionRoot.getListener args.pathParams.listenerId orgId
            GetListenerDefaultStatusCode { content = listener }
          with
          | NotFoundException(msg) ->
            GetListenerStatusCode404 { content = msg }        

        member this.ListMessagesForAction ctx args =
          try
            let userId = getUserId ctx.User.Claims
            let orgId = getOrgId ctx.User.Claims
            CompositionRoot.getListenerAction args.pathParams.listenerActionId orgId |> ignore // throws NotFoundException
            let messages = CompositionRoot.listMessagesForListenerAction args.pathParams.listenerActionId orgId
            ListMessagesForActionDefaultStatusCode { content = messages }
          with
          | NotFoundException(msg) ->
            ListMessagesForActionStatusCode404 { content = msg }
            
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

        member this.UpdateListener ctx (args:UpdateListenerArgs) =
          try
            validate args.bodyParams
            let userId = getUserId ctx.User.Claims
            let orgId = getOrgId ctx.User.Claims
            let listener = CompositionRoot.updateListener args.pathParams.listenerId args.bodyParams.EventName args.bodyParams.EventParams args.bodyParams.Trigger userId orgId 
            UpdateListenerDefaultStatusCode { content = listener }
          with
          | NotFoundException(msg) ->
              UpdateListenerStatusCode404 { content = msg }
          | e ->
              UpdateListenerStatusCode422 { content = e.ToString() }            

      //#endregion

    let ListenerApiService = ListenerApiServiceImpl() :> IListenerApiService