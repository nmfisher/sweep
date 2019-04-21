# Sweep.Api.OrganizationApi

All URIs are relative to *https://app.sweephq.com/1.0.0*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetOrganizationInfo**](OrganizationApi.md#getorganizationinfo) | **GET** /organization | Get organization info for the currently authenticated context


<a name="getorganizationinfo"></a>
# **GetOrganizationInfo**
> Organization GetOrganizationInfo ()

Get organization info for the currently authenticated context

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class GetOrganizationInfoExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new OrganizationApi();

            try
            {
                // Get organization info for the currently authenticated context
                Organization result = apiInstance.GetOrganizationInfo();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrganizationApi.GetOrganizationInfo: " + e.Message );
            }
        }
    }
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

