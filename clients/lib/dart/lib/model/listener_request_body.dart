part of sweep_api.api;

class ListenerRequestBody { 
  
  String eventName = null;
  
  String triggerEvent = null;
  
  num triggerNumber = null;
  
  String triggerPeriod = null;
  
  String triggerMatch = null;
  
  List<String> eventParams = [];
  ListenerRequestBody();

  @override
  String toString() {
    return 'ListenerRequestBody[eventName=$eventName, triggerEvent=$triggerEvent, triggerNumber=$triggerNumber, triggerPeriod=$triggerPeriod, triggerMatch=$triggerMatch, eventParams=$eventParams, ]';
  }

  ListenerRequestBody.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    if (json['eventName'] == null) {
      eventName = null;
    } else {
          eventName = json['eventName'];
    }
    if (json['triggerEvent'] == null) {
      triggerEvent = null;
    } else {
          triggerEvent = json['triggerEvent'];
    }
    if (json['triggerNumber'] == null) {
      triggerNumber = null;
    } else {
          triggerNumber = json['triggerNumber'];
    }
    if (json['triggerPeriod'] == null) {
      triggerPeriod = null;
    } else {
          triggerPeriod = json['triggerPeriod'];
    }
    if (json['triggerMatch'] == null) {
      triggerMatch = null;
    } else {
          triggerMatch = json['triggerMatch'];
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
      'triggerEvent': triggerEvent,
      'triggerNumber': triggerNumber,
      'triggerPeriod': triggerPeriod,
      'triggerMatch': triggerMatch,
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

