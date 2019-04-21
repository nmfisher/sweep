# sweep_api.api.ListenerApi

## Load the API package
```dart
import 'package:sweep_api/api.dart';
```

All URIs are relative to *https://app.sweephq.com/1.0.0*

Method | HTTP request | Description
------------- | ------------- | -------------
[**addListener**](ListenerApi.md#addListener) | **POST** /listeners | Create a new Listener
[**addListenerTemplate**](ListenerApi.md#addListenerTemplate) | **POST** /listeners/{listenerId}/templates/{templateId} | Associates a Template to a Listener
[**deleteListener**](ListenerApi.md#deleteListener) | **DELETE** /listeners/{listenerId} | Deletes a Listener
[**deleteListenerTemplate**](ListenerApi.md#deleteListenerTemplate) | **DELETE** /listeners/{listenerId}/templates/{templateId} | Disassociates a Template from a Listener
[**getListener**](ListenerApi.md#getListener) | **GET** /listeners/{listenerId} | Get a listener by ID
[**listListenerTemplates**](ListenerApi.md#listListenerTemplates) | **GET** /listeners/{listenerId}/templates | List Templates for Listener
[**listListeners**](ListenerApi.md#listListeners) | **GET** /listeners | List all Listeners
[**listMessagesForAction**](ListenerApi.md#listMessagesForAction) | **GET** /actions/{listenerActionId}/messages | List all messages
[**updateListener**](ListenerApi.md#updateListener) | **PUT** /listeners/{listenerId} | Updates a Listener


# **addListener**
> Listener addListener(listenerRequestBody)

Create a new Listener

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new ListenerApi();
var listenerRequestBody = new ListenerRequestBody(); // ListenerRequestBody | 

try { 
    var result = api_instance.addListener(listenerRequestBody);
    print(result);
} catch (e) {
    print("Exception when calling ListenerApi->addListener: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerRequestBody** | [**ListenerRequestBody**](ListenerRequestBody.md)|  | 

### Return type

[**Listener**](Listener.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **addListenerTemplate**
> addListenerTemplate(listenerId, templateId)

Associates a Template to a Listener

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new ListenerApi();
var listenerId = listenerId_example; // String | Listener id to disassociate
var templateId = templateId_example; // String | Template id to associate with the Listener

try { 
    api_instance.addListenerTemplate(listenerId, templateId);
} catch (e) {
    print("Exception when calling ListenerApi->addListenerTemplate: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerId** | **String**| Listener id to disassociate | [default to null]
 **templateId** | **String**| Template id to associate with the Listener | [default to null]

### Return type

void (empty response body)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **deleteListener**
> deleteListener(listenerId)

Deletes a Listener

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new ListenerApi();
var listenerId = listenerId_example; // String | ID of listener to return

try { 
    api_instance.deleteListener(listenerId);
} catch (e) {
    print("Exception when calling ListenerApi->deleteListener: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerId** | **String**| ID of listener to return | [default to null]

### Return type

void (empty response body)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **deleteListenerTemplate**
> deleteListenerTemplate(listenerId, templateId)

Disassociates a Template from a Listener

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new ListenerApi();
var listenerId = listenerId_example; // String | Listener id to disassociate
var templateId = templateId_example; // String | Template id to delete

try { 
    api_instance.deleteListenerTemplate(listenerId, templateId);
} catch (e) {
    print("Exception when calling ListenerApi->deleteListenerTemplate: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerId** | **String**| Listener id to disassociate | [default to null]
 **templateId** | **String**| Template id to delete | [default to null]

### Return type

void (empty response body)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **getListener**
> Listener getListener(listenerId)

Get a listener by ID

Returns a listener

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new ListenerApi();
var listenerId = listenerId_example; // String | ID of listener to update

try { 
    var result = api_instance.getListener(listenerId);
    print(result);
} catch (e) {
    print("Exception when calling ListenerApi->getListener: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerId** | **String**| ID of listener to update | [default to null]

### Return type

[**Listener**](Listener.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **listListenerTemplates**
> List<ListenerTemplate> listListenerTemplates(listenerId)

List Templates for Listener

Returns a list of templates associated with this listener

### Example 
```dart
import 'package:sweep_api/api.dart';

var api_instance = new ListenerApi();
var listenerId = listenerId_example; // String | ID of listener

try { 
    var result = api_instance.listListenerTemplates(listenerId);
    print(result);
} catch (e) {
    print("Exception when calling ListenerApi->listListenerTemplates: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerId** | **String**| ID of listener | [default to null]

### Return type

[**List<ListenerTemplate>**](ListenerTemplate.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **listListeners**
> List<Listener> listListeners()

List all Listeners

Returns a list of Listeners

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new ListenerApi();

try { 
    var result = api_instance.listListeners();
    print(result);
} catch (e) {
    print("Exception when calling ListenerApi->listListeners: $e\n");
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<Listener>**](Listener.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **listMessagesForAction**
> List<Message> listMessagesForAction(listenerActionId)

List all messages

Returns a list of messages for the given ListenerAction

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new ListenerApi();
var listenerActionId = listenerActionId_example; // String | The id of the ListenerAction to limit the results

try { 
    var result = api_instance.listMessagesForAction(listenerActionId);
    print(result);
} catch (e) {
    print("Exception when calling ListenerApi->listMessagesForAction: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerActionId** | **String**| The id of the ListenerAction to limit the results | [default to null]

### Return type

[**List<Message>**](Message.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **updateListener**
> Listener updateListener(listenerId, listenerRequestBody)

Updates a Listener

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new ListenerApi();
var listenerId = listenerId_example; // String | ID of listener to update
var listenerRequestBody = new ListenerRequestBody(); // ListenerRequestBody | 

try { 
    var result = api_instance.updateListener(listenerId, listenerRequestBody);
    print(result);
} catch (e) {
    print("Exception when calling ListenerApi->updateListener: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerId** | **String**| ID of listener to update | [default to null]
 **listenerRequestBody** | [**ListenerRequestBody**](ListenerRequestBody.md)|  | 

### Return type

[**Listener**](Listener.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

