/* eslint-disable */
<template>
  <v-container
    fill-height
    fluid
    grid-list-xl
  >
    <v-expand-x-transition hide-on-leave>
      <v-navigation-drawer v-show="editing" absolute right width="800" style="overflow:visible">
        <template-editor :listener="selected" @close="editing=null;selected=null;"></template-editor>
      </v-navigation-drawer>
    </v-expand-x-transition>
    <v-layout
      justify-center
      wrap
    >
      <v-flex
        md12
      >
        <material-card
          color="tertiary"
        >
         <div
            slot="header"
          >
            <v-layout row wrap align-center>
              <v-flex xs12 lg6>
                <h4 class="title font-weight-light mb-2">Events & Templates</h4>
              </v-flex>
              <v-flex
                xs12
                lg5
              >
                <v-slide-x-transition>
                  <v-text-field
                    v-show="showingNewEventField"
                    :disabled="saving"
                    label="Event Name"
                    color="white"
                    @keyup.enter="addListener"
                    v-model="newListener"/>
                </v-slide-x-transition>
              </v-flex>
              <v-flex xs1>
                  <v-icon
                    class="rotate"
                    v-if="saving"
                  >
                    mdi-reload
                  </v-icon>
                  <v-icon
                    class="blink"
                    v-if="!saving && !showingNewEventField"
                    @click="showingNewEventField = true"
                    size="24"
                  >
                    mdi-plus-circle
                  </v-icon>
                  <v-icon
                    class="blink"
                    v-if="!saving && showingNewEventField"
                    @click="showingNewEventField = false"
                    size="24"
                  >
                    mdi-close-circle
                  </v-icon>
              </v-flex>
              </v-layout>
          </div>
          <v-data-table
            :items="listeners"
            no-data-text="Looks like you haven't configured any listeners. Click the add icon above to listen for an event."
            style="overflow:visible"
            hide-actions
          >
            <template
              slot="items"
              slot-scope="{ item:listener }"
            >
              <td style="position:relative">
                <v-layout row align-center class="event" wrap @click="selected=listener;" style="cursor:pointer">
                  <v-flex xs9>
                      <v-layout row wrap>
                        <v-flex xs12>
                          <h3 class="mt-0 mb-0">{{ listener.eventName }} </h3>
                        </v-flex>
                        <v-flex lg4 xs12>
                          <v-slide-x-transition>
                            <v-combobox
                                v-show="selected == listener"
                                :error="listener.paramErrors && listener.paramErrors.length > 0"
                                :error-messages="listener.errorMessages"
                                v-model="listener.eventParams"
                                :items="listener.eventParams"
                                @change="update(listener)"
                                hide-selected
                                hint="May contain a-zA-Z0-9_"
                                label="Type an event parameter and press enter"
                                multiple
                                persistent-hint
                                small-chips
                              >
                              <template v-slot:selection="{ item : paramName, parent, selected }">
                                <v-chip
                                  :color="listener.paramErrors && listener.paramErrors.includes(paramName) ? 'red' : ''"
                                  :class="listener.paramErrors && listener.paramErrors.includes(paramName) ? 'white--text' : ''"
                                >
                                    {{ paramName }}
                                <v-icon
                                      small
                                      @click="listener.eventParams.splice(listener.eventParams.indexOf(paramName), 1)"
                                    >mdi-close</v-icon>
                                </v-chip>
                              </template>
                            </v-combobox>
                          </v-slide-x-transition>
                        </v-flex>
                    </v-layout>
                  </v-flex>
                  <v-flex xs3>
                    <v-slide-x-transition>
                      <v-layout row v-show="selected == listener">
                        <v-icon @click="deleteListener(listener)" outline color="red" class="white--text" style="position:absolute;right:-10px;top:-10px">
                            mdi-close-box
                        </v-icon>
                        <v-btn @click="editing = true; selected=listener" outline color="indigo" class="white--text">
                            Edit template
                        </v-btn> 
                      </v-layout>
                    </v-slide-x-transition>
                  </v-flex>
                </v-layout>
              </td>
            </template>
          </v-data-table>
        </material-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import {
  mapMutations,
  mapState
} from 'vuex'
import Vue from 'vue'

import TemplateEditor from './TemplateEditor.vue';
// import CodePreview from './TemplateEditor.vue';

import { TemplateApiFactory, ListenerApiFactory, ListenerApiFp, ListenerApi, ListenerRequestBody, Listener } from '../../lib/api';

export default {
   data: () => ({
      editing:false,
      selected:null,
      loading:false,
      saving: false,
      showingNewEventField:false,
      listeners: [],
      newListener:"",
  }),
  mounted() {
    var vm = this;
    ListenerApiFactory().listListeners(null,{withCredentials:true}).then((resp) => {
      vm.listeners = resp.data;
    }).catch((err) => {
      vm.$store.state.app.snackbar = err;
    });
  },
  methods: {
    ...mapMutations('app', ['setSnackbar']),
    addListener(e) {
      var vm = this;
      this.saving = true;
      var req = {eventName:this.newListener}
      new ListenerApi().addListener(req, null, {withCredentials:true}).then((resp) => {
        resp.data.eventParams = [];
        this.listeners.splice(0,0,resp.data);
        vm.newListener = null;
      }).catch((err) => {
        console.error(err);
        vm.$store.state.app.snackbar = err;
      }).finally(() => {
        vm.saving = false;
      });
    },
    deleteListener(listener) {
      var vm = this;
      new ListenerApi().deleteListener(listener.id, null, {withCredentials:true}).then((resp) => {
        vm.listeners.splice(vm.listeners.indexOf(listener), 1);
      }).catch((err) => {
        console.error(err);
        vm.$store.state.app.snackbar = err;
      });
    },
    update(item) {
      var vm = this;
      item.paramErrors = [];

      for(var i = 0; i < item.eventParams.length; i++) {
        if(/[^a-zA-Z0-9_]/.test(item.eventParams[i])) {
          item.paramErrors.push(item.eventParams[i]);
        };
      }
      if(item.paramErrors.length > 0) {
        item.errorMessages = ["Invalid parameter - keys can only contain a-zA-Z0-9_"];
        return;
      }
            
      new ListenerApi().updateListener(item.id, item, null, {withCredentials:true}).then((resp) => {

      }).catch((err) => {
        console.error(err);
        vm.$store.state.app.snackbar = err;
      });
    }
  },
  components: { 
    TemplateEditor//, CodePreview
  },
}
</script>
<style>

.event .hoverable {
  visibility:hidden;
}

.event:hover .hoverable {
  visibility:visible;
}

.v-table__overflow {
  overflow:visible !important;
}
</style>
