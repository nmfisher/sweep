namespace Sweep.Data

open System
open Sweep.Model.Template
open Sql
open Sweep.Exceptions
open Newtonsoft.Json

module Template = 

  let deserializeTemplate (prop,value:obj) =
   match prop with
    | "Id" ->
      value.ToString() :> obj
    | "SendTo" ->
      JsonConvert.DeserializeObject<string[]> (value.ToString()) :> obj
    | _ -> 
      value

  let add content subject fromName fromAddress sendTo organizationId userId : Template =
    let ctx = GetDataContext()
    let template = ctx.SweepDb.Template.Create()
    let id = Guid.NewGuid().ToString()
    template.Content <- content
    template.Subject <- Some(subject)
    template.FromName <- Some(fromName)
    template.FromAddress <- Some(fromAddress)
    template.SendTo <- JsonConvert.SerializeObject sendTo
    template.OrganizationId <- organizationId
    template.UserId <- userId
    template.Id <- id
    ctx.SubmitUpdates()
    {
      Template.Id = id;
      Content = content;
      Subject = subject;
      FromName = fromName;
      FromAddress = fromAddress;
      SendTo = sendTo;
      OrganizationId = organizationId;
      UserId = userId;
      Deleted = false
    }

  let delete id orgId userId =
    let ctx = GetDataContext()
    let row = query {
      for template in ctx.SweepDb.Template do
      where (template.Id = id && template.OrganizationId = orgId)
      select template
      exactlyOneOrDefault
    } 
    match (isNull row) with 
    | true ->
      raise (NotFoundException("Template not found"))      
    | _ ->
      row.Deleted <- Some((sbyte)1)
      ctx.SubmitUpdates()
               
  let get id organizationId = 
    let ctx = GetDataContext()
    let row = query {      
      for template in ctx.SweepDb.Template do
      where (template.Id = id && template.OrganizationId = organizationId && (template.Deleted.IsNone || (template.Deleted.IsSome && template.Deleted.Value = sbyte(0))))
      select (template)
      exactlyOneOrDefault
    } 
    if isNull row then
      raise (NotFoundException("Not found"))
    else
      row.MapTo<Template>(deserializeTemplate)

  let update id content subject fromName fromAddress sendTo organizationId = 
    let ctx = GetDataContext()
    let row = query {      
      for template in ctx.SweepDb.Template do
      where (template.Id = id && template.OrganizationId = organizationId)
      select (template)
      exactlyOneOrDefault
    } 
    if isNull row then
      raise (NotFoundException("Not found"))
    else
      row.SendTo <- JsonConvert.SerializeObject sendTo
      row.Content <- content
      row.Subject <- Some(subject)
      row.FromAddress <- Some(fromAddress)
      row.FromName <- Some(fromName)
      ctx.SubmitUpdates()
      row.MapTo<Template>(deserializeTemplate)

  let list organizationId =
    let ctx = GetDataContext()
    query {      
      for template in ctx.SweepDb.Template do
      where (template.OrganizationId = organizationId && (template.Deleted.IsNone || (template.Deleted.IsSome && template.Deleted.Value = sbyte(0))))
      select (template)
    } |> Seq.map (fun x -> x.MapTo<Template>(deserializeTemplate))
      |> Seq.toArray