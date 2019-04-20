part of openapi.api;



class TemplateApi {
  final ApiClient apiClient;

  TemplateApi([ApiClient apiClient]) : apiClient = apiClient ?? defaultApiClient;

  /// Create a new Template
  ///
  /// 
  Future<Template> addTemplate(TemplateRequestBody templateRequestBody) async {
    Object postBody = templateRequestBody;

    // verify required params are set
    if(templateRequestBody == null) {
     throw new ApiException(400, "Missing required param: templateRequestBody");
    }

    // create path and map variables
    String path = "/templates".replaceAll("{format}","json");

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
      return apiClient.deserialize(_decodeBodyBytes(response), 'Template') as Template;
    } else {
      return null;
    }
  }
  /// Deletes a Template
  ///
  /// 
  Future deleteTemplate(String templateId) async {
    Object postBody;

    // verify required params are set
    if(templateId == null) {
     throw new ApiException(400, "Missing required param: templateId");
    }

    // create path and map variables
    String path = "/templates/{templateId}".replaceAll("{format}","json").replaceAll("{" + "templateId" + "}", templateId.toString());

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
  /// Find Template by ID
  ///
  /// Returns a single template
  Future<Template> getTemplateById(String templateId) async {
    Object postBody;

    // verify required params are set
    if(templateId == null) {
     throw new ApiException(400, "Missing required param: templateId");
    }

    // create path and map variables
    String path = "/templates/{templateId}".replaceAll("{format}","json").replaceAll("{" + "templateId" + "}", templateId.toString());

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
      return apiClient.deserialize(_decodeBodyBytes(response), 'Template') as Template;
    } else {
      return null;
    }
  }
  /// List all Templates
  ///
  /// Returns a list of templates
  Future<List<Template>> listTemplate() async {
    Object postBody;

    // verify required params are set

    // create path and map variables
    String path = "/templates".replaceAll("{format}","json");

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
      return (apiClient.deserialize(_decodeBodyBytes(response), 'List<Template>') as List).map((item) => item as Template).toList();
    } else {
      return null;
    }
  }
  /// Renders a template using the provided event parameters
  ///
  /// Returns a string representing the HTML content of an email to be sent
  Future<Message> renderTemplate(String templateId, RenderTemplateRequestBody renderTemplateRequestBody) async {
    Object postBody = renderTemplateRequestBody;

    // verify required params are set
    if(templateId == null) {
     throw new ApiException(400, "Missing required param: templateId");
    }
    if(renderTemplateRequestBody == null) {
     throw new ApiException(400, "Missing required param: renderTemplateRequestBody");
    }

    // create path and map variables
    String path = "/templates/{templateId}/render".replaceAll("{format}","json").replaceAll("{" + "templateId" + "}", templateId.toString());

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
      return apiClient.deserialize(_decodeBodyBytes(response), 'Message') as Message;
    } else {
      return null;
    }
  }
  /// Update an existing Template
  ///
  /// 
  Future<Template> updateTemplate(String templateId, TemplateRequestBody templateRequestBody) async {
    Object postBody = templateRequestBody;

    // verify required params are set
    if(templateId == null) {
     throw new ApiException(400, "Missing required param: templateId");
    }
    if(templateRequestBody == null) {
     throw new ApiException(400, "Missing required param: templateRequestBody");
    }

    // create path and map variables
    String path = "/templates/{templateId}".replaceAll("{format}","json").replaceAll("{" + "templateId" + "}", templateId.toString());

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
      return apiClient.deserialize(_decodeBodyBytes(response), 'Template') as Template;
    } else {
      return null;
    }
  }
}
