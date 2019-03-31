namespace Sweep
open TemplateApiHandlerParams
open System
open Giraffe
open Microsoft.AspNetCore.Http


module TemplateApiServiceInterface =
    
    //#region Service interface
    type ITemplateApiService = 
      abstract member AddTemplate:HttpContext -> AddTemplateArgs->AddTemplateResult
      abstract member DeleteTemplate:HttpContext -> DeleteTemplateArgs->DeleteTemplateResult
      abstract member GetTemplateById:HttpContext -> GetTemplateByIdArgs->GetTemplateByIdResult
      abstract member ListTemplate:HttpContext ->ListTemplateResult
      abstract member UpdateTemplate:HttpContext -> UpdateTemplateArgs->UpdateTemplateResult
    //#endregion