namespace Sweep
open Sweep.Model.Template
open TemplateApiHandlerParams
open TemplateApiServiceInterface
open System.Collections.Generic
open System
open Giraffe
open UserContext
open System.Net.Mail
open CompositionRoot
open Exceptions


module TemplateApiServiceImplementation =
    
    //#region Service implementation
    type TemplateApiServiceImpl() = 

      let validateEmails (emails:string[]) (content:string) = 
        match emails.Length with 
        | 0 -> false
        | _ ->
          emails |> 
            Seq.forall
              (fun x -> 
                try 
                  MailAddress(x) |> ignore
                  true
                with                 
                | e ->
                  x.StartsWith "{{" && x.EndsWith "}}" && content.Contains(x))

      let validateTemplate content sendTo = 
        if String.IsNullOrWhiteSpace(content) then 
          Some("Content cannot be empty")
        else if not (validateEmails sendTo content) then                    
          Some("At least one email or placeholder must be specified")
        else                
          None
      
      interface ITemplateApiService with
        member this.AddTemplate ctx args =
            let orgId = getOrgId ctx.User.Claims
            let userId = getUserId ctx.User.Claims

            match validateTemplate args.bodyParams.Content args.bodyParams.SendTo  with
            | Some err ->
              AddTemplateStatusCode422 { content = err  }  
            | None ->            
              
              CompositionRoot.addTemplate args.bodyParams.Content args.bodyParams.Subject args.bodyParams.FromName args.bodyParams.FromAddress args.bodyParams.SendTo orgId userId |> ignore
              AddTemplateDefaultStatusCode { content = "OK" }

        member this.DeleteTemplate ctx args =
          try 
            let orgId = getOrgId ctx.User.Claims
            let userId = getUserId ctx.User.Claims
            CompositionRoot.deleteTemplate args.pathParams.templateId orgId userId
            DeleteTemplateDefaultStatusCode { content = "OK" }
          with
          | NotFoundException(msg) ->
            DeleteTemplateStatusCode404 { content = "Not found" }

        member this.GetTemplateById ctx args =
          try
            let orgId = getOrgId ctx.User.Claims
            let userId = getUserId ctx.User.Claims
            let template = CompositionRoot.getTemplate args.pathParams.templateId orgId
            GetTemplateByIdDefaultStatusCode { content = template }
          with
          | NotFoundException(msg) ->
            GetTemplateByIdStatusCode404 { content = msg }

        member this.ListTemplate ctx  =
          let orgId = getOrgId ctx.User.Claims
          let templates = CompositionRoot.listTemplates orgId
          ListTemplateDefaultStatusCode { content = templates }

        member this.UpdateTemplate ctx args =
          try
            let orgId = getOrgId ctx.User.Claims            
            match validateTemplate args.bodyParams.Content args.bodyParams.SendTo with
            | Some err ->
              UpdateTemplateStatusCode422 { content = err }
            | None ->
              CompositionRoot.updateTemplate args.pathParams.templateId args.bodyParams.Content args.bodyParams.Subject args.bodyParams.FromName args.bodyParams.FromAddress args.bodyParams.SendTo orgId
              UpdateTemplateDefaultStatusCode { content = "OK" }
          with
          | NotFoundException(msg) ->
            UpdateTemplateStatusCode404 { content = msg }
          | e ->
            UpdateTemplateStatusCode422 { content = e.ToString()   }
      //#endregion

    let TemplateApiService = TemplateApiServiceImpl() :> ITemplateApiService