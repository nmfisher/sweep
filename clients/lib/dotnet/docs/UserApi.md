# Sweep.Api.UserApi

All URIs are relative to *https://app.sweephq.com/1.0.0*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DeleteUser**](UserApi.md#deleteuser) | **DELETE** /user | Delete user
[**GetUserInfo**](UserApi.md#getuserinfo) | **GET** /user | Get user info for the currently authenticated user
[**LoginUser**](UserApi.md#loginuser) | **GET** /user/login | Logs user into the system
[**LogoutUser**](UserApi.md#logoutuser) | **GET** /user/logout | Logs out current logged in user session
[**UpdateUser**](UserApi.md#updateuser) | **PUT** /user | Updated user


<a name="deleteuser"></a>
# **DeleteUser**
> void DeleteUser ()

Delete user

This can only be done by the logged in user.

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class DeleteUserExample
    {
        public void main()
        {
            var apiInstance = new UserApi();

            try
            {
                // Delete user
                apiInstance.DeleteUser();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UserApi.DeleteUser: " + e.Message );
            }
        }
    }
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

<a name="getuserinfo"></a>
# **GetUserInfo**
> User GetUserInfo ()

Get user info for the currently authenticated user

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class GetUserInfoExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new UserApi();

            try
            {
                // Get user info for the currently authenticated user
                User result = apiInstance.GetUserInfo();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UserApi.GetUserInfo: " + e.Message );
            }
        }
    }
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

<a name="loginuser"></a>
# **LoginUser**
> string LoginUser (string username, string password)

Logs user into the system

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class LoginUserExample
    {
        public void main()
        {
            var apiInstance = new UserApi();
            var username = username_example;  // string | The user name for login
            var password = password_example;  // string | The password for login in clear text

            try
            {
                // Logs user into the system
                string result = apiInstance.LoginUser(username, password);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UserApi.LoginUser: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **username** | **string**| The user name for login | 
 **password** | **string**| The password for login in clear text | 

### Return type

**string**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="logoutuser"></a>
# **LogoutUser**
> void LogoutUser ()

Logs out current logged in user session

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class LogoutUserExample
    {
        public void main()
        {
            var apiInstance = new UserApi();

            try
            {
                // Logs out current logged in user session
                apiInstance.LogoutUser();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UserApi.LogoutUser: " + e.Message );
            }
        }
    }
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

<a name="updateuser"></a>
# **UpdateUser**
> void UpdateUser (UserRequestBody userRequestBody)

Updated user

This can only be done by the logged in user.

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class UpdateUserExample
    {
        public void main()
        {
            var apiInstance = new UserApi();
            var userRequestBody = new UserRequestBody(); // UserRequestBody | 

            try
            {
                // Updated user
                apiInstance.UpdateUser(userRequestBody);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UserApi.UpdateUser: " + e.Message );
            }
        }
    }
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

