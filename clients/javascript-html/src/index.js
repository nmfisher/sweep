import { EventApi, EventRequestBody } from '../../lib/typescript-axios';

export const SetApiKey = (apiKey) => {
  window.SWEEP_API_KEY = apiKey;
} 

export const Raise = (eventName, eventParams) => {
    if(window.SWEEP_API_KEY == null) 
      throw "Sweep API key not set";

    console.log(window.SWEEP_API_KEY);
  
      let requestBody = {
        eventName: eventName,
        params:eventParams
      }
      return new EventApi().addEvent(requestBody, window.SWEEP_API_KEY, {}).then((foo) => {
        console.log("FOO");
      }).catch((err) => {console.error(err)});
} 