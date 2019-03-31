namespace Sweep

open Microsoft.Azure.Documents;
open Microsoft.Azure.Documents.Client;
open Microsoft.Azure.Documents.Linq;
open System
open System.Linq
open System.Linq.Expressions
open FSharp.Control.AsyncSeqExtensions

module Data = 

  let getItemAsync<'T> (client:Microsoft.Azure.Documents.Client.DocumentClient) databaseId collectionId id =
      try 
        client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, id))
      with      
      | :? DocumentClientException as e ->
          if e.StatusCode = Nullable(System.Net.HttpStatusCode.NotFound) then
            null
          else
            raise e          
      | e -> raise e                

  let getItemsAsync<'T> (client:Microsoft.Azure.Documents.Client.DocumentClient) databaseId collectionId (pred:Expression<Func<'T,int,bool>>) = 
      let uri = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId)
      let feedOptions = new FeedOptions()
      feedOptions.MaxItemCount <- Nullable(-1)
      let query = client.CreateDocumentQuery<'T>(uri, feedOptions).Where(pred).AsDocumentQuery();
      asyncSeq {
        while query.HasMoreResults do
            let! results = query.ExecuteNextAsync<'T>() |> Async.AwaitTask
            for result in results do 
              yield result
      }
       
  let createItemAsync<'T> (client:Microsoft.Azure.Documents.Client.DocumentClient) databaseId collectionId item =
    client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), item) |> Async.AwaitTask

  let updateItemAsync<'T> (client:Microsoft.Azure.Documents.Client.DocumentClient) databaseId collectionId id item =
      client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, id), item) |> Async.AwaitTask

  let deleteItemAsync (client:Microsoft.Azure.Documents.Client.DocumentClient) databaseId collectionId id =
    client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, id)) |> Async.AwaitTask

  let getClient endpoint (key:string)  =     
    let uri = Uri(endpoint)
    new DocumentClient(uri, key)
    