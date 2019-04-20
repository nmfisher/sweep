# openapi.api.EventApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *https://app.sweephq.com/1.0.0*

Method | HTTP request | Description
------------- | ------------- | -------------
[**addEvent**](EventApi.md#addEvent) | **POST** /events | Raise an event
[**getEventById**](EventApi.md#getEventById) | **GET** /events/{eventId} | Find raised event by ID
[**listEvents**](EventApi.md#listEvents) | **GET** /events | List all received events


# **addEvent**
> addEvent(eventRequestBody, apiKey)

Raise an event

### Example 
```dart
import 'package:openapi/api.dart';
// TODO Configure API key authorization: api_key
//defaultApiClient.getAuthentication<ApiKeyAuth>('api_key').apiKey = 'YOUR_API_KEY';
// uncomment below to setup prefix (e.g. Bearer) for API key, if needed
//defaultApiClient.getAuthentication<ApiKeyAuth>('api_key').apiKeyPrefix = 'Bearer';

var api_instance = new EventApi();
var eventRequestBody = new EventRequestBody(); // EventRequestBody | 
var apiKey = apiKey_example; // String | 

try { 
    api_instance.addEvent(eventRequestBody, apiKey);
} catch (e) {
    print("Exception when calling EventApi->addEvent: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **eventRequestBody** | [**EventRequestBody**](EventRequestBody.md)|  | 
 **apiKey** | **String**|  | [optional] [default to null]

### Return type

void (empty response body)

### Authorization

[api_key](../README.md#api_key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **getEventById**
> Event getEventById(eventId)

Find raised event by ID

### Example 
```dart
import 'package:openapi/api.dart';

var api_instance = new EventApi();
var eventId = eventId_example; // String | ID of event that needs to be fetched

try { 
    var result = api_instance.getEventById(eventId);
    print(result);
} catch (e) {
    print("Exception when calling EventApi->getEventById: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **eventId** | **String**| ID of event that needs to be fetched | [default to null]

### Return type

[**Event**](Event.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **listEvents**
> List<Event> listEvents(withActions, startDate, endDate)

List all received events

Returns a list of all events

### Example 
```dart
import 'package:openapi/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new EventApi();
var withActions = true; // bool | 
var startDate = 2013-10-20T19:20:30+01:00; // DateTime | 
var endDate = 2013-10-20T19:20:30+01:00; // DateTime | 

try { 
    var result = api_instance.listEvents(withActions, startDate, endDate);
    print(result);
} catch (e) {
    print("Exception when calling EventApi->listEvents: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **withActions** | **bool**|  | [optional] [default to null]
 **startDate** | **DateTime**|  | [optional] [default to null]
 **endDate** | **DateTime**|  | [optional] [default to null]

### Return type

[**List<Event>**](Event.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

