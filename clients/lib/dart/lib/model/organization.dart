part of sweep_api.api;

class Organization { 
  
  String id = null;
  
  String primaryApiKey = null;
  
  String secondaryApiKey = null;
  Organization();

  @override
  String toString() {
    return 'Organization[id=$id, primaryApiKey=$primaryApiKey, secondaryApiKey=$secondaryApiKey, ]';
  }

  Organization.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    if (json['id'] == null) {
      id = null;
    } else {
          id = json['id'];
    }
    if (json['primaryApiKey'] == null) {
      primaryApiKey = null;
    } else {
          primaryApiKey = json['primaryApiKey'];
    }
    if (json['secondaryApiKey'] == null) {
      secondaryApiKey = null;
    } else {
          secondaryApiKey = json['secondaryApiKey'];
    }
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'primaryApiKey': primaryApiKey,
      'secondaryApiKey': secondaryApiKey
    };
  }

  static List<Organization> listFromJson(List<dynamic> json) {
    return json == null ? new List<Organization>() : json.map((value) => new Organization.fromJson(value)).toList();
  }

  static Map<String, Organization> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, Organization>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new Organization.fromJson(value));
    }
    return map;
  }
}

