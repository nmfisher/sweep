<template>
  <v-layout fill-height style="margin:0">
    <v-card color="none" :elevation="0" style="height:100%;width:100%">
     <v-dialog v-model="showingPreview" width="600">
       <message-preview ref="preview" :templateId="templateId" :params="listener ? listener.eventParams : null"/>
     </v-dialog>
        <v-container fill-height id="tribute-wrapper">
          <v-layout column>
            <v-flex style="flex-grow:0">
              <v-layout row  align-center justify-center>
                <v-flex xs6>
                  <v-form ref="form">
                      <combobox2
                        :search-input.sync="sendToInput"
                        clearable
                        :preventCapture="preventComboCapture"
                          label="To (address)"
                          ref="sendTo"
                          :items="sendTo"
                          :rules="[rules.recipients]"
                          v-model="sendTo"
                          hint="Type an e-mail address or a template variable and press enter"
                          class="white-input"
                          hide-selected
                          multiple
                          persistent-hint
                          @keyup.native="onComboBoxInput"
                          small-chips>
                          <template v-slot:selection="{ item, parent, selected }">
                            <helper-combobox-item :rules="[rules.required, rules.templateOrEmail]" :item="item" @remove="sendTo.splice(sendTo.indexOf(item), 1)"/>
                          </template>
                      </combobox2>
                          <v-text-field
                              ref="subject"
                              v-model="subject"
                              label="Subject"
                              :rules="[rules.templateOrString]"
                              class="white-input"/>
                </v-form>
              </v-flex>
              <v-flex xs6>
                  <v-text-field
                              label="From (display name)"
                              ref="fromName"
                              v-model="fromName"
                              :rules="[rules.templateOrString]"
                              class="white-input"/>
                  <v-text-field
                      v-model="fromAddress"
                      ref="fromAddress"
                      label="From (address)"
                      :rules="[rules.templateOrEmailOrEmpty]"
                      class="white-input"/>
              </v-flex>
            </v-layout>
          </v-flex>
          <v-flex>
            <v-layout column fill-height>
              <v-layout row style="flex-grow:0;padding:12px" align-center>
                <v-switch v-model="editRaw" :label="editRaw ? `Raw HTML`: `Visual`"></v-switch>
                <span v-if="validated" @click="showPreview" style="cursor:pointer">
                    Preview
                </span>
                <v-btn flat icon color="grey">
                  <v-tooltip>
                    <v-icon slot="activator" size="16">
                      mdi-help
                    </v-icon>
                      <p>When an event is raised, these are replaced by the value of the event parameter you raise in your code.</p>
                      <p>Event parameters not referenced in templates will be logged but ignored. If you raise an event without event parameters for all template variables, the event will fail and no e-mail will be sent.</p>
                    </v-tooltip>
                </v-btn>
              </v-layout>
              <v-flex xs11>
                  <content-tools-editor @change="content = $event" :tribute="tribute" :editRaw="editRaw" :active="active"/>
                  <v-text-field v-model="content" :rules="[rules.required, rules.templateOrString]" class="hide-input"/>
              </v-flex>
              <v-flex xs1>
                <v-layout row justify-center align-center>
                  <v-btn flat outline color="primary" @click="$emit('close')">Close</v-btn>
                  <v-btn flat outline color="indigo" @click="save" :disabled="!validated" class="ml-3">Save</v-btn>
                </v-layout>
              </v-flex>
            </v-layout>
          </v-flex>
        </v-layout>
      </v-container> 
   </v-card> 
  </v-layout>
</template>
<script lang="ts">
import Vue from 'vue'
import Tribute from '../../lib/tribute/src'
import Combobox2 from '../components/helper/Combobox2'
import 'tributejs/dist/tribute.css'
import { TemplateApi, TemplateApiFactory, ListenerApiFactory, ListenerApiFp, ListenerApi, ListenerRequestBody, Listener } from '../../../clients/lib/typescript-axios';
import ContentToolsEditor from './ContentToolsEditor.vue';


export default {
  props:{
    listener:Object,
    active:Boolean
  },
   data: () => ({
      content:"",
      editRaw:false,
      sendTo:[],
      sendToInput:"",
      fromName:"",
      fromAddress:"",
      preventComboCapture:false,
      subject:"",
      showingPreview:false,
      contentValidationError:false,
      loading:false,
      saving:false,
      newRecipient:null,
      templateId:null,
      validated:false,
      tribute:null
  }),
  methods:{
    onComboBoxInput() {
      var vm = this;
      Vue.nextTick(() => {
        vm.preventComboCapture = vm.tribute.isActive;
      });
    },
    showPreview() {
      var vm = this;
      this.save().then((resp) => {
        vm.showingPreview = true;
      }).catch((err) => {
        console.error(err);
        vm.$store.state.app.snackbar = err;
      });
    },
    validate() {
      var vm = this;
      if(typeof(this.$refs.form) !== "undefined") {
        if(this.validationTimer != null) 
          clearTimeout(this.validationTimer);
        var vm = this;
        this.validationTimer = setTimeout(() => {
          vm.validated = this.$refs.form.validate() 
        },900);
      }
    },
    save() {
      var vm = this;
      var requestBody = {
          content:this.content, 
          subject:this.subject, 
          fromAddress:this.fromAddress, 
          fromName:this.fromName,
          sendTo:this.sendTo,
      }
      vm.saving = true;
      if(this.templateId == null) {
        return new TemplateApi().addTemplate(requestBody, {withCredentials:true}).then((resp) => {
            vm.templateId = resp.data.id;
            return new ListenerApi().addListenerTemplate(vm.listener.id, resp.data.id, {withCredentials:true});
        }).catch((err) => {
            console.error(err);
            vm.$store.state.app.snackbar = err.response.data;
        }).finally(() => {
          vm.saving = false;
        });
      } else {
        return new TemplateApi().updateTemplate(this.templateId, requestBody, {withCredentials:true}).catch((err) => {
          console.error(err);
          vm.$store.state.app.snackbar = err.response.data;
        }).finally(() => {
          vm.saving = false;
        });
      }
    }
  },
  computed:{
    rules() {
      var vm = this;
      return {
        recipients(val) {
          if(val.length == 0) 
            return "At least one valid recipient must be specified";

          for(var i = 0; i < val.length; i++) {
            var v = val[i];
            if(vm.rules.required(v) !== true || vm.rules.templateOrEmail(v) !== true)
              return "At least one valid recipient must be specified";
          };
          return true;
        },
        required(val) {
            if(val == null || val == "")
              return "Value must not be null";
            return true;
        },
        template(val, allowNone) {
          if(vm.listener == null)
            return true;
          var expr = /{{([^{}]+?)(<\/span>)?}}/g;
          var matches = expr.exec(val);

          if(!allowNone && matches == null)
            return "Event parameter must be specified";
          while(matches != null) {
            if(!vm.listener.eventParams.includes(matches[1])) {
              return "The event parameter " + matches[1] + " is not defined.";
            }
            matches = expr.exec(val);
          }
                
          return true;
        },
        templateOrString(val) {
          return vm.rules.template(val, true);
        },
        templateOrEmailOrEmpty(val) {
          if(val === null || val === '')
            return true;
          
          return vm.rules.templateOrEmail(val);
        },
        templateOrEmail(val) {

          var validTemplate = vm.rules.template(val, false);
          var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
          var isEmail = re.test(val.toLowerCase())
          if(validTemplate === true || isEmail === true)
            return true;
          else 
            return "Must be a valid e-mail or a template parameter";
        }
      }
    }
  },
  mounted() {
    var vm = this;
  
    vm.tribute = new Tribute({
        trigger: '{{',
        requireLeadingSpace: false,
        replaceTextSuffix: '  ',
        selectTemplate: (item) => {
          return '{{' + item.original.value + '}}';
        },
        values:[],
    });

    ["fromName", "fromAddress","subject","sendTo"].forEach((k) => {
      vm.tribute.attach(vm.$refs[k].$el.getElementsByTagName("input")[0]);
    });

    ["fromName", "fromAddress","subject"].forEach((k) => {
      vm.$refs[k].$el.getElementsByTagName("input")[0].addEventListener('tribute-replaced', function (evt) {
        vm[k] = vm.$refs[k].$el.getElementsByTagName("input")[0].value.trim();
      });
    });
    
    vm.$refs.sendTo.$el.getElementsByTagName("input")[0].addEventListener('tribute-replaced', function (evt) {
      vm.sendTo.push(vm.$refs.sendTo.$el.getElementsByTagName("input")[0].value.trim());
      vm.sendToInput = "";
    });
  },
  watch:{
    listener:{
      deep:true,
      handler(newVal) {
        //this.$refs.editor.reset();
        this.validate();
        var vm = this;
        if(typeof(this.tribute) === "undefined")
          return;

        if(typeof(newVal) == "undefined" || newVal == null)
          return;

        if(typeof(newVal.eventParams) != "undefined" && newVal.eventParams != null && newVal.eventParams.length > 0) {
          this.tribute.collection[0].values=newVal.eventParams.map((x) => { return {key:x,value:x} });
        } else {
          this.tribute.collection[0].values=[];
        }
        if(newVal != null) {
          new ListenerApi().listListenerTemplates(newVal.id, { withCredentials:true }).then((resp) => { 
            if(resp.data.length > 0)
              return new TemplateApi().getTemplateById(resp.data[0].templateId, {withCredentials:true});

          }).then((resp) => {
            if(typeof(resp) !== "undefined") {
              vm.templateId = resp.data.id;
              vm.content = resp.data.content;
              vm.fromAddress = resp.data.fromAddress;
              vm.fromName = resp.data.fromName;
              vm.sendTo = resp.data.sendTo;
              vm.subject = resp.data.subject;
            } else {
              vm.templateId = null;
              vm.content = null;
              vm.fromAddress = null;
              vm.fromName = null;
              vm.sendTo = [];
              vm.subject = null;
            }
            //vm.$refs.editor.content = vm.content;
          }).catch((err) => {
              console.error(err);
              vm.$store.state.app.snackbar = err;
          });
        }
      }
    },
    content(newVal) {
      this.validate();
    },
    sendTo(newVal) {
      this.validate();
    },
    fromAddress(newVal) {
      this.validate();
    },
    fromName(newVal) {
      this.validate();
    },
    subject(newVal) {
      this.validate();
    }
  },
  components: { 
    Combobox2, ContentToolsEditor
  },
}
</script>
<style>
.hide-input {
     margin-top:25px;
}
.hide-input .v-input__slot {
   display:none !important;
} 
</style>