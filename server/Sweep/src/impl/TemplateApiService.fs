namespace Sweep
open TemplateModel
open TemplateApiHandlerParams
open TemplateApiServiceInterface
open System.Collections.Generic
open System
open Giraffe
open UserContext
open System.Net.Mail
open CompositionRoot

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
      
      interface ITemplateApiService with
        member this.AddTemplate ctx args =
            let orgId = getOrgId ctx.User.Claims
            let userId = getUserId ctx.User.Claims
            if String.IsNullOrWhiteSpace(args.bodyParams.Content) then 
              AddTemplateStatusCode422 { content = "Content cannot be empty" }  
            else if not (validateEmails args.bodyParams.SendTo args.bodyParams.Content) then                    
              AddTemplateStatusCode422 { content = "At least one email or placeholder must be specified" }  
            else  
              CompositionRoot.addTemplate args.bodyParams.Content args.bodyParams.SendTo orgId userId |> ignore
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
          if true then 
            let content = "successful operation" :> obj :?> Template // this cast is obviously wrong, and is only intended to allow generated project to compile   
            GetTemplateByIdDefaultStatusCode { content = content }
          else if true then 
            let content = "Invalid ID supplied" 
            GetTemplateByIdStatusCode400 { content = content }
          else
            let content = "Listener not found" 
            GetTemplateByIdStatusCode404 { content = content }

        member this.ListTemplate ctx  =
          let orgId = getOrgId ctx.User.Claims
          let templates = CompositionRoot.listTemplates orgId
          ListTemplateDefaultStatusCode { content = templates }

        member this.UpdateTemplate ctx args =
          if true then 
            let content = "Invalid ID supplied" 
            UpdateTemplateStatusCode400 { content = content }
          else if true then 
            let content = "Template not found" 
            UpdateTemplateStatusCode404 { content = content }
          else
            let content = "Validation exception" 
            UpdateTemplateStatusCode422 { content = content }

      //#endregion

    let TemplateApiService = TemplateApiServiceImpl() :> ITemplateApiService