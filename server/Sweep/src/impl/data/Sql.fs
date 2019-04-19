namespace Sweep.Data

open System
open FSharp.Data.Sql

module Sql =

  let [<Literal>] resPath = __SOURCE_DIRECTORY__ + @"../../../../lib"
  
  #if DEBUG
  FSharp.Data.Sql.Common.QueryEvents.SqlQueryEvent |> Event.add (printfn "Executing SQL: %O")
  #endif
  
  type Sql = SqlDataProvider< 
                            ConnectionString = "server=localhost;database=sweep_db;user=root;password=MyNewPass",
                            DatabaseVendor = Common.DatabaseProviderTypes.MYSQL,
                            ResolutionPath = resPath,
                            Owner="sweep_db",
                            IndividualsAmount = 1000,
                            UseOptionTypes = true>


  let GetDataContext () = 
    Sql.GetDataContext(Environment.GetEnvironmentVariable("ConnectionString"))

    
    