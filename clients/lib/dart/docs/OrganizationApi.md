# openapi.api.OrganizationApi

## Load the API package
```dart
import 'package:openapi/api.dart';
```

All URIs are relative to *https://app.sweephq.com/1.0.0*

Method | HTTP request | Description
------------- | ------------- | -------------
[**getOrganizationInfo**](OrganizationApi.md#getOrganizationInfo) | **GET** /organization | Get organization info for the currently authenticated context


# **getOrganizationInfo**
> Organization getOrganizationInfo()

Get organization info for the currently authenticated context

### Example 
```dart
import 'package:openapi/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new OrganizationApi();

try { 
    var result = api_instance.getOrganizationInfo();
    print(result);
} catch (e) {
    print("Exception when calling OrganizationApi->getOrganizationInfo: $e\n");
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**Organization**](Organization.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

