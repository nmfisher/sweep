import 'package:sweep_api/api.dart';


class Sweep {

  static String _apiKey;

  static void SetApiKey(String apiKey) {
    _apiKey = apiKey;
  }

  static Future<dynamic> Raise({String eventName, Map<String,Object> params}) {
    if(_apiKey == null) {
      throw Exception("API Key not set");
    }
    var api_instance = new EventApi();
    var eventRequestBody = new EventRequestBody();
    eventRequestBody.eventName = eventName;
    eventRequestBody.params = params; // EventRequestBody | 
    return api_instance.addEvent(eventRequestBody, apiKey:_apiKey);
  }
}