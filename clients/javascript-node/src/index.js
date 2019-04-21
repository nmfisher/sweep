var api = require('../../lib/typescript-axios');

export const Sweep = {
  SetApiKey:(apiKey) => {
    this.SWEEP_API_KEY = apiKey;
  }, 
  Raise:(eventName, eventParams) => {
    if(this.SWEEP_API_KEY == null) 
      throw "Sweep API key not set";
  
      let requestBody = {
        eventName: eventName,
        params:eventParams
      }
      return new api.EventApi().addEvent(requestBody, this.SWEEP_API_KEY, {}).catch((err) => {console.error(err)});
  } 
}

module.exports = Sweep;