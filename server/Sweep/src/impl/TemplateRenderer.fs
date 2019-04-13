namespace Sweep

open Stubble.Core.Builders
open Sweep.Model.Template
open Sweep.Model.Message
open System
open System.Text.RegularExpressions
open Exceptions

module TemplateRenderer =

  type MailDefaults = {
      FromAddress:string;
      FromName:string;
      Subject:string;
    }    

  let (<=>) a b = 
    match String.IsNullOrWhiteSpace(a) with
    | true ->
      b 
    | false ->
      a

  let renderTemplate mailDefaults eventParams (template:Template) =
    let paramDict = match eventParams with | null -> dict [||] | _ -> eventParams

    let unresolved = 
      template.SendTo 
      |> Array.append [| template.FromAddress;
                         template.FromName; 
                         template.Subject; 
                         template.Content |] 
      |> Array.map (fun x -> Regex.Match(x, "{{(?<name>[a-zA-Z0-9_]+)}}").Groups.["name"].Value)
      |> Seq.where (String.IsNullOrEmpty >> not)
      |> Seq.where (paramDict.ContainsKey >> not)
      |> Seq.distinct
    match Seq.isEmpty unresolved with
    | false ->
      raise (RenderException("Values for the following parameters were not provided: " + String.Join(",", unresolved)))
    | true ->
      let builder = StubbleBuilder().Build()
      let message = {
        Message.Id = Guid.NewGuid().ToString();
        SendTo = template.SendTo |> Array.map (fun x -> builder.Render(x, paramDict));
        FromAddress = (<=>) (builder.Render(template.FromAddress, paramDict)) mailDefaults.FromAddress;
        FromName = (<=>)  (builder.Render(template.FromName, paramDict)) mailDefaults.FromName;
        Subject = (<=>) (builder.Render(template.Subject, paramDict)) mailDefaults.Subject;
        Content = "<html><body>" + builder.Render(template.Content, paramDict).Replace("<html>","").Replace("<body>","") + "</body></html>";
        OrganizationId = template.OrganizationId
      } 
      message
      