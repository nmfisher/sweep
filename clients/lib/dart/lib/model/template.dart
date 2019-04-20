part of openapi.api;

class Template {
  
  String id = null;
  
  String content = null;
  
  String subject = null;
  
  String fromAddress = null;
  
  String fromName = null;
  
  List<String> sendTo = [];
  
  String organizationId = null;
  
  bool deleted = null;
  
  String userId = null;
  Template();

  @override
  String toString() {
    return 'Template[id=$id, content=$content, subject=$subject, fromAddress=$fromAddress, fromName=$fromName, sendTo=$sendTo, organizationId=$organizationId, deleted=$deleted, userId=$userId, ]';
  }

  Template.fromJson(Map<String, dynamic> json) {
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
    if (json['deleted'] == null) {
      deleted = null;
    } else {
          deleted = json['deleted'];
    }
    if (json['userId'] == null) {
      userId = null;
    } else {
          userId = json['userId'];
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
      'deleted': deleted,
      'userId': userId
    };
  }

  static List<Template> listFromJson(List<dynamic> json) {
    return json == null ? new List<Template>() : json.map((value) => new Template.fromJson(value)).toList();
  }

  static Map<String, Template> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, Template>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new Template.fromJson(value));
    }
    return map;
  }
}

