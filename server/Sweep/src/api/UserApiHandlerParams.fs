namespace Sweep

open Sweep.Model.User
open Sweep.Model.UserRequestBody
open System.Collections.Generic

module UserApiHandlerParams = 


    
    type DeleteUserStatusCode400Response = {
      content:string;
      
    }
    
    type DeleteUserStatusCode404Response = {
      content:string;
      
    }
    type DeleteUserResult = DeleteUserStatusCode400 of DeleteUserStatusCode400Response|DeleteUserStatusCode404 of DeleteUserStatusCode404Response


    
    type GetUserInfoDefaultStatusCodeResponse = {
      content:User;
      
    }
    
    type GetUserInfoStatusCode400Response = {
      content:string;
      
    }
    
    type GetUserInfoStatusCode404Response = {
      content:string;
      
    }
    type GetUserInfoResult = GetUserInfoDefaultStatusCode of GetUserInfoDefaultStatusCodeResponse|GetUserInfoStatusCode400 of GetUserInfoStatusCode400Response|GetUserInfoStatusCode404 of GetUserInfoStatusCode404Response


    //#region Query parameters
    [<CLIMutable>]
    type LoginUserQueryParams = {
      username : string ;
      

      password : string ;
      
    }
    //#endregion

    
    type LoginUserDefaultStatusCodeResponse = {
      content:string;
      
    }
    
    type LoginUserStatusCode400Response = {
      content:string;
      
    }
    type LoginUserResult = LoginUserDefaultStatusCode of LoginUserDefaultStatusCodeResponse|LoginUserStatusCode400 of LoginUserStatusCode400Response

    type LoginUserArgs = {
      queryParams:Result<LoginUserQueryParams,string>;
    }

    
    type LogoutUserDefaultStatusCodeResponse = {
      content:string;
      
    }
    type LogoutUserResult = LogoutUserDefaultStatusCode of LogoutUserDefaultStatusCodeResponse


    //#region Body parameters
    [<CLIMutable>]
    type UpdateUserBodyParams = UserRequestBody 
    //#endregion

    
    type UpdateUserStatusCode400Response = {
      content:string;
      
    }
    
    type UpdateUserStatusCode404Response = {
      content:string;
      
    }
    type UpdateUserResult = UpdateUserStatusCode400 of UpdateUserStatusCode400Response|UpdateUserStatusCode404 of UpdateUserStatusCode404Response

    type UpdateUserArgs = {
      bodyParams:UpdateUserBodyParams
    }
    