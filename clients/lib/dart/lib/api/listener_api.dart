part of openapi.api;



class ListenerApi {
  final ApiClient apiClient;

  ListenerApi([ApiClient apiClient]) : apiClient = apiClient ?? defaultApiClient;

  /// Create a new Listener
  ///
  /// 
  Future<Listener> addListener(ListenerRequestBody listenerRequestBody) async {
    Object postBody = listenerRequestBody;

    // verify required params are set
    if(listenerRequestBody == null) {
     throw new ApiException(400, "Missing required param: listenerRequestBody");
    }

    // create path and map variables
    String path = "/listeners".replaceAll("{format}","json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};

    List<String> contentTypes = ["application/json"];

    String contentType = contentTypes.length > 0 ? contentTypes[0] : "application/json";
    List<String> authNames = ["Google"];

    if(contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = new MultipartRequest(null, null);
      if(hasFields)
        postBody = mp;
    }
    else {
    }

    var response = await apiClient.invokeAPI(path,
                                             'POST',
                                             queryParams,
                                             postBody,
                                             headerParams,
                                             formParams,
                                             contentType,
                                             authNames);

    if(response.statusCode >= 400) {
      throw new ApiException(response.statusCode, _decodeBodyBytes(response));
    } else if(response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'Listener') as Listener;
    } else {
      return null;
    }
  }
  /// Associates a Template to a Listener
  ///
  /// 
  Future addListenerTemplate(String listenerId, String templateId) async {
    Object postBody;

    // verify required params are set
    if(listenerId == null) {
     throw new ApiException(400, "Missing required param: listenerId");
    }
    if(templateId == null) {
     throw new ApiException(400, "Missing required param: templateId");
    }

    // create path and map variables
    String path = "/listeners/{listenerId}/templates/{templateId}".replaceAll("{format}","json").replaceAll("{" + "listenerId" + "}", listenerId.toString()).replaceAll("{" + "templateId" + "}", templateId.toString());

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};

    List<String> contentTypes = [];

    String contentType = contentTypes.length > 0 ? contentTypes[0] : "application/json";
    List<String> authNames = ["Google"];

    if(contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = new MultipartRequest(null, null);
      if(hasFields)
        postBody = mp;
    }
    else {
    }

    var response = await apiClient.invokeAPI(path,
                                             'POST',
                                             queryParams,
                                             postBody,
                                             headerParams,
                                             formParams,
                                             contentType,
                                             authNames);

    if(response.statusCode >= 400) {
      throw new ApiException(response.statusCode, _decodeBodyBytes(response));
    } else if(response.body != null) {
    } else {
      return;
    }
  }
  /// Deletes a Listener
  ///
  /// 
  Future deleteListener(String listenerId) async {
    Object postBody;

    // verify required params are set
    if(listenerId == null) {
     throw new ApiException(400, "Missing required param: listenerId");
    }

    // create path and map variables
    String path = "/listeners/{listenerId}".replaceAll("{format}","json").replaceAll("{" + "listenerId" + "}", listenerId.toString());

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};

    List<String> contentTypes = [];

    String contentType = contentTypes.length > 0 ? contentTypes[0] : "application/json";
    List<String> authNames = ["Google"];

    if(contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = new MultipartRequest(null, null);
      if(hasFields)
        postBody = mp;
    }
    else {
    }

    var response = await apiClient.invokeAPI(path,
                                             'DELETE',
                                             queryParams,
                                             postBody,
                                             headerParams,
                                             formParams,
                                             contentType,
                                             authNames);

    if(response.statusCode >= 400) {
      throw new ApiException(response.statusCode, _decodeBodyBytes(response));
    } else if(response.body != null) {
    } else {
      return;
    }
  }
  /// Disassociates a Template from a Listener
  ///
  /// 
  Future deleteListenerTemplate(String listenerId, String templateId) async {
    Object postBody;

    // verify required params are set
    if(listenerId == null) {
     throw new ApiException(400, "Missing required param: listenerId");
    }
    if(templateId == null) {
     throw new ApiException(400, "Missing required param: templateId");
    }

    // create path and map variables
    String path = "/listeners/{listenerId}/templates/{templateId}".replaceAll("{format}","json").replaceAll("{" + "listenerId" + "}", listenerId.toString()).replaceAll("{" + "templateId" + "}", templateId.toString());

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};

    List<String> contentTypes = [];

    String contentType = contentTypes.length > 0 ? contentTypes[0] : "application/json";
    List<String> authNames = ["Google"];

    if(contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = new MultipartRequest(null, null);
      if(hasFields)
        postBody = mp;
    }
    else {
    }

    var response = await apiClient.invokeAPI(path,
                                             'DELETE',
                                             queryParams,
                                             postBody,
                                             headerParams,
                                             formParams,
                                             contentType,
                                             authNames);

    if(response.statusCode >= 400) {
      throw new ApiException(response.statusCode, _decodeBodyBytes(response));
    } else if(response.body != null) {
    } else {
      return;
    }
  }
  /// Get a listener by ID
  ///
  /// Returns a listener
  Future<Listener> getListener(String listenerId) async {
    Object postBody;

    // verify required params are set
    if(listenerId == null) {
     throw new ApiException(400, "Missing required param: listenerId");
    }

    // create path and map variables
    String path = "/listeners/{listenerId}".replaceAll("{format}","json").replaceAll("{" + "listenerId" + "}", listenerId.toString());

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};

    List<String> contentTypes = [];

    String contentType = contentTypes.length > 0 ? contentTypes[0] : "application/json";
    List<String> authNames = ["Google"];

    if(contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = new MultipartRequest(null, null);
      if(hasFields)
        postBody = mp;
    }
    else {
    }

    var response = await apiClient.invokeAPI(path,
                                             'GET',
                                             queryParams,
                                             postBody,
                                             headerParams,
                                             formParams,
                                             contentType,
                                             authNames);

    if(response.statusCode >= 400) {
      throw new ApiException(response.statusCode, _decodeBodyBytes(response));
    } else if(response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'Listener') as Listener;
    } else {
      return null;
    }
  }
  /// List Templates for Listener
  ///
  /// Returns a list of templates associated with this listener
  Future<List<ListenerTemplate>> listListenerTemplates(String listenerId) async {
    Object postBody;

    // verify required params are set
    if(listenerId == null) {
     throw new ApiException(400, "Missing required param: listenerId");
    }

    // create path and map variables
    String path = "/listeners/{listenerId}/templates".replaceAll("{format}","json").replaceAll("{" + "listenerId" + "}", listenerId.toString());

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};

    List<String> contentTypes = [];

    String contentType = contentTypes.length > 0 ? contentTypes[0] : "application/json";
    List<String> authNames = [];

    if(contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = new MultipartRequest(null, null);
      if(hasFields)
        postBody = mp;
    }
    else {
    }

    var response = await apiClient.invokeAPI(path,
                                             'GET',
                                             queryParams,
                                             postBody,
                                             headerParams,
                                             formParams,
                                             contentType,
                                             authNames);

    if(response.statusCode >= 400) {
      throw new ApiException(response.statusCode, _decodeBodyBytes(response));
    } else if(response.body != null) {
      return (apiClient.deserialize(_decodeBodyBytes(response), 'List<ListenerTemplate>') as List).map((item) => item as ListenerTemplate).toList();
    } else {
      return null;
    }
  }
  /// List all Listeners
  ///
  /// Returns a list of Listeners
  Future<List<Listener>> listListeners() async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/listeners".replaceAll("{format}","json");

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};

    List<String> contentTypes = [];

    String contentType = contentTypes.length > 0 ? contentTypes[0] : "application/json";
    List<String> authNames = ["Google"];

    if(contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = new MultipartRequest(null, null);
      if(hasFields)
        postBody = mp;
    }
    else {
    }

    var response = await apiClient.invokeAPI(path,
                                             'GET',
                                             queryParams,
                                             postBody,
                                             headerParams,
                                             formParams,
                                             contentType,
                                             authNames);

    if(response.statusCode >= 400) {
      throw new ApiException(response.statusCode, _decodeBodyBytes(response));
    } else if(response.body != null) {
      return (apiClient.deserialize(_decodeBodyBytes(response), 'List<Listener>') as List).map((item) => item as Listener).toList();
    } else {
      return null;
    }
  }
  /// List all messages
  ///
  /// Returns a list of messages for the given ListenerAction
  Future<List<Message>> listMessagesForAction(String listenerActionId) async {
    Object postBody;

    // verify required params are set
    if(listenerActionId == null) {
     throw new ApiException(400, "Missing required param: listenerActionId");
    }

    // create path and map variables
    String path = "/actions/{listenerActionId}/messages".replaceAll("{format}","json").replaceAll("{" + "listenerActionId" + "}", listenerActionId.toString());

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};

    List<String> contentTypes = [];

    String contentType = contentTypes.length > 0 ? contentTypes[0] : "application/json";
    List<String> authNames = ["Google"];

    if(contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = new MultipartRequest(null, null);
      if(hasFields)
        postBody = mp;
    }
    else {
    }

    var response = await apiClient.invokeAPI(path,
                                             'GET',
                                             queryParams,
                                             postBody,
                                             headerParams,
                                             formParams,
                                             contentType,
                                             authNames);

    if(response.statusCode >= 400) {
      throw new ApiException(response.statusCode, _decodeBodyBytes(response));
    } else if(response.body != null) {
      return (apiClient.deserialize(_decodeBodyBytes(response), 'List<Message>') as List).map((item) => item as Message).toList();
    } else {
      return null;
    }
  }
  /// Updates a Listener
  ///
  /// 
  Future<Listener> updateListener(String listenerId, ListenerRequestBody listenerRequestBody) async {
    Object postBody = listenerRequestBody;

    // verify required params are set
    if(listenerId == null) {
     throw new ApiException(400, "Missing required param: listenerId");
    }
    if(listenerRequestBody == null) {
     throw new ApiException(400, "Missing required param: listenerRequestBody");
    }

    // create path and map variables
    String path = "/listeners/{listenerId}".replaceAll("{format}","json").replaceAll("{" + "listenerId" + "}", listenerId.toString());

    // query params
    List<QueryParam> queryParams = [];
    Map<String, String> headerParams = {};
    Map<String, String> formParams = {};

    List<String> contentTypes = ["application/json"];

    String contentType = contentTypes.length > 0 ? contentTypes[0] : "application/json";
    List<String> authNames = ["Google"];

    if(contentType.startsWith("multipart/form-data")) {
      bool hasFields = false;
      MultipartRequest mp = new MultipartRequest(null, null);
      if(hasFields)
        postBody = mp;
    }
    else {
    }

    var response = await apiClient.invokeAPI(path,
                                             'PUT',
                                             queryParams,
                                             postBody,
                                             headerParams,
                                             formParams,
                                             contentType,
                                             authNames);

    if(response.statusCode >= 400) {
      throw new ApiException(response.statusCode, _decodeBodyBytes(response));
    } else if(response.body != null) {
      return apiClient.deserialize(_decodeBodyBytes(response), 'Listener') as Listener;
    } else {
      return null;
    }
  }
}
