namespace Sweep.Data

open System
open Sweep.Model.Template
open Sql
open Sweep.Exceptions
open Newtonsoft.Json
open Sweep.Model.User

module User = 

  let deserialize (prop,value:obj) =
   match prop with
    | "Id" ->
      value.ToString() :> obj
    | "Password" ->
      "" :> obj
    | _ -> 
      value

  let save id username (apiKey:string) orgId = 
    let ctx = Sql.GetDataContext()
    let user = ctx.SweepDevelopment.User.Create()
    user.ApiKey <- apiKey
    user.Id <- id
    user.OrganizationId <- id // TODO - need to allow multi-user organizations
    ctx.SubmitUpdates()

  let get id : User option = 
    let ctx = Sql.GetDataContext()
    query {
      for user in ctx.SweepDevelopment.User do
      where (user.Id = id)
      select user
    } 
    |> Seq.map (fun x -> x.MapTo<User>(deserialize))
    |> Seq.tryHead

  let findByApiKey apiKey = 
    let ctx = Sql.GetDataContext()
    query {
      for user in ctx.SweepDevelopment.User do
      where (user.ApiKey = apiKey)
      select user
    } 
    |> Seq.map (fun x -> x.MapTo<User>(deserialize))
    |> Seq.tryHead
  