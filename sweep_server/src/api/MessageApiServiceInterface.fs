namespace Sweep
open MessageApiHandlerParams
open System
open Giraffe
open Microsoft.AspNetCore.Http


module MessageApiServiceInterface =
    
    //#region Service interface
    type IMessageApiService = 
      abstract member GetmessageById:HttpContext -> GetmessageByIdArgs->GetmessageByIdResult
      abstract member ListMessages:HttpContext ->ListMessagesResult
    //#endregion