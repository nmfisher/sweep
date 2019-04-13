<template>
    <v-tabs
      v-model="active"
      color="indigo"
      dark
    >
    <v-tab key="0">PARAMETERS</v-tab>
    <v-tab key="1" :disabled="!message">PREVIEW</v-tab>
    <v-tab-item key="0" style="height:250px;">
      <v-card style="padding:15px;height:100%;display:flex;flex-direction:column">
          <v-layout row wrap fill-height justify-center align-center>
              <v-flex xs5>
                <p>Render template with:</p>
              </v-flex>
              <v-flex xs6> 
                <v-list style="width:100%">
                  <v-list-tile v-for="param in params" :key="param">
                    <v-text-field :hint="param" persistent-hint v-model="requestBody[param]"/>
                  </v-list-tile>
                </v-list>
              </v-flex>
          </v-layout>
          <v-layout column align-center justify-center>
              <material-notification
                v-if="renderError"
                  class="mb-3"
                  color="error"
                  dismissible
                >
                  {{renderError}}
              </material-notification>
              <v-btn @click="render">OK</v-btn>
          </v-layout>
      </v-card>
    </v-tab-item>
    <v-tab-item key="1" style="height:500px">
      <v-card style="padding:15px;height:100%;">        
        <v-layout column v-if="message" fill-height>
            <v-list>
              <v-list-tile>
                From: <v-chip>{{message.fromName}} ({{message.fromAddress}})</v-chip>
              </v-list-tile>
              <v-list-tile>
                To: <v-chip v-for="recipient in message.sendTo" :key="recipient">{{recipient}}</v-chip>
              </v-list-tile>
              <v-list-tile>
                Subject: <v-chip>{{message.subject}}</v-chip>
              </v-list-tile>
            </v-list>
            <v-flex xs12>
              <iframe :srcdoc="message.content" height="100%" width="100%" style="background:#fff; border: 1px solid #ccc;border-radius: 3px;flex-grow:1"></iframe>
            </v-flex>
      </v-layout>
    </v-card>
    </v-tab-item>
  </v-tabs>
</template>
<script>
  import { TemplateApiFactory, TemplateApi  } from '../../lib/api';
  export default {
    data:() => ({
      active:0,
      message:null,
      renderError:null,
      requestBody:{}
    }),
    props:{
      templateId:String,
      params:Array
    },
    methods:{
      render() {
        var vm = this;
        this.renderError = null;
        if(this.templateId == null)
          return;
        
        new TemplateApi().renderTemplate(this.templateId, {params:this.requestBody}, null, { withCredentials:true }).then((resp) => { 
          vm.message = resp.data;
          vm.active = 1;
        }).catch((err) => {
          console.error(err);
          vm.renderError = err.response.data;
        });
      }
    },
  }
</script>
<style>

</style>
  
