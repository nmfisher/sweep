namespace Sweep
open MessageApiHandlerParams
open System
open Giraffe
open Microsoft.AspNetCore.Http


module MessageApiServiceInterface =
    
    //#region Service interface
    type IMessageApiService = 
      abstract member GetMessageById:HttpContext -> GetMessageByIdArgs->GetMessageByIdResult
      abstract member ListMessages:HttpContext -> ListMessagesArgs->ListMessagesResult
    //#endregion