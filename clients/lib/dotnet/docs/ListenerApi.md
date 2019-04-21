# Sweep.Api.ListenerApi

All URIs are relative to *https://app.sweephq.com/1.0.0*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AddListener**](ListenerApi.md#addlistener) | **POST** /listeners | Create a new Listener
[**AddListenerTemplate**](ListenerApi.md#addlistenertemplate) | **POST** /listeners/{listenerId}/templates/{templateId} | Associates a Template to a Listener
[**DeleteListener**](ListenerApi.md#deletelistener) | **DELETE** /listeners/{listenerId} | Deletes a Listener
[**DeleteListenerTemplate**](ListenerApi.md#deletelistenertemplate) | **DELETE** /listeners/{listenerId}/templates/{templateId} | Disassociates a Template from a Listener
[**GetListener**](ListenerApi.md#getlistener) | **GET** /listeners/{listenerId} | Get a listener by ID
[**ListListenerTemplates**](ListenerApi.md#listlistenertemplates) | **GET** /listeners/{listenerId}/templates | List Templates for Listener
[**ListListeners**](ListenerApi.md#listlisteners) | **GET** /listeners | List all Listeners
[**ListMessagesForAction**](ListenerApi.md#listmessagesforaction) | **GET** /actions/{listenerActionId}/messages | List all messages
[**UpdateListener**](ListenerApi.md#updatelistener) | **PUT** /listeners/{listenerId} | Updates a Listener


<a name="addlistener"></a>
# **AddListener**
> Listener AddListener (ListenerRequestBody listenerRequestBody)

Create a new Listener

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class AddListenerExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ListenerApi();
            var listenerRequestBody = new ListenerRequestBody(); // ListenerRequestBody | 

            try
            {
                // Create a new Listener
                Listener result = apiInstance.AddListener(listenerRequestBody);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ListenerApi.AddListener: " + e.Message );
            }
        }
    }
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

<a name="addlistenertemplate"></a>
# **AddListenerTemplate**
> void AddListenerTemplate (string listenerId, string templateId)

Associates a Template to a Listener

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class AddListenerTemplateExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ListenerApi();
            var listenerId = listenerId_example;  // string | Listener id to disassociate
            var templateId = templateId_example;  // string | Template id to associate with the Listener

            try
            {
                // Associates a Template to a Listener
                apiInstance.AddListenerTemplate(listenerId, templateId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ListenerApi.AddListenerTemplate: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerId** | **string**| Listener id to disassociate | 
 **templateId** | **string**| Template id to associate with the Listener | 

### Return type

void (empty response body)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deletelistener"></a>
# **DeleteListener**
> void DeleteListener (string listenerId)

Deletes a Listener

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class DeleteListenerExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ListenerApi();
            var listenerId = listenerId_example;  // string | ID of listener to return

            try
            {
                // Deletes a Listener
                apiInstance.DeleteListener(listenerId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ListenerApi.DeleteListener: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerId** | **string**| ID of listener to return | 

### Return type

void (empty response body)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deletelistenertemplate"></a>
# **DeleteListenerTemplate**
> void DeleteListenerTemplate (string listenerId, string templateId)

Disassociates a Template from a Listener

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class DeleteListenerTemplateExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ListenerApi();
            var listenerId = listenerId_example;  // string | Listener id to disassociate
            var templateId = templateId_example;  // string | Template id to delete

            try
            {
                // Disassociates a Template from a Listener
                apiInstance.DeleteListenerTemplate(listenerId, templateId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ListenerApi.DeleteListenerTemplate: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerId** | **string**| Listener id to disassociate | 
 **templateId** | **string**| Template id to delete | 

### Return type

void (empty response body)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getlistener"></a>
# **GetListener**
> Listener GetListener (string listenerId)

Get a listener by ID

Returns a listener

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class GetListenerExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ListenerApi();
            var listenerId = listenerId_example;  // string | ID of listener to update

            try
            {
                // Get a listener by ID
                Listener result = apiInstance.GetListener(listenerId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ListenerApi.GetListener: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerId** | **string**| ID of listener to update | 

### Return type

[**Listener**](Listener.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listlistenertemplates"></a>
# **ListListenerTemplates**
> List<ListenerTemplate> ListListenerTemplates (string listenerId)

List Templates for Listener

Returns a list of templates associated with this listener

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class ListListenerTemplatesExample
    {
        public void main()
        {
            var apiInstance = new ListenerApi();
            var listenerId = listenerId_example;  // string | ID of listener

            try
            {
                // List Templates for Listener
                List&lt;ListenerTemplate&gt; result = apiInstance.ListListenerTemplates(listenerId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ListenerApi.ListListenerTemplates: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerId** | **string**| ID of listener | 

### Return type

[**List<ListenerTemplate>**](ListenerTemplate.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listlisteners"></a>
# **ListListeners**
> List<Listener> ListListeners ()

List all Listeners

Returns a list of Listeners

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class ListListenersExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ListenerApi();

            try
            {
                // List all Listeners
                List&lt;Listener&gt; result = apiInstance.ListListeners();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ListenerApi.ListListeners: " + e.Message );
            }
        }
    }
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

<a name="listmessagesforaction"></a>
# **ListMessagesForAction**
> List<Message> ListMessagesForAction (string listenerActionId)

List all messages

Returns a list of messages for the given ListenerAction

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class ListMessagesForActionExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ListenerApi();
            var listenerActionId = listenerActionId_example;  // string | The id of the ListenerAction to limit the results

            try
            {
                // List all messages
                List&lt;Message&gt; result = apiInstance.ListMessagesForAction(listenerActionId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ListenerApi.ListMessagesForAction: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerActionId** | **string**| The id of the ListenerAction to limit the results | 

### Return type

[**List<Message>**](Message.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatelistener"></a>
# **UpdateListener**
> Listener UpdateListener (string listenerId, ListenerRequestBody listenerRequestBody)

Updates a Listener

### Example
```csharp
using System;
using System.Diagnostics;
using Sweep.Api;
using Sweep.Client;
using Sweep.Model;

namespace Example
{
    public class UpdateListenerExample
    {
        public void main()
        {
            // Configure OAuth2 access token for authorization: Google
            Configuration.Default.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new ListenerApi();
            var listenerId = listenerId_example;  // string | ID of listener to update
            var listenerRequestBody = new ListenerRequestBody(); // ListenerRequestBody | 

            try
            {
                // Updates a Listener
                Listener result = apiInstance.UpdateListener(listenerId, listenerRequestBody);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ListenerApi.UpdateListener: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **listenerId** | **string**| ID of listener to update | 
 **listenerRequestBody** | [**ListenerRequestBody**](ListenerRequestBody.md)|  | 

### Return type

[**Listener**](Listener.md)

### Authorization

[Google](../README.md#Google)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

