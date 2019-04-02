# Sweep

A [Giraffe](https://github.com/giraffe-fsharp/Giraffe) server stub for the Sweep package, created via the [OpenAPI generator](https://github.com/OpenAPITools/openapi-generator/).

## Models

The following models have been auto-generated from the provided OpenAPI schema:

model/EventModel.fs
model/UserModel.fs
model/TemplateModel.fs
model/MessageModel.fs
model/LoggedEventModel.fs
model/ListenerModel.fs

## Operations

Handlers have been auto-generated from the operations specified in the OpenAPI schema as follows:


## Operation Parameters

Types have been generated for the URL, query, form, header and cookie parameters passed to each handler in the following files:

api/EventApiHandlerParams.fs
api/ListenerApiHandlerParams.fs
api/MessageApiHandlerParams.fs
api/TemplateApiHandlerParams.fs
api/UserApiHandlerParams.fs

## Service Interfaces

Handlers will attempt to bind parameters to the applicable type and pass to a Service specific to that Handler. Service interfaces have been generated as follows:

api/EventApiServiceInterface.fs
api/ListenerApiServiceInterface.fs
api/MessageApiServiceInterface.fs
api/TemplateApiServiceInterface.fs
api/UserApiServiceInterface.fs

Each Service contains functions for each [OperationId], each accepting a [OperationId]Params object that wraps the operation's parameters.

If a requestBody is a ref type (i.e. a Model) or a single simple type, the operation parameter will be typed as the expected Model:

`type AddPetBodyParams = Pet`

If a requestBody is a simple type with named properties, the operation parameters will be typed to reflect those properties:

`type AddFooBodyParams = {
  Name:string;
  Age:int
}

Each Service/operation function must accept the [OperationId]Params object and return a [OperationId]Result type. For example:

`type AddPetArgs = { bodyParams:AddPetBodyParams }
type IPetApiService = abstract member AddPet:HttpContext -> AddPetArgs->AddPetResult`

[OperationId]Result is a discriminated union of all possible OpenAPI response types for that operation. 

This means that service implementations can only return status codes that have been declared in the OpenAPI specification. 
However, if the OpenAPI spec declares a default Response for an operation, the service can manually set the status code.

For example:

`type FindPetsByStatusDefaultStatusCodeResponse = { content:Pet[];}
type FindPetsByStatusStatusCode400Response = { content:string; }
type FindPetsByStatusResult = FindPetsByStatusDefaultStatusCode of FindPetsByStatusDefaultStatusCodeResponse | FindPetsByStatusStatusCode400 of FindPetsByStatusStatusCode400Response`

## Note on response codes for URL parameter binding

Giraffe binds URL parameters by requiring compile-time format strings for routes  (e.g. "/foo/%s/%d) or known types (e.g. FooUrlParameters).

With either approach, Giraffe will emit a 400 error response if parameter binding fails (e.g. if a string is provided where an int was expected).

Currently, I am not aware of any way to customize this response, meaning if your OpenAPI schema specifies a different response code for an incorrectly formatted URL parameter, this will basically be ignored.

To ensure your OpenAPI schema and implementation are consistent, I suggest ensuring that your schema only specifies return code 400 for incorrectly formatted URL parameters.

If you have any suggestions for customizing this, please file an issue.

## Service Implementations

Stubbed service implementations of those interfaces have been generated as follows:

impl/EventApiService.fs
impl/ListenerApiService.fs
impl/MessageApiService.fs
impl/TemplateApiService.fs
impl/UserApiService.fs

You should manually edit these files to implement your business logic.

## Additional Handlers

Additional handlers can be configured in the Customization.fs

`let handlers : HttpHandler list = [
    // insert your handlers here
    GET >=> 
      choose [
        route "/login" >=> redirectToLogin
        route "/logout" >=> logout
      ]
  ]`

## Authentication

### OAuth

If your OpenAPI spec contains oAuth2 securitySchemes, these will have been auto-generated.

To configure any of these, you must set the "xxxClientId" and "xxxClientSecret" environment variables (e.g. "GoogleClientId", "GoogleClientSecret") where xxx is the securityScheme ID.

If you specify the securityScheme ID as "Google" or "GitHub" (note the capital "G" and "H" in the latter), the generator will default to:
- for Google, the [ASP.NET Core providers](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-2.2)
- for GitHub, the [aspnet-contrib provider](https://www.nuget.org/packages/AspNet.Security.OAuth.GitHub/)

For any other ID (e.g. "Facebook"), a [generic ASP.NET Core oAuth provider](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.oauthextensions.addoauth?view=aspnetcore-2.2) will be configured.

See impl/AuthSchemes.fs for further details.

### API key

Where applicable, handlers for api_key authentication are also auto-generated. Your validation logic

## TODO/currently unsupported

- form request bodies (URL-encoded or multipart)
- implicit oAuth
- XML content/response types
- http authentication
- testing header params

## .openapi-generator-ignore

It is recommended to add src/impl/** and the project's .fsproj file to the .openapi-generator-ignore file. 

This will allow you to regenerate model, operation and parameter files without overriding your implementations of business logic, authentication, data layers, and so on.

## Build and test the application

### Windows

Run the `build.bat` script in order to restore, build and test (if you've selected to include tests) the application:

```
> ./build.bat
```

### Linux/macOS

Run the `build.sh` script in order to restore, build and test (if you've selected to include tests) the application:

```
$ ./build.sh
```

## Run the application

After a successful build you can start the web application by executing the following command in your terminal:

```
dotnet run --project src/{{packageName}
```

After the application has started visit [http://localhost:5000](http://localhost:5000) in your preferred browser.