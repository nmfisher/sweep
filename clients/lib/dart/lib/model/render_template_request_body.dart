part of openapi.api;

class RenderTemplateRequestBody {
  
  Map<String, Object> params = {};
  RenderTemplateRequestBody();

  @override
  String toString() {
    return 'RenderTemplateRequestBody[params=$params, ]';
  }

  RenderTemplateRequestBody.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    if (json['params'] == null) {
      params = null;
    } else {
      params = Object.mapFromJson(json['params']);
    }
  }

  Map<String, dynamic> toJson() {
    return {
      'params': params
    };
  }

  static List<RenderTemplateRequestBody> listFromJson(List<dynamic> json) {
    return json == null ? new List<RenderTemplateRequestBody>() : json.map((value) => new RenderTemplateRequestBody.fromJson(value)).toList();
  }

  static Map<String, RenderTemplateRequestBody> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, RenderTemplateRequestBody>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new RenderTemplateRequestBody.fromJson(value));
    }
    return map;
  }
}

