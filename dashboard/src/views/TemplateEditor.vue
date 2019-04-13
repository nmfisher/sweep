<template>
  <v-layout fill-height style="margin:0">
    <v-card style="position:absolute;left:-50px;z-index:9999" class="elevation-0">
      <v-card-text>
      <v-layout column>
          <v-btn flat icon color="indigo" @click="save" :disabled="!validated"><v-icon>mdi-content-save</v-icon></v-btn>
          <v-btn flat icon color="orange" @click="$emit('close')"><v-icon>mdi-arrow-left</v-icon></v-btn>
      </v-layout>
      </v-card-text>
    </v-card>
   <v-card color="none" :elevation="0" style="height:100%;width:100%">
     <v-dialog v-model="showingPreview" width="600">
       <message-preview ref="preview" :templateId="templateId" :params="listener ? listener.eventParams : null"/>
     </v-dialog>
        <v-container fill-height id="tribute-wrapper">
          <v-layout column>
            <v-layout row>
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
            </v-form>
          </v-flex>
          <v-flex xs6>
              <!-- <p>Type a double brace "{{" to insert an event parameter as a <a href='https://mustache.github.io/'>Mustache variable</a>.</p> -->
                  <p>When an event is raised, these are replaced by the value of the event parameter you raise in your code.</p>
                  <p>Event parameters not referenced in templates will be logged but ignored. If you raise an event without event parameters for all template variables, the event will fail and no e-mail will be sent.</p>
          </v-flex>
          </v-layout>
          <v-layout column xs8 fill-height>
            <v-flex xs11>
                <jodit-vue v-model="content" :config="joditConfig" :buttons="joditConfig.buttons"/>
                <v-text-field v-model="content" :rules="[rules.required, rules.templateOrString]" class="hide-input"/>
            </v-flex>
            <v-flex xs1>
              <v-layout row justify-center>
                  <v-btn outline color="green" @click="showPreview" :disabled="!validated" style="float:right">Preview</v-btn>
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
import Combobox2 from '../components/helper/Combobox2'
Vue.use(JoditVue)
import 'jodit/build/jodit.min.css'
import 'tributejs/dist/tribute.css'
import { TemplateApi, TemplateApiFactory, ListenerApiFactory, ListenerApiFp, ListenerApi, ListenerRequestBody, Listener } from '../../lib/api';
import MessagePreview from './MessagePreview.vue';

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
      sendTo:[],
      sendToInput:"",
      fromName:"",
      fromAddress:"",
      preventComboCapture:false,
      subject:"",
      showingPreview:false,
      contentValidationError:false,
      loading:false,
      newRecipient:null,
      templateId:null,
      validated:false,
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
      if(this.templateId == null) {
        return new TemplateApi().addTemplate(requestBody, null, {withCredentials:true}).then((resp) => {
            vm.templateId = resp.data.id;
            return new ListenerApi().addListenerTemplate(vm.listener.id, resp.data.id, null, {withCredentials:true});
        }).catch((err) => {
            console.error(err);
            vm.$store.state.app.snackbar = err;
        });
      } else {
        return new TemplateApi().updateTemplate(this.templateId, requestBody, null, {withCredentials:true}).catch((err) => {
          console.error(err);
          vm.$store.state.app.snackbar = err;
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
                    requireLeadingSpace: false,
                    selectTemplate: (item) => {
                      return '{{' + item.original.value + '}}';
                    },
                    menuContainer:document.getElementById("tribute-container"),
                    values:[]
                };
                vm.tribute = new Tribute(options);
                vm.tribute.attach(editor.editor)  ;

                ["fromName", "fromAddress","subject","sendTo"].forEach((k) => {
                  vm.tribute.attach(vm.$refs[k].$el.getElementsByTagName("input")[0]);
                });

                ["fromName", "fromAddress","subject"].forEach((k) => {
                  vm.$refs[k].$el.getElementsByTagName("input")[0].addEventListener('tribute-replaced', function (evt) {
                    vm[k] = vm.$refs[k].$el.getElementsByTagName("input")[0].value;
                  });
                });
                
                vm.$refs.sendTo.$el.getElementsByTagName("input")[0].addEventListener('tribute-replaced', function (evt) {
                  //if(vm.sendTo == null)
                  //  vm.sendTo = [];
                  vm.sendTo.push(vm.$refs.sendTo.$el.getElementsByTagName("input")[0].value);
                  console.log(vm.$refs.sendTo )
                  vm.sendToInput = "";
                  //vm.newRecipient = null;
                });
            });
    };
  },
  components: { 
    JoditVue, Combobox2, MessagePreview
  },
  watch:{
    listener:{
      deep:true,
      handler(newVal) {
        this.validate();
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
              vm.subject = resp.data.subject;
            }
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
.hide-input {
    margin-top:25px;
}
.hide-input .v-input__slot {
  display:none !important;
}
</style>