<template>
  <v-layout fill-height style="margin:0">
    <v-card style="position:absolute;left:-50px;z-index:9999" class="elevation-0">
      <v-card-text>
      <v-layout column>
          <v-btn flat icon color="indigo" @click="save"><v-icon>mdi-content-save</v-icon></v-btn>
          <v-btn flat icon color="orange" @click="$emit('close')"><v-icon>mdi-arrow-left</v-icon></v-btn>
      </v-layout>
      </v-card-text>
    </v-card>
   <v-card color="none" :elevation="0" style="height:100%;width:100%">
     <v-dialog v-model="preview">
        <iframe :srcdoc="content" height="100%" width="100%" style="border: 1px solid #ccc;border-radius: 3px;flex-grow:1"></iframe>
     </v-dialog>
        <v-container fill-height id="tribute-wrapper">
          <v-layout column>
            <v-form>
              <v-layout wrap>
                <v-flex xs6>
                    <v-text-field
                        label="From (display name)"
                        ref="fromName"
                        v-model="fromName"
                        :rules="[rules.template]"
                        class="white-input"/>
                    <v-text-field
                        v-model="fromAddress"
                        ref="fromAddress"
                        label="From (address)"
                        :rules="[rules.template]"
                        class="white-input"/>
                </v-flex>
                <v-flex xs6>
                    <v-text-field
                        label="To (address)"
                        ref="to"
                        v-model="newRecipient"
                        :rules="[rules.templateOrEmail, rules.required]"
                        @keypress.enter="addRecipient"
                        class="white-input"/>
                    <v-text-field
                        ref="subject"
                        label="Subject"
                        :rules="[rules.template]"
                        class="white-input"/>
                </v-flex>
            </v-layout>
          </v-form>
          <v-layout column xs8 fill-height>
            <v-flex xs11>
                <jodit-vue v-model="content" :config="joditConfig" :buttons="joditConfig.buttons"/>
            </v-flex>
            <v-flex xs1>
              <v-layout row>
                <v-flex xs10>
                  <!-- <p>Type a double brace "{{" to insert an event parameter as a <a href='https://mustache.github.io/'>Mustache variable</a>.</p> -->
                  <p>When an event is raised, these are replaced by the value of the event parameter you raise in your code.</p>
                  <p>Event parameters not referenced in templates will be logged but ignored. If you raise an event without event parameters for all template variables, the event will fail and no e-mail will be sent.</p>
                </v-flex>
                <v-flex xs2>
                  <v-btn outline color="green" @click="preview = true" style="float:right">Preview</v-btn>
                </v-flex>
              </v-layout>
            </v-flex>
          </v-layout>
        </v-layout>
      </v-container>
   </v-card>
  </v-layout>
</template>
<script lang="ts">
import Vue from 'vue'
import JoditVue from 'jodit-vue'
import Jodit from 'jodit'
import Tribute from '../../lib/tribute/src'
Vue.use(JoditVue)
import 'jodit/build/jodit.min.css'
import 'tributejs/dist/tribute.css'
import { TemplateApiFactory, ListenerApiFactory, ListenerApiFp, ListenerApi, ListenerRequestBody, Listener } from '../../lib/api';import { TemplateApi } from '../../lib/api';

export default {
  props:{
    listener:Object
  },
   data: () => ({
      joditConfig:{
        defaultMode: JoditVue.MODE_SOURCE,
        inline:true,
        buttons: [
          'source',
          '|',
          'fontsize',
          'font',
          'bold',
          'strikethrough',
          'underline',
          'italic',
          '|',
          'ul',
          'ol',
          '|',
          'outdent',
          'indent',
          '|',
          'brush',
          '|',
          'image',
          'table',
          'link',
          '|',
          'align',],
        toolbarButtonSize: "large"
      },
      content:"",
      sendTo:"",
      fromName:"",
      fromAddress:"",
      subject:"",
      preview:false,
      loading:false,
      templateId:null
  }),
  methods:{
    addRecipient(item) {

    },
    save() {
      var vm = this;
      if(this.templateId == null) {
        var requestBody = {
          content:this.content, 
          subject:this.subject, 
          fromAddress:this.fromAddress, 
          fromName:this.fromName,
          sendTo:this.sendTo,
        }
        new TemplateApi().addTemplate(requestBody, null, {withCredentials:true}).then((resp) => {
            return new ListenerApi().addListenerTemplate(vm.listener.id, resp.data.id, null, {withCredentials:true});
        }).catch((err) => {
            vm.$store.state.app.snackbar = err;
        });
      }
    }
  },
  computed:{
    rules() {
      var vm = this;
      return {
        required(val) {
            if(val == null || val == "")
              return "Value must not be null";
            return true;
        },
        template(val) {
            if(val != null && val.startsWith("{{") && val.endsWith("}}") && !vm.listener.eventParams.includes(val))
                return "This event parameter is not defined.";
            return true;
        },
        templateOrEmail(val) {
          if(val != null) {
            if(val.startsWith("{{") && val.endsWith("}}") && !vm.listener.eventParams.includes(val))
              return "This event parameter is not defined.";
            var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if(!re.test(val.toLowerCase()))
              return "Must be a valid e-mail or a template parameter";
          } 
        }
      }
    }
  },
  created() {
    var vm = this;
    Jodit.plugins.tributejs = function (editor) {
        editor.events
            .on('beforeEnter', function (event) {
                if (vm.tribute.isActive) {
                    return true;
                }
            })
            .on('afterInit', function () {
                var options = {
                    trigger: '{{',
                    replaceTextSuffix:"}}",
                    requireLeadingSpace: false,
                    menuContainer:document.getElementById("tribute-container"),
                    values:[]
                };
                vm.tribute = new Tribute(options);
                vm.tribute.attach(editor.editor)  ;
                vm.tribute.attach(vm.$refs.fromName.$el.getElementsByTagName("input")[0]);
                vm.$refs.fromName.$el.getElementsByTagName("input")[0].addEventListener('tribute-replaced', function (evt) {
                  vm.fromName = vm.$refs.fromName.$el.getElementsByTagName("input")[0].value;
                });
                vm.tribute.attach(vm.$refs.fromAddress.$el.getElementsByTagName("input")[0]);
                vm.tribute.attach(vm.$refs.to.$el.getElementsByTagName("input")[0]);
                vm.tribute.attach(vm.$refs.subject.$el.getElementsByTagName("input")[0]);
            });
    };
  },
  components: { 
    JoditVue
  },
  watch:{
    listener(newVal) {
        var vm = this;
        if(typeof(this.tribute) !== "undefined" && typeof(newVal) != "undefined" && newVal != null && typeof(newVal.eventParams) != "undefined" && newVal.eventParams != null && newVal.eventParams.length > 0) {
          this.tribute.collection[0].values=newVal.eventParams.map((x) => { return {key:x,value:x} });
        } else {
          this.tribute.collection[0].values=[];
        }
        if(newVal != null) {
          new ListenerApi().listListenerTemplates(newVal.id, { withCredentials:true }).then((resp) => { 
            if(resp.data.length > 0)
              return new TemplateApi().getTemplateById(resp.data[0].templateId, null, {withCredentials:true});

          }).then((resp) => {
            if(typeof(resp) !== "undefined") {
              vm.templateId = resp.data.id;
              vm.content = resp.data.content;
              vm.fromAddress = resp.data.fromAddress;
              vm.fromName = resp.data.fromName;
              vm.sendTo = resp.data.sendTo;
              vm.subject = resp.data.sendTo;
            }
          }).catch((err) => {
              vm.$store.state.app.snackbar = err;
          });
        }
    }
  }
}
</script>
<style lang="scss">
.jodit_source {
    height:90%;
}
.jodit_workplace {
  height:90% !important;
  border: 1px solid #ccc;
}
.jodit_container {
  height:90% !important;
}
</style>