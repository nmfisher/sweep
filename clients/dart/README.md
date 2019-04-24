# Sweep SDK for Dart 

This is the Dart SDK for the (Sweep)[https://sweephq.com] platform.

## Usage

- Create an account at https://app.sweephq.com and login.
- Copy the API key.

``` 
Sweep.SetApiKey("YOUR-API-KEY");```
var eventName = "signup";
var eventParams = {"username":"foo@bar"};
Sweep.Raise(eventName:"foo", params:eventParams);
```