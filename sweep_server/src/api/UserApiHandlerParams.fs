namespace Sweep

open UserModel
open System.Collections.Generic

module UserApiHandlerParams = 


    //#region Body parameters
    [<CLIMutable>]
    type CreateUserBodyParams = {
      user : User
    }
    //#endregion

    
    type CreateUserDefaultStatusCodeResponse = {
      content:string;
      
    }
    type CreateUserResult = CreateUserDefaultStatusCode of CreateUserDefaultStatusCodeResponse

    type CreateUserArgs = {
      bodyParams:CreateUserBodyParams
    }
    //#region Path parameters
    [<CLIMutable>]
    type DeleteUserPathParams = {
      userId : string
    }
    //#endregion

    
    type DeleteUserStatusCode400Response = {
      content:string;
      
    }
    
    type DeleteUserStatusCode404Response = {
      content:string;
      
    }
    type DeleteUserResult = DeleteUserStatusCode400 of DeleteUserStatusCode400Response|DeleteUserStatusCode404 of DeleteUserStatusCode404Response

    type DeleteUserArgs = {
      pathParams:DeleteUserPathParams;
    }
    //#region Path parameters
    [<CLIMutable>]
    type GetUserByNamePathParams = {
      userId : string
    }
    //#endregion

    
    type GetUserByNameDefaultStatusCodeResponse = {
      content:User;
      
    }
    
    type GetUserByNameStatusCode400Response = {
      content:string;
      
    }
    
    type GetUserByNameStatusCode404Response = {
      content:string;
      
    }
    type GetUserByNameResult = GetUserByNameDefaultStatusCode of GetUserByNameDefaultStatusCodeResponse|GetUserByNameStatusCode400 of GetUserByNameStatusCode400Response|GetUserByNameStatusCode404 of GetUserByNameStatusCode404Response

    type GetUserByNameArgs = {
      pathParams:GetUserByNamePathParams;
    }

    //#region Query parameters
    [<CLIMutable>]
    type LoginUserQueryParams = {
      username : string

      password : string
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

    //#region Path parameters
    [<CLIMutable>]
    type UpdateUserPathParams = {
      userId : string
    }
    //#endregion

    //#region Body parameters
    [<CLIMutable>]
    type UpdateUserBodyParams = {
      user : User
    }
    //#endregion

    
    type UpdateUserStatusCode400Response = {
      content:string;
      
    }
    
    type UpdateUserStatusCode404Response = {
      content:string;
      
    }
    type UpdateUserResult = UpdateUserStatusCode400 of UpdateUserStatusCode400Response|UpdateUserStatusCode404 of UpdateUserStatusCode404Response

    type UpdateUserArgs = {
      pathParams:UpdateUserPathParams;
      bodyParams:UpdateUserBodyParams
    }
    