<template>
  <v-container
    fill-height
    fluid
    grid-list-xl
  >

    <v-expand-x-transition hide-on-leave>
      <v-navigation-drawer v-show="editing" absolute right width="800" style="overflow:visible">
        <template-editor :listener="selected" @close="editing=false;selected=null;" :active="editing"></template-editor>
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
                <h4 class="title font-weight-light mb-2">Listeners</h4>
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
          <v-layout row v-if="loading" justify-center>
            <v-icon class="rotate mt-5 mb-5">
              mdi-reload
            </v-icon>
          </v-layout>
          <v-data-table
            v-if="!loading"
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
                <v-form :ref="listener.id">
                <v-layout row align-center class="event" wrap @click="selected=listener;" style="cursor:pointer">
                  <v-flex xs9>
                        <h3 class="mt-0 mb-0">{{ listener.eventName }} </h3>
                        <v-slide-x-transition>
                          <v-layout column align-start wrap v-show="selected == listener" v-if="selected"> 
                            <v-flex xs12 style="display:flex;align-items:center;width:50% ">
                                <span class="mr-2">AND</span> 
                                <v-text-field class="mr-2" :rules="[rules.required]" v-model="selected.triggerEvent" hint="Event" @change="update(listener)"></v-text-field> 
                            </v-flex>
                            <v-flex xs12 style="display:flex;align-items:center">
                              <span class="mr-2">WITHIN</span>
                              <v-select class="mr-2" :rules="[rules.required]" :items='[1,2,3,4,5,6,7]' v-model="selected.triggerNumber" hint="Number" @change="update(listener)"></v-select>
                              <v-select class="mr-2" :rules="[rules.required]" :items="[`DAYS`,`HOURS`]" hint="Period" v-model="selected.triggerPeriod" @change="update(listener)"></v-select> 
                            </v-flex>
                            <v-flex xs12 style="display:flex;align-items:center">
                              <span class="mr-2">MATCH ON</span> 
                              <v-text-field class="mr-2" :rules="[rules.required]" v-model="selected.triggerMatch" @change="update(listener)" ></v-text-field>
                            </v-flex>
                            <v-flex xs12 style="display:flex;align-items:center">
                              <v-combobox
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
                                        @click="listener.eventParams.splice(listener.eventParams.indexOf(paramName), 1); update(listener)"
                                      >mdi-close</v-icon>
                                  </v-chip>
                                </template>
                              </v-combobox>
                            </v-flex>
                            <v-flex xs12>
                                <v-menu style="position:absolute;right:-10px;top:-10px">
                                  <v-icon slot="activator" outline color="red" class="white--text">
                                    mdi-close-box
                                  </v-icon>
                                  <v-card style="padding:15px">
                                    <v-layout column align-center>
                                      Are you sure you want to delete this listener?
                                      <v-btn color="red" class="white--text" @click="deleteListener(listener)">
                                        Delete
                                      </v-btn>
                                    </v-layout>
                                  </v-card>
                                </v-menu>
                                <v-btn @click="editing = true; selected=listener" outline color="indigo" class="white--text">
                                    Edit template
                                </v-btn> 
                            </v-flex>
                          </v-layout>
                          </v-slide-x-transition>
                  </v-flex>
                </v-layout>
                </v-form>
              </td>
            </template>
          </v-data-table>
        </material-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import {
  mapMutations,
  mapState
} from 'vuex'
import Vue from 'vue'
import TemplateEditor from '../components/TemplateEditor.vue';

import { TemplateApiFactory, ListenerApiFactory, ListenerApiFp, ListenerApi, ListenerRequestBody, Listener } from '../../../clients/lib/typescript-axios';

export default {
   data: () => ({
      editing:false,
      selected:null,
      selectedTriggerError:null,
      loading:false,
      saving: false,
      showingNewEventField:false,
      listeners: [],
      newListener:"",
  }),
  mounted() {
    var vm = this;
    vm.loading = true;
    ListenerApiFactory().listListeners({withCredentials:true}).then((resp) => {
      vm.listeners = resp.data;
    }).catch((err) => {
      vm.$store.state.app.snackbar = err;
    }).finally(() => {
      vm.loading = false;
    });
  },
  methods: {
    ...mapMutations('app', ['setSnackbar']),
    addListener(e) {
      var vm = this;
      this.saving = true;
      var req = {eventName:this.newListener}
      new ListenerApi().addListener(req, {withCredentials:true}).then((resp) => {
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
      new ListenerApi().deleteListener(listener.id, {withCredentials:true}).then((resp) => {
        vm.listeners.splice(vm.listeners.indexOf(listener), 1);
      }).catch((err) => {
        console.error(err);
        vm.$store.state.app.snackbar = err;
      });
    },
    update(item) {
      var vm = this;
      item.paramErrors = [];
      if(!item.eventParams) {
        item.eventParams = [];
      }
      if(!this.$refs[item.id].validate())
        return;

      for(var i = 0; i < item.eventParams.length; i++) {
        if(/[^a-zA-Z0-9_]/.test(item.eventParams[i])) {
          item.paramErrors.push(item.eventParams[i]);
        };
      }
      if(item.paramErrors.length > 0) {
        item.errorMessages = ["Invalid parameter - keys can only contain a-zA-Z0-9_"];
        return;
      }
            
      new ListenerApi().updateListener(item.id, item, {withCredentials:true}).then((resp) => {

      }).catch((err) => {
        console.error(err);
        vm.$store.state.app.snackbar = err;
      });
    }
  },
  components: { 
    TemplateEditor
  },
  computed:{
    rules() {
      var vm = this;
      return {
        required(val) {
          var els = [vm.selected.triggerEvent, vm.selected.triggerPeriod, vm.selected.triggerNumber, vm.selected.triggerMatch];
          if (els.every((x => x != null && x.length > 0))) {
            return true;
          } else {
            return els.filter((x) => x === null || x.length == 0 || x == 0).length > 0 ? "If a trigger is provided, all values must be filled in." : true;          
          }
        }
      }
    }
  }
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
