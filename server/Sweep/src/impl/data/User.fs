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

  let save id username orgId = 
    let ctx = GetDataContext()
    let user = ctx.SweepDb.User.Create()
    user.Id <- id
    user.Username <- Some(username)
    user.OrganizationId <- orgId // TODO - need to allow multi-user organizations
    ctx.SubmitUpdates()

  let get id : User option = 
    let ctx = GetDataContext()
    query {
      for user in ctx.SweepDb.User do
      where (user.Id = id)
      select user
    } 
    |> Seq.map (fun x -> x.MapTo<User>(deserialize))
    |> Seq.tryHead
  
  let getByUsername (username:string) : User option = 
    let ctx = GetDataContext()
    query {
      for user in ctx.SweepDb.User do
      where (user.Username = Some(username))
      select user
    } 
    |> Seq.map (fun x -> x.MapTo<User>(deserialize))
    |> Seq.tryHead