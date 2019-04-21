part of sweep_api.api;

class Event { 
  
  String id = null;
  
  String eventName = null;
  
  Map<String, Object> params = {};
  
  DateTime receivedOn = null;
  
  DateTime processedOn = null;
  
  String error = null;
  
  String organizationId = null;
  
  List<ListenerAction> actions = [];
  Event();

  @override
  String toString() {
    return 'Event[id=$id, eventName=$eventName, params=$params, receivedOn=$receivedOn, processedOn=$processedOn, error=$error, organizationId=$organizationId, actions=$actions, ]';
  }

  Event.fromJson(Map<String, dynamic> json) {
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
    if (json['params'] == null) {
      params = null;
    } else {
      params = convert.json.decode(json['params']);
    }
    if (json['receivedOn'] == null) {
      receivedOn = null;
    } else {
      receivedOn = DateTime.parse(json['receivedOn']);
    }
    if (json['processedOn'] == null) {
      processedOn = null;
    } else {
      processedOn = DateTime.parse(json['processedOn']);
    }
    if (json['error'] == null) {
      error = null;
    } else {
          error = json['error'];
    }
    if (json['organizationId'] == null) {
      organizationId = null;
    } else {
          organizationId = json['organizationId'];
    }
    if (json['actions'] == null) {
      actions = null;
    } else {
      actions = ListenerAction.listFromJson(json['actions']);
    }
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'eventName': eventName,
      'params': params,
      'receivedOn': receivedOn == null ? '' : receivedOn.toUtc().toIso8601String(),
      'processedOn': processedOn == null ? '' : processedOn.toUtc().toIso8601String(),
      'error': error,
      'organizationId': organizationId,
      'actions': actions
    };
  }

  static List<Event> listFromJson(List<dynamic> json) {
    return json == null ? new List<Event>() : json.map((value) => new Event.fromJson(value)).toList();
  }

  static Map<String, Event> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, Event>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new Event.fromJson(value));
    }
    return map;
  }
}

