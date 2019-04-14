namespace Sweep

open System.Collections.Generic
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2.ContextInsensitive
open UserApiHandlerParams
open UserApiServiceInterface
open UserApiServiceImplementation
open Sweep.Model.User
open Sweep.Model.UserRequestBody

module UserApiHandler = 

    /// <summary>
    /// 
    /// </summary>

    //#region DeleteUser
    /// <summary>
    /// Delete user
    /// </summary>

    let DeleteUser  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let result = UserApiService.DeleteUser ctx 
          return! (match result with 
                      | DeleteUserStatusCode400 resolved ->
                            setStatusCode 400 >=> text resolved.content 
                      | DeleteUserStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region GetUserInfo
    /// <summary>
    /// Get user info for the currently authenticated user
    /// </summary>

    let GetUserInfo  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let result = UserApiService.GetUserInfo ctx 
          return! (match result with 
                      | GetUserInfoDefaultStatusCode resolved ->
                            setStatusCode 200 >=> json resolved.content 
                      | GetUserInfoStatusCode400 resolved ->
                            setStatusCode 400 >=> text resolved.content 
                      | GetUserInfoStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region LoginUser
    /// <summary>
    /// Logs user into the system
    /// </summary>

    let LoginUser  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let queryParams = ctx.TryBindQueryString<LoginUserQueryParams>()
          let serviceArgs = {  queryParams=queryParams;    } : LoginUserArgs
          let result = UserApiService.LoginUser ctx serviceArgs
          return! (match result with 
                      | LoginUserDefaultStatusCode resolved ->
                            setStatusCode 200 >=> text resolved.content 
                      | LoginUserStatusCode400 resolved ->
                            setStatusCode 400 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region LogoutUser
    /// <summary>
    /// Logs out current logged in user session
    /// </summary>

    let LogoutUser  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let result = UserApiService.LogoutUser ctx 
          return! (match result with 
                      | LogoutUserDefaultStatusCode resolved ->
                            setStatusCode 0 >=> text resolved.content 
          ) next ctx
        }
    //#endregion

    //#region UpdateUser
    /// <summary>
    /// Updated user
    /// </summary>

    let UpdateUser  : HttpHandler = 
      fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
          let! bodyParams = 
            ctx.BindJsonAsync<UpdateUserBodyParams>()
          let serviceArgs = {     bodyParams=bodyParams } : UpdateUserArgs
          let result = UserApiService.UpdateUser ctx serviceArgs
          return! (match result with 
                      | UpdateUserStatusCode400 resolved ->
                            setStatusCode 400 >=> text resolved.content 
                      | UpdateUserStatusCode404 resolved ->
                            setStatusCode 404 >=> text resolved.content 
          ) next ctx
        }
    //#endregion


    
