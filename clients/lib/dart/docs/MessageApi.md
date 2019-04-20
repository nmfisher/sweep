# openapi.api.MessageApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *https://app.sweephq.com/1.0.0*

Method | HTTP request | Description
------------- | ------------- | -------------
[**getMessageById**](MessageApi.md#getMessageById) | **GET** /messages/{messageId} | Find message by ID
[**listMessages**](MessageApi.md#listMessages) | **GET** /messages | List all messages


# **getMessageById**
> Message getMessageById(messageId)

Find message by ID

Returns a single message

### Example 
```dart
import 'package:openapi/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new MessageApi();
var messageId = messageId_example; // String | ID of message to return

try { 
    var result = api_instance.getMessageById(messageId);
    print(result);
} catch (e) {
    print("Exception when calling MessageApi->getMessageById: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **messageId** | **String**| ID of message to return | [default to null]

### Return type

[**Message**](Message.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **listMessages**
> List<Message> listMessages(startDate, endDate)

List all messages

Returns a list of messages

### Example 
```dart
import 'package:openapi/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new MessageApi();
var startDate = 2013-10-20T19:20:30+01:00; // DateTime | 
var endDate = 2013-10-20T19:20:30+01:00; // DateTime | 

try { 
    var result = api_instance.listMessages(startDate, endDate);
    print(result);
} catch (e) {
    print("Exception when calling MessageApi->listMessages: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **startDate** | **DateTime**|  | [optional] [default to null]
 **endDate** | **DateTime**|  | [optional] [default to null]

### Return type

[**List<Message>**](Message.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

