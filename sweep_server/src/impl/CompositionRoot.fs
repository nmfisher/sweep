namespace Sweep

open System
open UserModel
open Microsoft.FSharp.Linq.RuntimeHelpers
open System.Linq.Expressions
open FSharp.Control.AsyncSeqExtensions
open LoggedEventModel
open FSharp.Control
open TemplateModel
open System.Linq.Expressions
open Microsoft.FSharp.Quotations

module CompositionRoot =

  let rec translateExpr (linq:Expression) = 
    match linq with
    | :? MethodCallExpression as mc ->
        let le = mc.Arguments.[0] :?> LambdaExpression
        let args, body = translateExpr le.Body
        le.Parameters.[0] :: args, body
    | _ -> [], linq

  let endpoint = Environment.GetEnvironmentVariable("CosmosEndpoint")
  let key = Environment.GetEnvironmentVariable("CosmosKey")
  let databaseId = Environment.GetEnvironmentVariable("CosmosDatabaseId")
  let collectionId = Environment.GetEnvironmentVariable("CosmosCollectionId")

  if isNull endpoint || isNull key || isNull databaseId || isNull collectionId then
    raise (Exception("Cosmos configuration not properly set"))

  let client = Data.getClient endpoint key

  let getItemAsync<'T> id = Data.getItemAsync<'T> client databaseId collectionId id
  let getItemsAsync<'T> pred = Data.getItemsAsync<'T> client databaseId collectionId pred
  let createItemAsync item = Data.createItemAsync<'T> client databaseId collectionId item
  let updateItemAsync<'T> id item = Data.updateItemAsync<'T> client databaseId collectionId id item
  let deleteItemAsync client id = Data.deleteItemAsync client databaseId collectionId id

  // Events
  let addEvent (event:EventModel.Event) userId = 
    let id = (Guid.NewGuid().ToString())
    let loggedEvent = { 
      EventName=event.EventName;
      Params=event.Params;
      Id=id;
      UserId=userId; 
      id=id;
    }
    createItemAsync loggedEvent |> ignore
    loggedEvent
    
  let getEvent eventId userId = 
      getItemAsync<
      

  let listEvents userId = 
    <@ (fun (event:LoggedEvent) (idx:int) -> event.UserId = userId) @>
    |> LeafExpressionConverter.QuotationToExpression
    |> translateExpr
    |> (fun (args, body) -> Expression.Lambda<Func<LoggedEvent, int, bool>>(body, Array.ofSeq args))
    |> getItemsAsync<LoggedEvent> 
    |> AsyncSeq.toArray
               
  let getTemplate id userId = 
    <@ (fun (template:TemplateModel.Template) idx  -> template.UserId = userId && template.Id = id) @> 
      |> LeafExpressionConverter.QuotationToExpression 
      |> unbox<Expression<Func<Template,int,bool>>>
      |> getItemsAsync<Template> 
      |> AsyncSeq.firstOrDefault

  let listTemplates userId = 
    <@ (fun (template:TemplateModel.Template) idx  -> template.UserId = userId) @> 
      |> LeafExpressionConverter.QuotationToExpression 
      |> unbox<Expression<Func<Template,int,bool>>>
      |> getItemsAsync<TemplateModel.Template>

  // Users    
  let saveUser id username apiKey orgId = createItemAsync {Id=id;ApiKey=apiKey;Username=username;Password="";OrganizationId=orgId} |> ignore
  let getUser id = getItemAsync<User> id