# Sweep.Api.EventApi

All URIs are relative to *https://app.sweephq.com/1.0.0*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddEvent**](EventApi.md#addevent) | **POST** /events | Raise an event
[**GetEventById**](EventApi.md#geteventbyid) | **GET** /events/{eventId} | Find raised event by ID
[**ListEvents**](EventApi.md#listevents) | **GET** /events | List all received events


<a name="addevent"></a>
# **AddEvent**
> void AddEvent (EventRequestBody eventRequestBody, string apiKey = null)

Raise an event

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class AddEventExample
    {
        public void main()
        {
            // Configure API key authorization: api_key
            Configuration.Default.AddApiKey("api_key", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("api_key", "Bearer");

            var apiInstance = new EventApi();
            var eventRequestBody = new EventRequestBody(); // EventRequestBody | 
            var apiKey = apiKey_example;  // string |  (optional) 

            try
            {
                // Raise an event
                apiInstance.AddEvent(eventRequestBody, apiKey);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling EventApi.AddEvent: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **eventRequestBody** | [**EventRequestBody**](EventRequestBody.md)|  | 
 **apiKey** | **string**|  | [optional] 

### Return type

void (empty response body)

### Authorization

[api_key](../README.md#api_key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="geteventbyid"></a>
# **GetEventById**
> Event GetEventById (string eventId)

Find raised event by ID

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class GetEventByIdExample
    {
        public void main()
        {
            var apiInstance = new EventApi();
            var eventId = eventId_example;  // string | ID of event that needs to be fetched

            try
            {
                // Find raised event by ID
                Event result = apiInstance.GetEventById(eventId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling EventApi.GetEventById: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **eventId** | **string**| ID of event that needs to be fetched | 

### Return type

[**Event**](Event.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listevents"></a>
# **ListEvents**
> List<Event> ListEvents (bool? withActions = null, DateTime? startDate = null, DateTime? endDate = null)

List all received events

Returns a list of all events

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class ListEventsExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new EventApi();
            var withActions = true;  // bool? |  (optional) 
            var startDate = 2013-10-20T19:20:30+01:00;  // DateTime? |  (optional) 
            var endDate = 2013-10-20T19:20:30+01:00;  // DateTime? |  (optional) 

            try
            {
                // List all received events
                List&lt;Event&gt; result = apiInstance.ListEvents(withActions, startDate, endDate);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling EventApi.ListEvents: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **withActions** | **bool?**|  | [optional] 
 **startDate** | **DateTime?**|  | [optional] 
 **endDate** | **DateTime?**|  | [optional] 

### Return type

[**List<Event>**](Event.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

