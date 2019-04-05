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


  let add (content:string) (sendTo:string[]) (organizationId:string) (userId:string) =
    let ctx = Sql.GetDataContext()
    let template = ctx.SweepDevelopment.Template.Create()
    template.Content <- content
    template.SendTo <- JsonConvert.SerializeObject sendTo
    template.OrganizationId <- organizationId
    template.UserId <- userId
    template.Id <- Guid.NewGuid().ToString()
    ctx.SubmitUpdates()

  let delete id orgId userId =
    let ctx = Sql.GetDataContext()
    let row = query {
      for template in ctx.SweepDevelopment.Template do
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
    let ctx = Sql.GetDataContext()
    let row = query {      
      for template in ctx.SweepDevelopment.Template do
      where (template.Id = id && template.OrganizationId = organizationId && (template.Deleted.IsNone || (template.Deleted.IsSome && template.Deleted.Value = sbyte(0))))
      select (template)
      exactlyOneOrDefault
    } 
    if isNull row then
      raise (NotFoundException("Not found"))
    else
      row.MapTo<Template>(deserializeTemplate)

  let update id content sendTo organizationId = 
    let ctx = Sql.GetDataContext()
    let row = query {      
      for template in ctx.SweepDevelopment.Template do
      where (template.Id = id && template.OrganizationId = organizationId)
      select (template)
      exactlyOneOrDefault
    } 
    if isNull row then
      raise (NotFoundException("Not found"))
    else
      row.SendTo <- JsonConvert.SerializeObject sendTo
      row.Content <- content
      ctx.SubmitUpdates()

  let list organizationId =
    let ctx = Sql.GetDataContext()
    query {      
      for template in ctx.SweepDevelopment.Template do
      where (template.OrganizationId = organizationId && (template.Deleted.IsNone || (template.Deleted.IsSome && template.Deleted.Value = sbyte(0))))
      select (template)
    } |> Seq.map (fun x -> x.MapTo<Template>(deserializeTemplate))
      |> Seq.toArray