<template>
  <v-container
    fill-height
    fluid
    grid-list-xl
  >
    <v-dialog v-model="showingTemplateDialog" max-width="1060" scrollable>
        <v-card style="overflow:hidden;height:720px">
            <v-layout row>
                <v-flex xs2 style="display:flex;flex-direction:column">
                    <v-list>
                        <v-list-tile v-for="parameter in Object.keys(parameters)">
                            <v-list-tile-title>
                                {{parameter}}
                            </v-list-tile-title>
                        </v-list-tile>
                    </v-list>
                    <v-btn color="primary" style="justify-self:end;margin-top:auto">Save</v-btn>
                </v-flex>
                <v-flex xs10>
                    <v-layout row>
                        <v-flex xs6>
                                <v-list>
                                    <v-list-tile>
                                      <v-text-field
                                            label="To"
                                            class="purple-input"/>
                                    </v-list-tile>
                                    <v-list-tile>
                                      <v-text-field
                                            label="From"
                                            class="purple-input"/>
                                    </v-list-tile>
                                    <v-list-tile>
                                      <v-text-field
                                            label="Subject"
                                            class="purple-input"/>
                                    </v-list-tile>
                                </v-list>
                            </v-flex>
                    </v-layout>
                    <v-layout row wrap style="padding:7px">
                        <v-flex xs6>
                            <p>HTML</p>
                            <!-- <jodit-vue v-model="content" :config="joditConfig" :buttons="joditConfig.buttons"/> -->
                                <editor v-model="content" @init="editorInit" lang="html" theme="chrome"></editor>
                        </v-flex>
                        <v-flex xs6 style="display:flex;flex-direction:column;padding: 0px 15px;">
                            <p>PREVIEW</p>
                            <iframe :srcdoc="content" height="100%" width="100%" style="border: 1px solid #ccc;border-radius: 3px;flex-grow:1"></iframe>
                        </v-flex>
                    </v-layout>
                </v-flex>
            </v-layout>
        </v-card>
    </v-dialog>
    <v-dialog v-model="showingCodeDialog" max-width="620">
         <v-card>
            <v-tabs
                  v-model="tabs"
                  color="transparent"
                  slider-color="white"
            >
                <v-tab class="mr-3">
                    C#
                </v-tab>
                <v-tab class="mr-3">
                    F#
                </v-tab>
                <v-tab class="mr-3">
                    Javascript/NodeJS
                </v-tab>
                <v-tab>
                    Python
                </v-tab>
            </v-tabs>
            <v-tabs-items v-model="tabs"
            >
                <v-tab-item>
                        <pre class="language-csharp"><code>using Sweep;
Sweep.Raise("signup", new { username="bill@gates.com", firstName="Bill", lastName="Gates"});</code>
                        </pre>
                        <v-btn
                             class="elevation-0"
                              fab
                              dark
                              absolute
                              right
                              top
                              style="top:10px;margin:0;background:none"
                          >
                            <v-icon>mdi-content-copy</v-icon>
                            COPY
                        </v-btn>
                </v-tab-item>
                <v-tab-item>
                    <pre class="language-fsharp"><code>open Sweep;
Sweep.Raise "signup" {| username="bill@gates.com"; firstName="Bill"; lastName="Gates"|};</code>
                    </pre>
                </v-tab-item>
                <v-tab-item>
                    <pre class="language-js"><code>import Sweep from './sweep';
Sweep.raise("signup", {"username":"bill@gates.com", firstName:"Bill", lastName:"Gates"});</code>
                    </pre>
                </v-tab-item>
                <v-tab-item>
                    <pre class="language-python">from Sweep import Sweep
<code>Sweep.raise("signup", { "username":"bill@gates.com", "firstName":"Bill"})</code>
                    </pre>
                </v-tab-item>
            </v-tabs-items>
        </v-card>
    </v-dialog>
    <v-layout
      justify-center
      wrap
    >
      <v-flex
        md12
      >
        <material-card
          color="tertiary"
          title="Events & Templates"
        >
        <v-flex 
            xs1
        >
            <v-btn
              href="https://www.creative-tim.com/product/vuetify-material-dashboard"
              target="_blank"
              color="secondary"
              block
            >
                Create
            </v-btn>
        </v-flex>
          <v-data-table
            :headers="headers"
            :items="items"
            hide-actions
          >
            <template
              slot="headerCell"
              slot-scope="{ header }"
            >
              <span
                class="subheading font-weight-light text-success text--darken-3"
                v-text="header.text"
              />
            </template>
            <template
              slot="items"
              slot-scope="{ item }"
            >
              <td><span>{{ item.event }}</span></td>
              <td class="">
                <v-menu
                bottom
                left
                :close-on-content-click="false"
                :close-on-click="false"
                content-class="dropdown-menu"
                offset-y
                transition="slide-y-transition">     
                    <v-icon slot='activator'>mdi-code-parentheses</v-icon>
                    <v-card>
                        <v-list>
                            <v-layout row style="padding-left:10%">
                             {
                            </v-layout>
                          <v-list-tile v-for="parameter in Object.keys(parameters)">
                            <v-list-tile-title>
                                <span style="margin-left:5%">"{{parameter}}":"",</span>
                            </v-list-tile-title>
                          </v-list-tile>
                          <v-layout row style="padding-left:10%">
                             }
                        </v-layout>
                        </v-list>
                    </v-card>
                </v-menu>
              </td>
            <td>
                <v-icon slot="activator" @click="showingCodeDialog = true" style="cursor:pointer">mdi-code-not-equal-variant</v-icon>
            </td>
            <td>
                <v-icon @click="showingTemplateDialog = true">
                    mdi-animation
                </v-icon></td>
            </template>
          </v-data-table>
        </material-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Vue from 'vue'
import VuePrism from 'vue-prism'
Vue.use(VuePrism);
import 'prismjs/components/prism-csharp'
import 'prismjs/components/prism-fsharp'
import 'prismjs/components/prism-python'
import 'prismjs/themes/prism-twilight.css'
import 'jodit/build/jodit.min.css'
import AceEditorOnVue from 'ace-editor-on-vue';
import JoditVue from 'jodit-vue'
import Jodit from 'jodit';
export default {
    methods: {
        editorInit: function () {
            require('brace/ext/language_tools') //language extension prerequsite...
            require('brace/mode/html')                
            require('brace/mode/javascript')    //language
            require('brace/mode/less')
            require('brace/theme/chrome')
            require('brace/snippets/javascript') //snippet
        }
    },
    components: { JoditVue, 
        editor: require('vue2-ace-editor'),
    },
  data: () => ({
   content: `<html style="background:#eee;color:#333;font-family:'Helvetica'">
   <h1>Hi %%username%%</h1>
   <p>Thanks for signing up!</p>
   <p>Best regards,</p>
   <p>Bill</p>
   </html>`,
    config: {
        lang: 'json', // default `json`
        theme: 'xcode', // default `xcode`
        options: { // options for Ace
            useSoftTabs: true, // default 2 space characters for indent
            tabSize: 2
        }
    },
    joditConfig:{
        defaultMode: Jodit.MODE_SOURCE,
        buttons: ['source',
        '|',
        'fontsize',
        'font',
        'bold',
        'strikethrough',
        'underline',
        'italic',
        '|',
        'superscript',
        'subscript',
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
        'align',
        'hr',
        '|',
        'symbol'],
        toolbarButtonSize: "large"
    },
    showingCodeDialog:false,
    showingTemplateDialog:false,
    parameters:{"username":"bill@gates.com", 
        "firstName":"Bill",
        "lastName":"Gates"
        },
    headers: [
      {
        sortable: false,
        text: 'Event',
        value: 'event'
      },
      {
        sortable:false,
        text:'Parameters',
        value:'parameters'
      },
      {
        sortable:false,
        text:'Code',
        value:'code'
      },
      {
        sortable: false,
        text: 'Template',
        value: 'template'
      },
    ],
    items: [
      {
        event: 'signup',
      },
      {
        event: 'signup + 7 days',
      },
      {
        event: 'click_widget',
      },
      {
        event: 'request_password_change',
      },
      {
        event: 'password_changed',
      },
      {
        event: '[none]',
      },
    ],
    tabs:0
  })
}
</script>
<style lang="scss">
.ace_editor {
    border: 1px solid #ccc;
}
.jodit_container {
    
}
.jodit_workplace {
    height:410px !important;
}
.jodit_source {
    overflow-y:scroll;
    height:100%;
}
pre {
    padding-top:35px !important;
}
code {
    box-shadow:none;
    white-space: pre-wrap !important;
}
code::before {
    content:''
}
</style>