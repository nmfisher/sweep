namespace Sweep

open Sweep.Model.Template
open Sweep.TemplateRenderer
open TemplateApiHandlerParams
open TemplateApiServiceInterface
open System.Collections.Generic
open System
open Giraffe
open UserContext
open System.Net.Mail
open CompositionRoot
open Exceptions
open Microsoft.AspNetCore.Http


module TemplateApiServiceImplementation =
    
    //#region Service implementation
    type TemplateApiServiceImpl() = 

      let validateEmails (emails:string[]) (content:string) = 
        if isNull emails then
          false
        else 
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
                    x.StartsWith "{{" && x.EndsWith "}}")

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
              
              let template = CompositionRoot.addTemplate args.bodyParams.Content args.bodyParams.Subject args.bodyParams.FromName args.bodyParams.FromAddress args.bodyParams.SendTo orgId userId 
              AddTemplateDefaultStatusCode { content = template }

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

        member this.RenderTemplate (ctx:HttpContext) (args:RenderTemplateArgs) =
          try
            let orgId = getOrgId ctx.User.Claims
            let userId = getUserId ctx.User.Claims
            let template = CompositionRoot.getTemplate args.pathParams.templateId orgId
            let mailDefaults = ctx.GetService<MailDefaults>()
            let rendered = renderTemplate mailDefaults args.bodyParams.Params template
            RenderTemplateDefaultStatusCode { content = rendered }
          with
          | NotFoundException(msg) ->
            RenderTemplateStatusCode404 { content = msg }
          | RenderException(msg) ->
            RenderTemplateStatusCode422 { content = msg }

        member this.UpdateTemplate ctx args =
          try
            let orgId = getOrgId ctx.User.Claims            
            match validateTemplate args.bodyParams.Content args.bodyParams.SendTo with
            | Some err ->
              UpdateTemplateStatusCode422 { content = err }
            | None ->
              let template = CompositionRoot.updateTemplate args.pathParams.templateId args.bodyParams.Content args.bodyParams.Subject args.bodyParams.FromName args.bodyParams.FromAddress args.bodyParams.SendTo orgId
              UpdateTemplateDefaultStatusCode { content = template }
          with
          | NotFoundException(msg) ->
            UpdateTemplateStatusCode404 { content = msg }
          | e ->
            UpdateTemplateStatusCode422 { content = e.ToString()   }
      //#endregion

    let TemplateApiService = TemplateApiServiceImpl() :> ITemplateApiService
