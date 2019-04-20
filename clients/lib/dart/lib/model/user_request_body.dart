part of openapi.api;

class UserRequestBody {
  
  String username = null;
  UserRequestBody();

  @override
  String toString() {
    return 'UserRequestBody[username=$username, ]';
  }

  UserRequestBody.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    if (json['username'] == null) {
      username = null;
    } else {
          username = json['username'];
    }
  }

  Map<String, dynamic> toJson() {
    return {
      'username': username
    };
  }

  static List<UserRequestBody> listFromJson(List<dynamic> json) {
    return json == null ? new List<UserRequestBody>() : json.map((value) => new UserRequestBody.fromJson(value)).toList();
  }

  static Map<String, UserRequestBody> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, UserRequestBody>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new UserRequestBody.fromJson(value));
    }
    return map;
  }
}

