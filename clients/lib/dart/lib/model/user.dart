part of openapi.api;

class User {
  
  String id = null;
  
  String username = null;
  
  String password = null;
  
  String organizationId = null;
  User();

  @override
  String toString() {
    return 'User[id=$id, username=$username, password=$password, organizationId=$organizationId, ]';
  }

  User.fromJson(Map<String, dynamic> json) {
    if (json == null) return;
    if (json['id'] == null) {
      id = null;
    } else {
          id = json['id'];
    }
    if (json['username'] == null) {
      username = null;
    } else {
          username = json['username'];
    }
    if (json['password'] == null) {
      password = null;
    } else {
          password = json['password'];
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
      'username': username,
      'password': password,
      'organizationId': organizationId
    };
  }

  static List<User> listFromJson(List<dynamic> json) {
    return json == null ? new List<User>() : json.map((value) => new User.fromJson(value)).toList();
  }

  static Map<String, User> mapFromJson(Map<String, dynamic> json) {
    var map = new Map<String, User>();
    if (json != null && json.length > 0) {
      json.forEach((String key, dynamic value) => map[key] = new User.fromJson(value));
    }
    return map;
  }
}

