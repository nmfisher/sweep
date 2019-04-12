<template>
  <v-card>
    <v-layout>
      <v-flex>
        <template v-for="param in params">
          {{param}} : <v-text-field :key="param" v-model="requestBody[param]"></v-text-field>
        </template>
      </v-flex>
      <v-flex xs12>
        From: {{message.fromName}} ({{message.fromAddress}})
        To: <span v-for="recipient in message.sendTo" :key="recipient">{{recipient}}</span>
        Subject: {{message.subject}}
      </v-flex>
      <iframe :srcdoc="message.content" height="100%" width="100%" style="background:#fff; border: 1px solid #ccc;border-radius: 3px;flex-grow:1"></iframe>
    </v-layout>
  </v-card>
</template>
<script>
  export default {
    props:{
      templateId:String,
      message:Object,
      params:Object
    },
    data:() => ({
      requestBody:{}
    }),
    watch:{
      templateId(newVal) {
        if(newVal == null)
          return;
        var vm = this;
        new TemplateApi().renderTemplate(this.templateId, this.requestBody, { withCredentials:true }).then((resp) => { 
          vm.message = resp.data;
        }).catch((err) => {
          vm.$store.state.app.snackbar = err;
        });
      }
    }
  }
</script>
<style>

</style>
  
