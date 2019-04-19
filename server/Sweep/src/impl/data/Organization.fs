namespace Sweep.Data

open Sweep.Model.Organization
open Sql
open Sweep.Exceptions

module Organization =
  
  let deserialize (prop,value:obj) =
    match prop with
      | "Id" ->
        value.ToString() :> obj
      | _ -> 
        value

  let get id = 
    let ctx = GetDataContext()
    let org = 
      query {
        for org in ctx.SweepDb.Organization do
        where (org.Id = id)
        select org
        exactlyOneOrDefault
      } 
    match isNull org with
    | true -> 
      raise (NotFoundException("Not found"))
    | false ->
      org.MapTo<Organization>(deserialize)

  let add id primaryApiKey secondaryApiKey =    
    let ctx = GetDataContext()
    let org = ctx.SweepDb.Organization.Create()
    org.Id <- id
    org.PrimaryApiKey <- primaryApiKey
    org.SecondaryApiKey <- secondaryApiKey
    ctx.SubmitUpdates()

  let findByApiKey apiKey = 
    let ctx = GetDataContext()
    query {
      for org in ctx.SweepDb.Organization do
      where (org.PrimaryApiKey = apiKey || org.SecondaryApiKey = apiKey)
      select org
    } 
    |> Seq.map (fun x -> x.MapTo<Organization>(deserialize))
    |> Seq.tryHead

