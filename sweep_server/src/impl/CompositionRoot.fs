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

  let [<Literal>] connectionString = "server=localhost;database=sweep_development;user=root;password=MyNewPass"
  let [<Literal>] resPath = __SOURCE_DIRECTORY__ + @"/../../lib"

  type Sql = SqlDataProvider< 
              ConnectionString = connectionString,
              DatabaseVendor = Common.DatabaseProviderTypes.MYSQL,
              ResolutionPath = resPath,
              IndividualsAmount = 1000,
              UseOptionTypes = true>
  
  let addEvent (event:EventModel.Event) userId =
    let ctx = Sql.GetDataContext()
    // let events = ctx.SweepDevelopment.User |> Seq.head
    // // .SweepDevelopment.Event |> Seq.head
    // raise (Exception())
    let loggedEvent = ctx.SweepDevelopment.Event.Create()
    loggedEvent.EventName <- Some("foo")
    loggedEvent.OrganizationId <- Some("some_Org+id")
    ctx.SubmitUpdates()

  let getEvent eventId userId = raise (Exception())
      
  let listEvents (userId:string) : LoggedEventModel.LoggedEvent[] = 
    let ctx = Sql.GetDataContext()
    // let events = ctx.SweepDevelopment.User |> Seq.head
    // // .SweepDevelopment.Event |> Seq.head
    // raise (Exception())
    let foo =
      query {      
        for event in ctx.SweepDevelopment.Loggedevent do
        select (event)
      } |> Seq.toArray
    raise (Exception())
    
               
  let getTemplate id userId = raise (Exception())

  let listTemplates userId = raise (Exception())

  // Users    
  let saveUser id username apiKey orgId = 
    ()
  //raise (Exception())//createItemAsync {Id=id;ApiKey=apiKey;Username=username;Password="";OrganizationId=orgId} |> ignore
  let getUser id = raise (Exception())

