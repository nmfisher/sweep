namespace Sweep.Data

open FSharp.Data.Sql

module Sql =
  let [<Literal>] connectionString = "server=localhost;database=sweep_development;user=root;password=MyNewPass"
  let [<Literal>] resPath = __SOURCE_DIRECTORY__ + @"../../../../lib"

  type Sql = SqlDataProvider< 
              ConnectionString = connectionString,
              DatabaseVendor = Common.DatabaseProviderTypes.MYSQL,
              ResolutionPath = resPath,
              IndividualsAmount = 1000,
              UseOptionTypes = true>