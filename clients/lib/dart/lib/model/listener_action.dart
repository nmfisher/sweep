part of sweep_api.api;

class ListenerAction { 
  
  String id = null;
  
  String eventId = null;
  
  String listenerId = null;
  
  String organizationId = null;
  
  bool completed = false;
  
  String error = null;
  ListenerAction();

  @override
  String toString() {
    return 'ListenerAction[id=$id, eventId=$eventId, listenerId=$listenerId, organizationId=$organizationId, completed=$completed, error=$error, ]';
  }

  ListenerAction.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    if (json['id'] == null) {
      id = null;
    } else {
          id = json['id'];
    }
    if (json['eventId'] == null) {
      eventId = null;
    } else {
          eventId = json['eventId'];
    }
    if (json['listenerId'] == null) {
      listenerId = null;
    } else {
          listenerId = json['listenerId'];
    }
    if (json['organizationId'] == null) {
      organizationId = null;
    } else {
          organizationId = json['organizationId'];
    }
    if (json['completed'] == null) {
      completed = null;
    } else {
          completed = json['completed'];
    }
    if (json['error'] == null) {
      error = null;
    } else {
          error = json['error'];
    }
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'eventId': eventId,
      'listenerId': listenerId,
      'organizationId': organizationId,
      'completed': completed,
      'error': error
    };
  }

  static List<ListenerAction> listFromJson(List<dynamic> json) {
    return json == null ? new List<ListenerAction>() : json.map((value) => new ListenerAction.fromJson(value)).toList();
  }

  static Map<String, ListenerAction> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, ListenerAction>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new ListenerAction.fromJson(value));
    }
    return map;
  }
}

