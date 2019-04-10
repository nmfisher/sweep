namespace Sweep.Model

open System
open System.Collections.Generic

module TemplateRequestBody = 

  //#region TemplateRequestBody


  type TemplateRequestBody = {
    Content : string;
    Subject : string;
    FromAddress : string;
    FromName : string;
    SendTo : string[];
  }
  //#endregion
  