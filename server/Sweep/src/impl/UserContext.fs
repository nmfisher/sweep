namespace Sweep

open System.Collections.Generic
open System.Security.Claims

module UserContext = 
  
  let getUserId (claims:IEnumerable<Claim>) = 
    claims
    |> Seq.where (fun (c:Claim) -> c.Type = ClaimTypes.Email)
    |> Seq.head
    |> (fun (c:Claim) -> c.Value)

  let getOrgId (claims:IEnumerable<Claim>) = 
    claims
    |> Seq.where (fun (c:Claim) -> c.Type = ClaimTypes.GroupSid)
    |> Seq.head
    |> (fun (c:Claim) -> c.Value)

  let getApiKey (claims:IEnumerable<Claim>) =
    claims
    |> Seq.where (fun (c:Claim) -> c.Type = "apiKey")
    |> Seq.head
    |> (fun (c:Claim) -> c.Value)