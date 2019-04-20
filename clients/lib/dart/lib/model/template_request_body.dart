part of openapi.api;

class TemplateRequestBody {
  
  String content = null;
  
  String subject = null;
  
  String fromAddress = null;
  
  String fromName = null;
  
  List<String> sendTo = [];
  TemplateRequestBody();

  @override
  String toString() {
    return 'TemplateRequestBody[content=$content, subject=$subject, fromAddress=$fromAddress, fromName=$fromName, sendTo=$sendTo, ]';
  }

  TemplateRequestBody.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
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
  }

  Map<String, dynamic> toJson() {
    return {
      'content': content,
      'subject': subject,
      'fromAddress': fromAddress,
      'fromName': fromName,
      'sendTo': sendTo
    };
  }

  static List<TemplateRequestBody> listFromJson(List<dynamic> json) {
    return json == null ? new List<TemplateRequestBody>() : json.map((value) => new TemplateRequestBody.fromJson(value)).toList();
  }

  static Map<String, TemplateRequestBody> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, TemplateRequestBody>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new TemplateRequestBody.fromJson(value));
    }
    return map;
  }
}

