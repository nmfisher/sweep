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
open FSharp.Data.Sql


module CompositionRoot =
  
  let serialize = Newtonsoft.Json.JsonConvert.SerializeObject

  let [<Literal>] connectionString = "server=localhost;database=sweep_development;user=root;password=MyNewPass"
  let [<Literal>] resPath = __SOURCE_DIRECTORY__ + @"/../../lib"

  type Sql = SqlDataProvider< 
              ConnectionString = connectionString,
              DatabaseVendor = Common.DatabaseProviderTypes.MYSQL,
              ResolutionPath = resPath,
              IndividualsAmount = 1000,
              UseOptionTypes = true>
  
  let addEvent (event:EventModel.Event) organizationId =
    let ctx = Sql.GetDataContext()
    let loggedEvent = ctx.SweepDevelopment.Loggedevent.Create()
    loggedEvent.EventName <- event.EventName
    loggedEvent.Params <- event.Params |> serialize |> Some
    loggedEvent.OrganizationId <- organizationId
    loggedEvent.Id <- Guid.NewGuid().ToString()
    ctx.SubmitUpdates()

  let getEvent eventId organizationId : LoggedEvent option = 
    let ctx = Sql.GetDataContext()
    query {
      for event in ctx.SweepDevelopment.Loggedevent do
      where (event.OrganizationId = organizationId && event.Id = eventId)
      select event
    } 
    |> Seq.map (fun x -> x.MapTo<LoggedEvent>())
    |> Seq.tryHead
      
  let listEvents (organizationId:string) : seq<LoggedEvent> =
    let ctx = Sql.GetDataContext()
    query {      
      for event in ctx.SweepDevelopment.Loggedevent do
      where (event.OrganizationId = organizationId)
      select (event)
    } |> Seq.map (fun x -> x.MapTo<LoggedEvent>()) 
               
  let getTemplate id organizationId = 
    let ctx = Sql.GetDataContext()
    query {      
      for template in ctx.SweepDevelopment.Template do
      where (template.Id = id && template.OrganizationId = organizationId)
      select (template)
    } |> Seq.map (fun x -> x.MapTo<Template>())
    |> Seq.tryHead

  let listTemplates organizationId =
    let ctx = Sql.GetDataContext()
    query {      
      for template in ctx.SweepDevelopment.Template do
      where (template.OrganizationId = organizationId)
      select (template)
    } |> Seq.map (fun x -> x.MapTo<LoggedEvent>())

  // Users    
  let saveUser id username apiKey orgId = 
    let ctx = Sql.GetDataContext()
    let user = ctx.SweepDevelopment.User.Create()
    user.Id <- id
    ctx.SubmitUpdates()

  //raise (Exception())//createItemAsync {Id=id;ApiKey=apiKey;Username=username;Password="";OrganizationId=orgId} |> ignore
  let getUser id : User option = 
    let ctx = Sql.GetDataContext()
    query {
      for user in ctx.SweepDevelopment.User do
      where (user.Id = id)
      select user
    } 
    |> Seq.map (fun x -> x.MapTo<User>())
    |> Seq.tryHead

  let saveOrganization id =
    let ctx = Sql.GetDataContext()
    let org = ctx.SweepDevelopment.Organization.Create()
    org.Id <- id
    ctx.SubmitUpdates()