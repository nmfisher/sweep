<template>
  <v-layout fill-height>
    <v-card style="position:absolute;left:-50px;z-index:9999" class="elevation-0">
      <v-card-text>
      <v-layout column>
          <v-btn flat icon color="indigo"><v-icon>mdi-content-save</v-icon></v-btn>
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
                        class="white-input"/>
                    <v-text-field
                        v-model="fromAddress"
                        ref="fromAddress"
                        label="From (address)"
                        class="white-input"/>
                </v-flex>
                <v-flex xs6>
                    <v-text-field
                        label="To (address)"
                        ref="to"
                        v-model="to"
                        class="white-input"/>
                    <v-text-field
                        ref="subject"
                        label="Subject"
                        class="white-input"/>
                </v-flex>
            </v-layout>
          </v-form>
          <v-layout row wrap xs8 fill-height>
            <v-flex xs11>
                <jodit-vue v-model="content" :config="joditConfig" :buttons="joditConfig.buttons"/>
            </v-flex>
            <v-flex xs1>
              <v-btn outline color="green" icon @click="preview = true"><v-icon>mdi-arrow-expand</v-icon></v-btn>
            </v-flex>
          </v-layout>
        </v-layout>
      </v-container>
   </v-card>
  </v-layout>
</template>
<script>
import Vue from 'vue'
import JoditVue from 'jodit-vue'
import Jodit from 'jodit'
import Tribute from '../../lib/tribute/src'
Vue.use(JoditVue)
import 'jodit/build/jodit.min.css'
import 'tributejs/dist/tribute.css'

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
      content: `<html style="background:#eee;color:#333;font-family:'Helvetica'">
      <h1>Hi %%username%%</h1>
      <p>Thanks for signing up!</p>
      <p>Best regards,</p>
      <p>Bill</p>
      </html>`,
      to:"",
      fromName:"",
      fromAddress:"",
      subject:"",
      preview:false,
      loading:false,
  }),
  methods:{
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
        if(typeof(this.tribute) !== "undefined" && typeof(newVal) != "undefined" && newVal != null && typeof(newVal.eventParams) != "undefined" && newVal.eventParams != null && newVal.eventParams.length > 0) {
          this.tribute.collection[0].values=newVal.eventParams.map((x) => { return {key:x,value:x} });
        } else {
          this.tribute.collection[0].values=[];
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
}
.jodit_container {
  height:90% !important;
}
</style>