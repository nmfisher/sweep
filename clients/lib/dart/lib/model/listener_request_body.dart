part of openapi.api;

class ListenerRequestBody {
  
  String eventName = null;
  
  String trigger = null;
  
  List<String> eventParams = [];
  ListenerRequestBody();

  @override
  String toString() {
    return 'ListenerRequestBody[eventName=$eventName, trigger=$trigger, eventParams=$eventParams, ]';
  }

  ListenerRequestBody.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    if (json['eventName'] == null) {
      eventName = null;
    } else {
          eventName = json['eventName'];
    }
    if (json['trigger'] == null) {
      trigger = null;
    } else {
          trigger = json['trigger'];
    }
    if (json['eventParams'] == null) {
      eventParams = null;
    } else {
      eventParams = (json['eventParams'] as List).cast<String>();
    }
  }

  Map<String, dynamic> toJson() {
    return {
      'eventName': eventName,
      'trigger': trigger,
      'eventParams': eventParams
    };
  }

  static List<ListenerRequestBody> listFromJson(List<dynamic> json) {
    return json == null ? new List<ListenerRequestBody>() : json.map((value) => new ListenerRequestBody.fromJson(value)).toList();
  }

  static Map<String, ListenerRequestBody> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, ListenerRequestBody>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new ListenerRequestBody.fromJson(value));
    }
    return map;
  }
}

