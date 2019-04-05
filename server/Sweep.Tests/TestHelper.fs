namespace Sweep.Tests

open System
open System.Net
open System.Net.Http
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.TestHost
open FSharp.Control.Tasks.V2.ContextInsensitive
open Xunit
open System.Text
open Microsoft.Extensions.Configuration
open System.Security.Claims
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Logging
open Giraffe
open Microsoft.AspNetCore.Identity
open FSharp.Data.Sql.Providers
open Microsoft.Extensions.DependencyInjection

module TestHelper = 

  let dbLock = obj()

  let encode x = 
    x 
    |> Newtonsoft.Json.JsonConvert.SerializeObject 
    |> Encoding.UTF8.GetBytes 
    |> (fun x -> new MemoryStream(x))
    |> (fun x -> new StreamContent(x))

  type AuthMiddleware (next: RequestDelegate) =

    member __.Invoke (ctx : HttpContext) =
        task {
            let claims = [new Claim(ClaimTypes.Email, "user");
                          new Claim(ClaimTypes.NameIdentifier, "userId");
                          new Claim(ClaimTypes.GroupSid, "orgId");]
            let claimsIdentity = ClaimsIdentity(claims, "Cookies")
            let principal = ClaimsPrincipal()
            ctx.User <- principal
            ctx.User.AddIdentity(claimsIdentity)
            return! next.Invoke ctx
        }

  // ---------------------------------
  // Test server/client setup
  // ---------------------------------

  let createHost() =
    let configBuilder = ConfigurationBuilder()
    configBuilder.AddEnvironmentVariables() |> ignore
      
    WebHostBuilder()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .Configure(fun appBuilder -> 
          appBuilder.UseMiddleware<AuthMiddleware>() |> ignore
          Sweep.App.configureApp appBuilder
        ).ConfigureServices(Action<IServiceCollection> Sweep.App.configureServices)
        .UseConfiguration(configBuilder.Build())


  // ---------------------------------
  // Helper functions
  // ---------------------------------

  let HttpGet (client : HttpClient) (path : string) =
      client.GetAsync path
      |> Async.AwaitTask
      |> Async.RunSynchronously

  let HttpPost (client: HttpClient) (path : string) content  =
      client.PostAsync(path, content)
      |> Async.AwaitTask
      |> Async.RunSynchronously

  let HttpPut (client: HttpClient)  (path : string) content =
      client.PutAsync(path, content)
      |> Async.AwaitTask
      |> Async.RunSynchronously

  let HttpDelete (client: HttpClient)  (path : string) =
      client.DeleteAsync(path)
      |> Async.AwaitTask
      |> Async.RunSynchronously

  let createRequest (method : HttpMethod) (path : string) =
      let url = "http://127.0.0.1" + path
      new HttpRequestMessage(method, url)

  let addCookiesFromResponse (response : HttpResponseMessage)
                            (request  : HttpRequestMessage) =
      request.Headers.Add("Cookie", response.Headers.GetValues("Set-Cookie"))
      request

  let makeRequest (client : HttpClient) request =
      request
      |> client.SendAsync

  let isStatus (code : HttpStatusCode) (response : HttpResponseMessage) =
      Assert.Equal(code, response.StatusCode)
      response

  let isOfType (contentType : string) (response : HttpResponseMessage) =
      Assert.Equal(contentType, response.Content.Headers.ContentType.MediaType)
      response

  let readText (response : HttpResponseMessage) =
      response.Content.ReadAsStringAsync()
      |> Async.AwaitTask            
      |> Async.RunSynchronously

  let shouldEqual (expected:obj) (actual:obj) =
      Assert.Equal(expected, actual)
      actual

  let shouldBeLength length (obj:'a[]) = 
      Assert.Equal(length, obj.Length)
      obj


  let getConverter mediaType = 
    (fun (x:string) -> 
      match mediaType with
      | "application/x-www-form-urlencoded" -> raise (NotSupportedException()) // TODO - implement FormUrlEncodedContent
      | _ -> x |> Encoding.UTF8.GetBytes |> (fun x -> new MemoryStream(x)) |> (fun x -> new StreamContent(x)))

  let conn  = MySql.createConnection "server=localhost;database=sweep_development;user=root;password=MyNewPass;Allow User Variables=true"
  conn.Open() |> ignore

  let initialize () = 
    let mutable cmd = MySql.createCommand "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'sweep_development'" conn
    let reader = cmd.ExecuteReader()
    let tables = 
          seq {
              while reader.Read() do
                yield (reader.GetValue(0).ToString())
          } |> Seq.toList
    reader.Dispose()
    cmd.Dispose()
    for table in tables do
      cmd <- MySql.createCommand ("DELETE FROM " + table) conn
      cmd.ExecuteNonQuery() |> ignore
      cmd.Dispose()
    ()
  
  let execute query =
      let cmd = MySql.createCommand query conn
      cmd.ExecuteNonQuery() |> ignore
      cmd.Dispose()