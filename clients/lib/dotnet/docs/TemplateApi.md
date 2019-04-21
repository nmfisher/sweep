# Sweep.Api.TemplateApi

All URIs are relative to *https://app.sweephq.com/1.0.0*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddTemplate**](TemplateApi.md#addtemplate) | **POST** /templates | Create a new Template
[**DeleteTemplate**](TemplateApi.md#deletetemplate) | **DELETE** /templates/{templateId} | Deletes a Template
[**GetTemplateById**](TemplateApi.md#gettemplatebyid) | **GET** /templates/{templateId} | Find Template by ID
[**ListTemplate**](TemplateApi.md#listtemplate) | **GET** /templates | List all Templates
[**RenderTemplate**](TemplateApi.md#rendertemplate) | **POST** /templates/{templateId}/render | Renders a template using the provided event parameters
[**UpdateTemplate**](TemplateApi.md#updatetemplate) | **PUT** /templates/{templateId} | Update an existing Template


<a name="addtemplate"></a>
# **AddTemplate**
> Template AddTemplate (TemplateRequestBody templateRequestBody)

Create a new Template

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class AddTemplateExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TemplateApi();
            var templateRequestBody = new TemplateRequestBody(); // TemplateRequestBody | 

            try
            {
                // Create a new Template
                Template result = apiInstance.AddTemplate(templateRequestBody);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TemplateApi.AddTemplate: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **templateRequestBody** | [**TemplateRequestBody**](TemplateRequestBody.md)|  | 

### Return type

[**Template**](Template.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deletetemplate"></a>
# **DeleteTemplate**
> void DeleteTemplate (string templateId)

Deletes a Template

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class DeleteTemplateExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TemplateApi();
            var templateId = templateId_example;  // string | Template id to delete

            try
            {
                // Deletes a Template
                apiInstance.DeleteTemplate(templateId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TemplateApi.DeleteTemplate: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **templateId** | **string**| Template id to delete | 

### Return type

void (empty response body)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gettemplatebyid"></a>
# **GetTemplateById**
> Template GetTemplateById (string templateId)

Find Template by ID

Returns a single template

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class GetTemplateByIdExample
    {
        public void main()
        {
            var apiInstance = new TemplateApi();
            var templateId = templateId_example;  // string | ID of template to return

            try
            {
                // Find Template by ID
                Template result = apiInstance.GetTemplateById(templateId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TemplateApi.GetTemplateById: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **templateId** | **string**| ID of template to return | 

### Return type

[**Template**](Template.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listtemplate"></a>
# **ListTemplate**
> List<Template> ListTemplate ()

List all Templates

Returns a list of templates

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class ListTemplateExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TemplateApi();

            try
            {
                // List all Templates
                List&lt;Template&gt; result = apiInstance.ListTemplate();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TemplateApi.ListTemplate: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<Template>**](Template.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="rendertemplate"></a>
# **RenderTemplate**
> Message RenderTemplate (string templateId, RenderTemplateRequestBody renderTemplateRequestBody)

Renders a template using the provided event parameters

Returns a string representing the HTML content of an email to be sent

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class RenderTemplateExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TemplateApi();
            var templateId = templateId_example;  // string | ID of template to return
            var renderTemplateRequestBody = new RenderTemplateRequestBody(); // RenderTemplateRequestBody | The event parameters used to render

            try
            {
                // Renders a template using the provided event parameters
                Message result = apiInstance.RenderTemplate(templateId, renderTemplateRequestBody);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TemplateApi.RenderTemplate: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **templateId** | **string**| ID of template to return | 
 **renderTemplateRequestBody** | [**RenderTemplateRequestBody**](RenderTemplateRequestBody.md)| The event parameters used to render | 

### Return type

[**Message**](Message.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatetemplate"></a>
# **UpdateTemplate**
> Template UpdateTemplate (string templateId, TemplateRequestBody templateRequestBody)

Update an existing Template

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class UpdateTemplateExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new TemplateApi();
            var templateId = templateId_example;  // string | ID of template to return
            var templateRequestBody = new TemplateRequestBody(); // TemplateRequestBody | successful operation

            try
            {
                // Update an existing Template
                Template result = apiInstance.UpdateTemplate(templateId, templateRequestBody);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TemplateApi.UpdateTemplate: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **templateId** | **string**| ID of template to return | 
 **templateRequestBody** | [**TemplateRequestBody**](TemplateRequestBody.md)| successful operation | 

### Return type

[**Template**](Template.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

