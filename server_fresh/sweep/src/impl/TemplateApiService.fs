namespace Sweep
open Sweep.Model.Template
open Sweep.Model.TemplateRequestBody
open TemplateApiHandlerParams
open TemplateApiServiceInterface
open System.Collections.Generic
open System
open Giraffe

module TemplateApiServiceImplementation =
    
    //#region Service implementation
    type TemplateApiServiceImpl() = 
      interface ITemplateApiService with
      
        member this.AddTemplate ctx args =
          if true then 
            let content = "Success" 
            AddTemplateDefaultStatusCode { content = content }
          else
            let content = "Invalid input" 
            AddTemplateStatusCode422 { content = content }

        member this.DeleteTemplate ctx args =
          if true then 
            let content = "Successful deletion" 
            DeleteTemplateDefaultStatusCode { content = content }
          else
            let content = "Template not found" 
            DeleteTemplateStatusCode404 { content = content }

        member this.GetTemplateById ctx args =
          if true then 
            let content = "successful operation" :> obj :?> Template // this cast is obviously wrong, and is only intended to allow generated project to compile   
            GetTemplateByIdDefaultStatusCode { content = content }
          else
            let content = "Listener not found" 
            GetTemplateByIdStatusCode404 { content = content }

        member this.ListTemplate ctx args =
            let content = "successful operation" :> obj :?> Template[] // this cast is obviously wrong, and is only intended to allow generated project to compile   
            ListTemplateDefaultStatusCode { content = content }

        member this.UpdateTemplate ctx args =
          if true then 
            let content = "Successfully updated" 
            UpdateTemplateDefaultStatusCode { content = content }
          else if true then 
            let content = "Template not found" 
            UpdateTemplateStatusCode404 { content = content }
          else
            let content = "Validation exception" 
            UpdateTemplateStatusCode422 { content = content }

      //#endregion

    let TemplateApiService = TemplateApiServiceImpl() :> ITemplateApiService