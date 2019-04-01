namespace Sweep

open System
open UserModel
open Microsoft.FSharp.Linq.RuntimeHelpers
open System.Linq.Expressions
open FSharp.Control.AsyncSeqExtensions
open FSharp.Control

module ApiKey = 
  // validate an API key
  let validate apiKey = 
    false
    // <@ (fun user idx  -> not (isNull user.ApiKey)) @> 
    // |> LeafExpressionConverter.QuotationToExpression 
    // |> unbox<Expression<Func<User,int,bool>>>
    // |> CompositionRoot.getItemsAsync<User>
    // |> AsyncSeq.length
    // |> Async.RunSynchronously
    // |> (fun x -> x > int64(0))
  
  let generate () =
    Guid.NewGuid().ToString()