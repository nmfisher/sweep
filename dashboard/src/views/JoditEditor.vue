<template>
    <jodit-vue v-model="content" :config="config" :buttons="config.buttons"/>
</template>
<script>
import Vue from 'vue'
import JoditVue from 'jodit-vue'
import Jodit from 'jodit'
import 'jodit/build/jodit.min.css'
Vue.use(JoditVue)
export default {
  data:() => ({
    content:"",
    config:{
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
  }),
  components: { 
    JoditVue
  },
  watch:{
    content(newVal) {
        this.$emit("change", newVal);
    }
  },
  methods:{
    reset() {
      this.content = "";
    }
  }
}
</script>
