namespace Sweep
open UserApiHandlerParams
open System
open Giraffe
open Microsoft.AspNetCore.Http


module UserApiServiceInterface =
    
    //#region Service interface
    type IUserApiService = 
      abstract member DeleteUser:HttpContext ->DeleteUserResult
      abstract member GetUserInfo:HttpContext ->GetUserInfoResult
      abstract member LoginUser:HttpContext -> LoginUserArgs->LoginUserResult
      abstract member LogoutUser:HttpContext ->LogoutUserResult
      abstract member UpdateUser:HttpContext -> UpdateUserArgs->UpdateUserResult
    //#endregion