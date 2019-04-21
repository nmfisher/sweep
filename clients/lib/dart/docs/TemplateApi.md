# sweep_api.api.TemplateApi

## Load the API package
```dart
import 'package:sweep_api/api.dart';
```

All URIs are relative to *https://app.sweephq.com/1.0.0*

Method | HTTP request | Description
------------- | ------------- | -------------
[**addTemplate**](TemplateApi.md#addTemplate) | **POST** /templates | Create a new Template
[**deleteTemplate**](TemplateApi.md#deleteTemplate) | **DELETE** /templates/{templateId} | Deletes a Template
[**getTemplateById**](TemplateApi.md#getTemplateById) | **GET** /templates/{templateId} | Find Template by ID
[**listTemplate**](TemplateApi.md#listTemplate) | **GET** /templates | List all Templates
[**renderTemplate**](TemplateApi.md#renderTemplate) | **POST** /templates/{templateId}/render | Renders a template using the provided event parameters
[**updateTemplate**](TemplateApi.md#updateTemplate) | **PUT** /templates/{templateId} | Update an existing Template


# **addTemplate**
> Template addTemplate(templateRequestBody)

Create a new Template

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new TemplateApi();
var templateRequestBody = new TemplateRequestBody(); // TemplateRequestBody | 

try { 
    var result = api_instance.addTemplate(templateRequestBody);
    print(result);
} catch (e) {
    print("Exception when calling TemplateApi->addTemplate: $e\n");
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

# **deleteTemplate**
> deleteTemplate(templateId)

Deletes a Template

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new TemplateApi();
var templateId = templateId_example; // String | Template id to delete

try { 
    api_instance.deleteTemplate(templateId);
} catch (e) {
    print("Exception when calling TemplateApi->deleteTemplate: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **templateId** | **String**| Template id to delete | [default to null]

### Return type

void (empty response body)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **getTemplateById**
> Template getTemplateById(templateId)

Find Template by ID

Returns a single template

### Example 
```dart
import 'package:sweep_api/api.dart';

var api_instance = new TemplateApi();
var templateId = templateId_example; // String | ID of template to return

try { 
    var result = api_instance.getTemplateById(templateId);
    print(result);
} catch (e) {
    print("Exception when calling TemplateApi->getTemplateById: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **templateId** | **String**| ID of template to return | [default to null]

### Return type

[**Template**](Template.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **listTemplate**
> List<Template> listTemplate()

List all Templates

Returns a list of templates

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new TemplateApi();

try { 
    var result = api_instance.listTemplate();
    print(result);
} catch (e) {
    print("Exception when calling TemplateApi->listTemplate: $e\n");
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

# **renderTemplate**
> Message renderTemplate(templateId, renderTemplateRequestBody)

Renders a template using the provided event parameters

Returns a string representing the HTML content of an email to be sent

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new TemplateApi();
var templateId = templateId_example; // String | ID of template to return
var renderTemplateRequestBody = new RenderTemplateRequestBody(); // RenderTemplateRequestBody | The event parameters used to render

try { 
    var result = api_instance.renderTemplate(templateId, renderTemplateRequestBody);
    print(result);
} catch (e) {
    print("Exception when calling TemplateApi->renderTemplate: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **templateId** | **String**| ID of template to return | [default to null]
 **renderTemplateRequestBody** | [**RenderTemplateRequestBody**](RenderTemplateRequestBody.md)| The event parameters used to render | 

### Return type

[**Message**](Message.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **updateTemplate**
> Template updateTemplate(templateId, templateRequestBody)

Update an existing Template

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new TemplateApi();
var templateId = templateId_example; // String | ID of template to return
var templateRequestBody = new TemplateRequestBody(); // TemplateRequestBody | successful operation

try { 
    var result = api_instance.updateTemplate(templateId, templateRequestBody);
    print(result);
} catch (e) {
    print("Exception when calling TemplateApi->updateTemplate: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **templateId** | **String**| ID of template to return | [default to null]
 **templateRequestBody** | [**TemplateRequestBody**](TemplateRequestBody.md)| successful operation | 

### Return type

[**Template**](Template.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

