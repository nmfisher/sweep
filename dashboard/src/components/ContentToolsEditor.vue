<template>
  <v-layout column fill-height>
    <v-switch v-model="editRaw" :label="editRaw ? `Raw HTML`: `Visual`" style="flex-grow:0"></v-switch>
    <v-flex style="border:1px solid #ccc">
      <v-slide-x-transition>
        <div ref="editor" id="editor" data-name="main-content" v-show="!editRaw">

        </div>
      </v-slide-x-transition>
      <v-slide-x-transition>
        <codemirror v-model="raw" v-show="editRaw"></codemirror>
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
import VueCodemirror from 'vue-codemirror'
Vue.use(VueCodemirror, {
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
  }
});
import '../../node_modules/codemirror/lib/codemirror.css'
import '../../node_modules/codemirror/theme/shadowfox.css'

export default {
  props:{
    tribute:Object,
  },
  data:() => ({
    editor:null,
    editRaw:false,
    raw:"",
  }),
  mounted() {
    var vm = this;
    this.editor = ContentTools.EditorApp.get();
    this.editor.init("#editor", 'data-name');  

    this.editor.start();
  },
  watch:{
    tribute(newVal) {
      if(newVal != null) {
          newVal.attach(this.$refs.editor);
      }
    },
    // ContentTools acts upon existing HTML elements under our own application document tree
    // This means we can't pass the raw template HTML string to ContentTools, as this may contain <html>, <head> and <body> tags
    // To deal with this, the raw HTML is stored in an iframe
    // When editing via ContentTools, we retrieve the iframe->body->innerHTML 
    // When editing is complete, we update the same
    editRaw(newVal) {
      
      var iframe = this.$refs.iframe.contentDocument || this.$refs.iframe.contentWindow.document;

      var updateElement = iframe.body ? iframe.body : iframe
      
      if(newVal == true) {
        this.editor.stop(true);
        updateElement.innerHTML = this.$refs.editor.innerHTML;
        this.raw = beautify(iframe.documentElement.outerHTML, {"indent-inner-html":true });
      } else {
        this.$refs.editor.innerHTML = updateElement.innerHTML;
        this.editor.start();
      }
    }
  },
  components:{
    VueCodemirror
  }
}
</script>
<style>
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
  left:50%;
  top:50%;
  display:none;
}

.ct-inspector {
  display:none;
}
</style>