import { EventApi, EventRequestBody } from '../../lib/typescript-axios';

export const SetApiKey = (apiKey) => {
  window.SWEEP_API_KEY = apiKey;
} 

export const Raise = (eventName, eventParams) => {
    if(window.SWEEP_API_KEY == null) 
      throw "Sweep API key not set";
  
      let requestBody = {
        eventName: eventName,
        params:eventParams
      }
      return new EventApi().addEvent(requestBody, window.SWEEP_API_KEY, {}).catch((err) => {console.error(err)});
} 