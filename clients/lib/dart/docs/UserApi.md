# sweep_api.api.UserApi

## Load the API package
```dart
import 'package:sweep_api/api.dart';
```

All URIs are relative to *https://app.sweephq.com/1.0.0*

Method | HTTP request | Description
------------- | ------------- | -------------
[**deleteUser**](UserApi.md#deleteUser) | **DELETE** /user | Delete user
[**getUserInfo**](UserApi.md#getUserInfo) | **GET** /user | Get user info for the currently authenticated user
[**loginUser**](UserApi.md#loginUser) | **GET** /user/login | Logs user into the system
[**logoutUser**](UserApi.md#logoutUser) | **GET** /user/logout | Logs out current logged in user session
[**updateUser**](UserApi.md#updateUser) | **PUT** /user | Updated user


# **deleteUser**
> deleteUser()

Delete user

This can only be done by the logged in user.

### Example 
```dart
import 'package:sweep_api/api.dart';

var api_instance = new UserApi();

try { 
    api_instance.deleteUser();
} catch (e) {
    print("Exception when calling UserApi->deleteUser: $e\n");
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **getUserInfo**
> User getUserInfo()

Get user info for the currently authenticated user

### Example 
```dart
import 'package:sweep_api/api.dart';
// TODO Configure OAuth2 access token for authorization: Google
//defaultApiClient.getAuthentication<OAuth>('Google').accessToken = 'YOUR_ACCESS_TOKEN';

var api_instance = new UserApi();

try { 
    var result = api_instance.getUserInfo();
    print(result);
} catch (e) {
    print("Exception when calling UserApi->getUserInfo: $e\n");
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**User**](User.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **loginUser**
> String loginUser(username, password)

Logs user into the system

### Example 
```dart
import 'package:sweep_api/api.dart';

var api_instance = new UserApi();
var username = username_example; // String | The user name for login
var password = password_example; // String | The password for login in clear text

try { 
    var result = api_instance.loginUser(username, password);
    print(result);
} catch (e) {
    print("Exception when calling UserApi->loginUser: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **username** | **String**| The user name for login | [default to null]
 **password** | **String**| The password for login in clear text | [default to null]

### Return type

**String**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **logoutUser**
> logoutUser()

Logs out current logged in user session

### Example 
```dart
import 'package:sweep_api/api.dart';

var api_instance = new UserApi();

try { 
    api_instance.logoutUser();
} catch (e) {
    print("Exception when calling UserApi->logoutUser: $e\n");
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **updateUser**
> updateUser(userRequestBody)

Updated user

This can only be done by the logged in user.

### Example 
```dart
import 'package:sweep_api/api.dart';

var api_instance = new UserApi();
var userRequestBody = new UserRequestBody(); // UserRequestBody | 

try { 
    api_instance.updateUser(userRequestBody);
} catch (e) {
    print("Exception when calling UserApi->updateUser: $e\n");
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **userRequestBody** | [**UserRequestBody**](UserRequestBody.md)|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

