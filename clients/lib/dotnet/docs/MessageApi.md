# Sweep.Api.MessageApi

All URIs are relative to *https://app.sweephq.com/1.0.0*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetMessageById**](MessageApi.md#getmessagebyid) | **GET** /messages/{messageId} | Find message by ID
[**ListMessages**](MessageApi.md#listmessages) | **GET** /messages | List all messages


<a name="getmessagebyid"></a>
# **GetMessageById**
> Message GetMessageById (string messageId)

Find message by ID

Returns a single message

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class GetMessageByIdExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new MessageApi();
            var messageId = messageId_example;  // string | ID of message to return

            try
            {
                // Find message by ID
                Message result = apiInstance.GetMessageById(messageId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MessageApi.GetMessageById: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **messageId** | **string**| ID of message to return | 

### Return type

[**Message**](Message.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listmessages"></a>
# **ListMessages**
> List<Message> ListMessages (DateTime? startDate = null, DateTime? endDate = null)

List all messages

Returns a list of messages

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class ListMessagesExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new MessageApi();
            var startDate = 2013-10-20T19:20:30+01:00;  // DateTime? |  (optional) 
            var endDate = 2013-10-20T19:20:30+01:00;  // DateTime? |  (optional) 

            try
            {
                // List all messages
                List&lt;Message&gt; result = apiInstance.ListMessages(startDate, endDate);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MessageApi.ListMessages: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **startDate** | **DateTime?**|  | [optional] 
 **endDate** | **DateTime?**|  | [optional] 

### Return type

[**List<Message>**](Message.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

