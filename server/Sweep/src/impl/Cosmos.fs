namespace Sweep

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
      feedOptions.EnableCrossPartitionQuery <- true
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
    

module Cosmos = 
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
  let addEvent (event:Event.Event) userId = 
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
    <@ (fun (template:Template.Template) idx  -> template.UserId = userId && template.Id = id) @> 
      |> LeafExpressionConverter.QuotationToExpression 
      |> unbox<Expression<Func<Template,int,bool>>>
      |> getItemsAsync<Template> 
      |> AsyncSeq.firstOrDefault

  let listTemplates userId = 
    <@ (fun (template:Template.Template) idx  -> template.UserId = userId) @> 
      |> LeafExpressionConverter.QuotationToExpression 
      |> unbox<Expression<Func<Template,int,bool>>>
      |> getItemsAsync<Template.Template>

  // Users    
  let saveUser id username apiKey orgId = createItemAsync {Id=id;ApiKey=apiKey;Username=username;Password="";OrganizationId=orgId} |> ignore
  let getUser id = getItemAsync<User> id