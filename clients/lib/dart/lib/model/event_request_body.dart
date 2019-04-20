part of openapi.api;

class EventRequestBody {
  
  String eventName = null;
  
  Map<String, Object> params = {};
  EventRequestBody();

  @override
  String toString() {
    return 'EventRequestBody[eventName=$eventName, params=$params, ]';
  }

  EventRequestBody.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    if (json['eventName'] == null) {
      eventName = null;
    } else {
          eventName = json['eventName'];
    }
    if (json['params'] == null) {
      params = null;
    } else {
      params = Object.mapFromJson(json['params']);
    }
  }

  Map<String, dynamic> toJson() {
    return {
      'eventName': eventName,
      'params': params
    };
  }

  static List<EventRequestBody> listFromJson(List<dynamic> json) {
    return json == null ? new List<EventRequestBody>() : json.map((value) => new EventRequestBody.fromJson(value)).toList();
  }

  static Map<String, EventRequestBody> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, EventRequestBody>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new EventRequestBody.fromJson(value));
    }
    return map;
  }
}

