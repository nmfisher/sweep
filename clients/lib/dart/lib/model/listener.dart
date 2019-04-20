part of openapi.api;

class Listener {
  
  String id = null;
  
  String eventName = null;
  
  List<String> eventParams = [];
  
  String organizationId = null;
  
  String trigger = null;
  Listener();

  @override
  String toString() {
    return 'Listener[id=$id, eventName=$eventName, eventParams=$eventParams, organizationId=$organizationId, trigger=$trigger, ]';
  }

  Listener.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    if (json['id'] == null) {
      id = null;
    } else {
          id = json['id'];
    }
    if (json['eventName'] == null) {
      eventName = null;
    } else {
          eventName = json['eventName'];
    }
    if (json['eventParams'] == null) {
      eventParams = null;
    } else {
      eventParams = (json['eventParams'] as List).cast<String>();
    }
    if (json['organizationId'] == null) {
      organizationId = null;
    } else {
          organizationId = json['organizationId'];
    }
    if (json['trigger'] == null) {
      trigger = null;
    } else {
          trigger = json['trigger'];
    }
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'eventName': eventName,
      'eventParams': eventParams,
      'organizationId': organizationId,
      'trigger': trigger
    };
  }

  static List<Listener> listFromJson(List<dynamic> json) {
    return json == null ? new List<Listener>() : json.map((value) => new Listener.fromJson(value)).toList();
  }

  static Map<String, Listener> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, Listener>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new Listener.fromJson(value));
    }
    return map;
  }
}

