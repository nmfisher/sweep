namespace Sweep.Data

open Sweep.Model.Message
open Sql
open Sweep.Exceptions
open Newtonsoft.Json

module Message = 

  let deserializeMessage (prop,value) =
    match prop with
     | "Id" ->
        value.ToString() :> obj
     | "SentTo" ->   
        JsonConvert.DeserializeObject<string[]> (value.ToString()) :> obj
     | _ -> 
        value

  let get id organizationId = 
    let ctx = Sql.GetDataContext()
    let row = query {
      for message in ctx.SweepDevelopment.Message do
      where (message.OrganizationId = organizationId && message.Id = id)
      select message
      exactlyOneOrDefault
    } 
    match isNull row with
    | true ->
      raise (NotFoundException("Not found"))
    | false ->        
      row.MapTo<Message>(deserializeMessage)
      
  let list organizationId =
    let ctx = Sql.GetDataContext()
    query {      
      for message in ctx.SweepDevelopment.Message do
      where (message.OrganizationId = organizationId)
      select (message)
    } 
    |> Seq.map (fun x -> x.MapTo<Message>(deserializeMessage))
    |> Seq.toArray