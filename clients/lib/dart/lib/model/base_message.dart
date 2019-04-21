part of sweep_api.api;

class BaseMessage { 
  
  String id = null;
  
  String content = null;
  
  String subject = null;
  
  String fromAddress = null;
  
  String fromName = null;
  
  List<String> sendTo = [];
  
  String organizationId = null;
  BaseMessage();

  @override
  String toString() {
    return 'BaseMessage[id=$id, content=$content, subject=$subject, fromAddress=$fromAddress, fromName=$fromName, sendTo=$sendTo, organizationId=$organizationId, ]';
  }

  BaseMessage.fromJson(Map<String, dynamic> json) {
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
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'content': content,
      'subject': subject,
      'fromAddress': fromAddress,
      'fromName': fromName,
      'sendTo': sendTo,
      'organizationId': organizationId
    };
  }

  static List<BaseMessage> listFromJson(List<dynamic> json) {
    return json == null ? new List<BaseMessage>() : json.map((value) => new BaseMessage.fromJson(value)).toList();
  }

  static Map<String, BaseMessage> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, BaseMessage>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new BaseMessage.fromJson(value));
    }
    return map;
  }
}

