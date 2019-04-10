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
        <!-- <div slot="header">
          <v-layout row @click="$emit('close')" style="cursor:pointer">
            <v-icon size="24">mdi-arrow-left-bold-hexagon-outline</v-icon>
            <h4 class="title font-weight-light ml-2 mt-2 mb-2">E-mail template</h4>
          </v-layout>
        </div> -->
        <v-container fill-height>
          <v-layout column>
            <v-form>
              <v-layout wrap>
                <v-flex xs6>
                    <v-combobox
                        label="From (display name)"
                        v-model="fromName"
                        class="white-input"/>
                    <v-combobox
                        v-model="fromAddress"
                        label="From (address)"
                        class="white-input"/>
                </v-flex>
                <v-flex xs6>
                    <v-combobox
                        label="To (address)"
                        v-model="to"
                        class="white-input"/>
                    <v-text-field
                        label="Subject"
                        class="white-input"/>
                </v-flex>
            </v-layout>
          </v-form>
          <v-layout row wrap xs8>
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
import 'jodit/build/jodit.min.css'
import Vue from 'vue'
import JoditVue from 'jodit-vue'
 
Vue.use(JoditVue)

export default {
  props:{
    listener:Object
  },
   data: () => ({
      joditConfig:{
        defaultMode: JoditVue.MODE_SOURCE,
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
  components: { 
    JoditVue
  },
}
</script>
<style lang="scss">

</style>