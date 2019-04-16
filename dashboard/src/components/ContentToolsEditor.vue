<template>
  <v-layout column fill-height id="tribute-target">
    <textarea ref="foo"></textarea>
    <v-flex style="border:1px solid #ccc">
      <v-slide-x-transition>
          <div id="editor" data-name="main-content" v-show="!editRaw">
          </div>
      </v-slide-x-transition>
      <v-slide-x-transition>
        <codemirror ref="codemirror" v-model="raw" v-show="editRaw" @key-handled="foo" @before-change="bar" @input-read="baz"></codemirror>
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
  }
});
import '../../node_modules/codemirror/lib/codemirror.css'
import '../../node_modules/codemirror/theme/shadowfox.css'

export default {
  props:{
    tribute:Object,
    editRaw:Boolean,
  },
  data:() => ({
    editor:null,
    raw:"",
  }),
  methods:{
    baz(inst, chg) {
      console.log(chg);
    },
    foo(inst, name, evt) {
      if(this.tribute.isActive && (evt.keyCode == 9 || evt.keyCode == 13)) {
        // evt.preventDefault();
        // evt.stopPropagation();
        // console.log("preventing");
        // return false;
      }
    },
    bar(inst, chng) {
      if(this.tribute.isActive && (chng.text == "\t" || (chng.text[0] == "" && chng.  text[1] == ""))) {
        chng.cancel();
      }

    }
  },
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
      })
    });

    
    this.editor.start();
  },
  watch:{
    tribute(newVal) {
      if(newVal) {
          newVal.attach(this.$refs.foo);
          newVal.attach(this.$refs.codemirror.$el.getElementsByTagName("textarea")[0]);
          newVal.attach(this.$refs.codemirror.$el.getElementsByTagName("textarea")[1]);
          this.$refs.codemirror.$el.getElementsByTagName("textarea")[1].addEventListener("tribute-replaced", () => {
              console.log("replacd");
          });
          this.$refs.codemirror.$el.getElementsByTagName("textarea")[0].addEventListener("tribute-replaced", () => {
              console.log("replacd");
          });
          this.$refs.foo.addEventListener("tribute-replaced", () => {
              console.log("replacd");
          });
      }
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
        Vue.nextTick(() => {
          vm.tribute.attach(vm.$refs.codemirror.$el);
        });
        
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
  left:50%;
  top:50%;
  /* display:none; */
}

.ct-inspector {
  display:none;
}
</style>