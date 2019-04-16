<template>
  <v-layout column fill-height id="tribute-target">
    <v-flex style="border:1px solid #ccc">
      <v-slide-x-transition>
          <div id="editor" data-name="main-content" v-show="!editRaw">
          </div>
      </v-slide-x-transition>
      <v-slide-x-transition>
        <!-- <codemirror ref="codemirror" v-model="raw" v-show="editRaw"></codemirror> -->
        <textarea v-model="raw" ref="textarea" v-show="editRaw" style="height:100%;width:100%"/>
      </v-slide-x-transition>
      <iframe :srcdoc="raw" ref="iframe" style="display:none"></iframe>
    </v-flex>
  </v-layout>
</template>
<script>
import Vue from 'vue'
var beautify = require('js-beautify').html; 
import '../../node_modules/ContentTools/build/content-tools.min.js';
import '../../node_modules/ContentTools/build/content-tools.min.css';
import CodeMirror from 'codemirror';
import VueCodemirror from 'vue-codemirror'
Vue.use(VueCodemirror, {
  events:["keyHandled","beforeChange"],
  options:{
    tabSize: 4,
    styleActiveLine: false,
    lineNumbers: true,
    line: true,
    foldGutter: true,
    styleSelectedText: false,
    mode: 'text/html',
    matchBrackets: true,
    showCursorWhenSelecting: true,
    theme: "shadowfox",
    inputStyle:"contenteditable"
  }
});
import '../../node_modules/codemirror/lib/codemirror.css'
import '../../node_modules/codemirror/theme/shadowfox.css'

export default {
  props:{
    tribute:Object,
    editRaw:Boolean,
    active:Boolean,
  },
  data:() => ({
    editor:null,
    raw:"",
  }),
  mounted() {
    var vm = this;
    this.editor = ContentTools.EditorApp.get();
    this.editor.init("#editor", 'data-name');  
    this.editor.addEventListener("start", () => {
      Vue.nextTick(() => {
        vm.tribute.attach(document.querySelectorAll(".ce-element"));
      });
    });

    Vue.nextTick(() => {
      document.getElementById("editor").addEventListener("input", () => {
        vm.tribute.attach(document.querySelectorAll(".ce-element"));
      });
      
    });

    //this.editor.start();
    
    vm.$refs.textarea.addEventListener('tribute-replaced', function (evt) {
      vm.$refs.textarea.dispatchEvent(new Event('change'));
      vm.$refs.textarea.dispatchEvent(new Event('input'));
    });

  },
  watch:{
    tribute(newVal) {
      if(newVal) {
            newVal.attach(this.$refs.textarea);
        }
    },
    active(newVal) {
      if(newVal)
        this.editor.start();
      else
        this.editor.stop(true);
    },
    raw(newVal) {
      this.$emit("change", newVal);
    },
    // ContentTools acts upon existing HTML elements under our own application document tree
    // This means we can't pass the raw template HTML string to ContentTools, as this may contain <html>, <head> and <body> tags
    // To deal with this, the raw HTML is stored in an iframe
    // When editing via ContentTools, we retrieve the iframe->body->innerHTML 
    // When editing is complete, we update the same
    editRaw(newVal) {
      var iframe = this.$refs.iframe.contentDocument || this.$refs.iframe.contentWindow.document;
      var vm = this;      
      if(newVal == true) {
        this.editor.stop(true);
        iframe.body.innerHTML = document.getElementById("editor").innerHTML;
        this.raw = beautify(iframe.documentElement.outerHTML, {"indent-inner-html":true });
      } else {
        document.getElementById("editor").innerHTML = iframe.body.innerHTML;
        vm.editor.start();
      }
    }
  },
  components:{
    VueCodemirror
  }
}
</script>
<style>
.CodeMirror {
  font-size:14px;
  line-height:18px;
}
.ct-ignition {
    display:none;

  right: 64px !important;
  top: 49% !important;
  left: auto !important;
}
.ct-ignition__button--cancel {
    display:none;

  left:0px !important;
  top:64px !important;
}
.ct-toolbox {
  left:35% !important;
  top:45% !important;
  /* display:none; */
}

.ct-inspector {
  display:none;
}
</style>