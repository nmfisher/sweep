part of openapi.api;

class ListenerTemplate {
  
  String listenerId = null;
  
  String templateId = null;
  
  String organizationId = null;
  ListenerTemplate();

  @override
  String toString() {
    return 'ListenerTemplate[listenerId=$listenerId, templateId=$templateId, organizationId=$organizationId, ]';
  }

  ListenerTemplate.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    if (json['listenerId'] == null) {
      listenerId = null;
    } else {
          listenerId = json['listenerId'];
    }
    if (json['templateId'] == null) {
      templateId = null;
    } else {
          templateId = json['templateId'];
    }
    if (json['organizationId'] == null) {
      organizationId = null;
    } else {
          organizationId = json['organizationId'];
    }
  }

  Map<String, dynamic> toJson() {
    return {
      'listenerId': listenerId,
      'templateId': templateId,
      'organizationId': organizationId
    };
  }

  static List<ListenerTemplate> listFromJson(List<dynamic> json) {
    return json == null ? new List<ListenerTemplate>() : json.map((value) => new ListenerTemplate.fromJson(value)).toList();
  }

  static Map<String, ListenerTemplate> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, ListenerTemplate>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new ListenerTemplate.fromJson(value));
    }
    return map;
  }
}

