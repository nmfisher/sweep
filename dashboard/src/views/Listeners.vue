/* eslint-disable */
<template>
  <v-container
    fill-height
    fluid
    grid-list-xl
  >
    <v-expand-x-transition hide-on-leave>
      <v-navigation-drawer v-show="editing" absolute right width="800" style="overflow:visible">
        <template-editor :listener="selected" @close="editing=null"></template-editor>
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
                    @keyup.enter="addEvent"
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
            hide-actions
          >
            <template
              slot="items"
              slot-scope="{ item }"
            >
              <td>
                <v-layout row align-center class="event">
                  <v-flex lg6 xs10>
                    <v-expansion-panel>
                      <v-expansion-panel-content>
                        <template v-slot:header>
                          <h3 slot="activator">{{ item.eventName }} </h3>
                        </template>
                        <v-list>
                            <v-list-tile key="newParam">
                              <v-text-field
                                label="Parameter"
                                  class="purple-input" 
                                  @keyup.enter="addParameter"
                                  v-model="newParameter"></v-text-field>
                            </v-list-tile>
                            <v-list-tile v-for="parameter in item.params" :key="parameter">
                              <v-list-tile-title>
                                  <span style="margin-left:5%">{{parameter}}</span>
                              </v-list-tile-title>
                            </v-list-tile>
                        </v-list>
                      </v-expansion-panel-content>
                    </v-expansion-panel>
                  </v-flex>
                  <v-flex lg4 justify-center style="display:flex">
                    <v-layout row justify-end>
                      <v-tooltip>
                        <v-icon @click="deleteListener(item)" size="36" class="hoverable" slot="activator">
                            mdi-delete
                        </v-icon>
                      </v-tooltip>
                      <v-tooltip>
                        <v-icon @click="editing = item" slot="activator" size="36" class="hoverable">
                            mdi-arrow-right-bold-hexagon-outline
                        </v-icon> 
                        Open template
                      </v-tooltip>
                    </v-layout>
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

import TemplateEditor from './TemplateEditor.vue';
// import CodePreview from './TemplateEditor.vue';

import { TemplateApiFactory, ListenerApiFactory, ListenerApiFp, ListenerApi, ListenerRequestBody, Listener } from '../../lib';

export default {
   data: () => ({
      editing:false,
      selected:null,
      loading:false,
      saving: false,
      showingNewEventField:false,
      listeners: [],
      newListener:"",
      newParameter:"",
  }),
  mounted() {
    var vm = this;
    ListenerApiFactory().listListeners({withCredentials:true}).then((resp) => {
      vm.listeners = resp.data;
    }).catch((err) => {
      vm.$store.state.app.snackbar = err;
    });
  },
  methods: {
    ...mapMutations('app', ['setSnackbar']),
    addEvent(e) {
      var vm = this;
      this.saving = true;
      var req = {eventName:this.newListener}
      ListenerApiFactory().addListener(req, {withCredentials:true}).then((resp) => {
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
</style>
