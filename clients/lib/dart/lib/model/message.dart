part of openapi.api;

class Message {
  
  String id = null;
  
  String content = null;
  
  String subject = null;
  
  String fromAddress = null;
  
  String fromName = null;
  
  List<String> sendTo = [];
  
  String organizationId = null;
  
  String listenerActionId = null;
  
  DateTime sentOn = null;
  Message();

  @override
  String toString() {
    return 'Message[id=$id, content=$content, subject=$subject, fromAddress=$fromAddress, fromName=$fromName, sendTo=$sendTo, organizationId=$organizationId, listenerActionId=$listenerActionId, sentOn=$sentOn, ]';
  }

  Message.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    if (json['id'] == null) {
      id = null;
    } else {
          id = json['id'];
    }
    if (json['content'] == null) {
      content = null;
    } else {
          content = json['content'];
    }
    if (json['subject'] == null) {
      subject = null;
    } else {
          subject = json['subject'];
    }
    if (json['fromAddress'] == null) {
      fromAddress = null;
    } else {
          fromAddress = json['fromAddress'];
    }
    if (json['fromName'] == null) {
      fromName = null;
    } else {
          fromName = json['fromName'];
    }
    if (json['sendTo'] == null) {
      sendTo = null;
    } else {
      sendTo = (json['sendTo'] as List).cast<String>();
    }
    if (json['organizationId'] == null) {
      organizationId = null;
    } else {
          organizationId = json['organizationId'];
    }
    if (json['listenerActionId'] == null) {
      listenerActionId = null;
    } else {
          listenerActionId = json['listenerActionId'];
    }
    if (json['sentOn'] == null) {
      sentOn = null;
    } else {
      sentOn = DateTime.parse(json['sentOn']);
    }
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'content': content,
      'subject': subject,
      'fromAddress': fromAddress,
      'fromName': fromName,
      'sendTo': sendTo,
      'organizationId': organizationId,
      'listenerActionId': listenerActionId,
      'sentOn': sentOn == null ? '' : sentOn.toUtc().toIso8601String()
    };
  }

  static List<Message> listFromJson(List<dynamic> json) {
    return json == null ? new List<Message>() : json.map((value) => new Message.fromJson(value)).toList();
  }

  static Map<String, Message> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, Message>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new Message.fromJson(value));
    }
    return map;
  }
}

