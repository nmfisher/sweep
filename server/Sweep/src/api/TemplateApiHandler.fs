namespace Sweep

open System.Collections.Generic
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open TemplateApiHandlerParams
open TemplateApiServiceInterface
open TemplateApiServiceImplementation
open TemplateModel

module TemplateApiHandler = 

    /// <summary>
    /// 
    /// </summary>

    //#region AddTemplate
    /// <summary>
    /// Create a new Template
    /// </summary>

    let AddTemplate  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let! bodyParams = 
            ctx.BindJsonAsync<AddTemplateBodyParams>()
          let serviceArgs = {    bodyParams=bodyParams } : AddTemplateArgs
          let result = TemplateApiService.AddTemplate ctx serviceArgs
          return! (match result with 
                      | AddTemplateDefaultStatusCode resolved ->
                            setStatusCode 200 >=> text resolved.content 
                      | AddTemplateStatusCode422 resolved ->
                            setStatusCode 422 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region DeleteTemplate
    /// <summary>
    /// Deletes a Template
    /// </summary>

    let DeleteTemplate (pathParams:DeleteTemplatePathParams) : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let serviceArgs = {   pathParams=pathParams;  } : DeleteTemplateArgs
          let result = TemplateApiService.DeleteTemplate ctx serviceArgs
          return! (match result with 
                      | DeleteTemplateDefaultStatusCode resolved ->
                            setStatusCode 200 >=> text resolved.content 
                      | DeleteTemplateStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region GetTemplateById
    /// <summary>
    /// Find Template by ID
    /// </summary>

    let GetTemplateById (pathParams:GetTemplateByIdPathParams) : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let serviceArgs = {   pathParams=pathParams;  } : GetTemplateByIdArgs
          let result = TemplateApiService.GetTemplateById ctx serviceArgs
          return! (match result with 
                      | GetTemplateByIdDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
                      | GetTemplateByIdStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region ListTemplate
    /// <summary>
    /// List all Templates
    /// </summary>

    let ListTemplate  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let result = TemplateApiService.ListTemplate ctx 
          return! (match result with 
                      | ListTemplateDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
          ) next ctx
        }
    //#endregion

    //#region UpdateTemplate
    /// <summary>
    /// Update an existing Template
    /// </summary>

    let UpdateTemplate  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let! bodyParams = 
            ctx.BindJsonAsync<UpdateTemplateBodyParams>()
          let serviceArgs = {    bodyParams=bodyParams } : UpdateTemplateArgs
          let result = TemplateApiService.UpdateTemplate ctx serviceArgs
          return! (match result with 
                      | UpdateTemplateStatusCode400 resolved ->
                            setStatusCode 400 >=> text resolved.content 
                      | UpdateTemplateStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
                      | UpdateTemplateStatusCode422 resolved ->
                            setStatusCode 422 >=> text resolved.content 
          ) next ctx
        }
    //#endregion


    
