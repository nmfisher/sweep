namespace Sweep

open System
open UserModel
open Microsoft.FSharp.Linq.RuntimeHelpers
open System.Linq.Expressions
open FSharp.Control.AsyncSeqExtensions

module CompositionRoot =

  let endpoint = Environment.GetEnvironmentVariable("CosmosEndpoint")
  let key = Environment.GetEnvironmentVariable("CosmosKey")
  let databaseId = Environment.GetEnvironmentVariable("CosmosDatabaseId")
  let collectionId = Environment.GetEnvironmentVariable("CosmosCollectionId")

  let client = Data.getClient endpoint key

  let getItemAsync<'T> id = Data.getItemAsync<'T> client databaseId collectionId id
  let getItemsAsync<'T> pred = Data.getItemsAsync<'T> client databaseId collectionId pred
  let createItemAsync<'T> item = Data.createItemAsync<'T> client databaseId collectionId item
  let updateItemAsync<'T> id item = Data.updateItemAsync<'T> client databaseId collectionId id item
  let deleteItemAsync client id = Data.deleteItemAsync client databaseId collectionId id

  let saveUser id username apiKey = createItemAsync<User> {Id=id;ApiKey=apiKey;Username=username;Password=""}
  let getUser id = getItemAsync<User> id
  let getEvent id = getItemAsync<EventModel.Event> id
  let saveEvent event userId = createItemAsync<EventModel.Event> event
  
  let getEvent id userId = 
    <@ (fun (event:EventModel.Event) idx  -> event.UserId = userId && event.Id = id) @> 
      |> LeafExpressionConverter.QuotationToExpression 
      |> unbox<Expression<Func<EventModel.Event,int,bool>>>
      |> getItemsAsync<EventModel.Event> 
      |> AsyncSeq.head

  let listEvents userId = 
    <@ (fun (event:EventModel.Event) idx  -> event.UserId = userId) @> 
      |> LeafExpressionConverter.QuotationToExpression 
      |> unbox<Expression<Func<EventModel.Event,int,bool>>>
      |> getItemsAsync<EventModel.Event>

  let getTemplate id userId = 
    <@ (fun (template:TemplateModel.Template) idx  -> template.UserId = userId && template.Id = id) @> 
      |> LeafExpressionConverter.QuotationToExpression 
      |> unbox<Expression<Func<TemplateModel.Template,int,bool>>>
      |> getItemsAsync<TemplateModel.Template> 
      |> Seq.head

  let listTemplates userId = 
    <@ (fun (template:TemplateModel.Template) idx  -> template.UserId = userId) @> 
      |> LeafExpressionConverter.QuotationToExpression 
      |> unbox<Expression<Func<TemplateModel.Template,int,bool>>>
      |> getItemsAsync<TemplateModel.Template>
