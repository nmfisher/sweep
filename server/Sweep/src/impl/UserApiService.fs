namespace Sweep
open Sweep.Model.User
open UserApiHandlerParams
open UserApiServiceInterface
open System.Collections.Generic
open System
open Giraffe
open UserContext


module UserApiServiceImplementation =
    
    //#region Service implementation
    type UserApiServiceImpl() = 
      interface IUserApiService with
      
        member this.DeleteUser ctx  =
          if true then 
            let content = "Invalid username supplied" 
            DeleteUserStatusCode400 { content = content }
          else
            let content = "User not found" 
            DeleteUserStatusCode404 { content = content }

        member this.GetUserInfo ctx  =
            let user = 
              { 
                Id=getUserId ctx.User.Claims;
                OrganizationId=getOrgId ctx.User.Claims;
                Username="";
                Password="";
              }
            GetUserInfoDefaultStatusCode { content = user }

        member this.LoginUser ctx args =
          if true then 
            let content = "successful operation" :> obj :?> string // this cast is obviously wrong, and is only intended to allow generated project to compile   
            LoginUserDefaultStatusCode { content = content }
          else
            let content = "Invalid username/password supplied" 
            LoginUserStatusCode400 { content = content }

        member this.LogoutUser ctx  =
            let content = "successful operation" 
            LogoutUserDefaultStatusCode { content = content }

        member this.UpdateUser ctx args =
          if true then 
            let content = "Invalid user supplied" 
            UpdateUserStatusCode400 { content = content }
          else
            let content = "User not found" 
            UpdateUserStatusCode404 { content = content }

      //#endregion

    let UserApiService = UserApiServiceImpl() :> IUserApiService