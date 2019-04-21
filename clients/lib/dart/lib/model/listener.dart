part of sweep_api.api;

class Listener { 
  
  String id = null;
  
  String eventName = null;
  
  List<String> eventParams = [];
  
  String organizationId = null;
  
  String triggerEvent = null;
  
  int triggerNumber = null;
  
  String triggerPeriod = null;
  
  String triggerMatch = null;
  Listener();

  @override
  String toString() {
    return 'Listener[id=$id, eventName=$eventName, eventParams=$eventParams, organizationId=$organizationId, triggerEvent=$triggerEvent, triggerNumber=$triggerNumber, triggerPeriod=$triggerPeriod, triggerMatch=$triggerMatch, ]';
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
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'eventName': eventName,
      'eventParams': eventParams,
      'organizationId': organizationId,
      'triggerEvent': triggerEvent,
      'triggerNumber': triggerNumber,
      'triggerPeriod': triggerPeriod,
      'triggerMatch': triggerMatch
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

