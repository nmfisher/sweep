<template>
  <v-container
    fill-height
    fluid
    grid-list-xl
  >
  <v-dialog v-model="showingErrorDialog">
    <v-card height="500">
      <v-layout row fill-height align-center>
        <v-list v-if="showingErrorItem">
          <template v-for="action in showingErrorItem.actions">
            <v-list-tile v-if="action.error" :key="action.error">
              {{action.error}}
            </v-list-tile>
          </template>
        </v-list>
      </v-layout>
    </v-card>
  </v-dialog>
    <v-layout
      justify-center
      wrap
    >
      <v-flex
        md12
      >
      <v-scroll-x-transition hide-on-leave>
        <material-card
          v-show="!showMessages"
          color="primary"
          title="Logs"
        >
          <v-layout row v-if="loading" justify-center>
            <v-icon  class="rotate mt-5 mb-5">
              mdi-reload
            </v-icon>
          </v-layout>
            <v-data-table
            v-if="!loading"
              v-show="!showMessages" 
              :headers="headers"
              :items="items"
              expand
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
                  <td>
                      <v-icon v-if="item.actions.length == 0">
                        mdi-sleep
                      </v-icon>
                      <template v-else>
                        <v-icon v-if="item.status == `Error`" color="red" @click="showingErrorDialog=true;showingErrorItem=item">mdi-alert-circle</v-icon>
                        <v-icon v-if="item.status == `Completed`" color="green">mdi-check</v-icon>
                        <v-icon v-if="item.status == `Pending`" color="gray">mdi-help</v-icon>
                      </template>
                  </td>
                  <td>{{ item.eventName }}</td>
                  <td>{{ item.params }}</td>
                  <td class="">{{item.receivedOn}}</td>
                  <td class="">{{item.processedOn}}</td>
                  <td>
                      <v-icon style="cursor:pointer" @click="showMessages = true; loadMessages(item)" >
                          mdi-email
                      </v-icon>
                  </td>
              </template>
            </v-data-table>
          </material-card>
        </v-scroll-x-transition>
        <v-scroll-x-transition hide-on-leave>
           <material-card v-show="showMessages" color="tertiary">
            <div slot="header">
              <v-layout row align-center class="mb-2 mt-2">
                <v-icon @click="showMessages = false" class="mr-2 ml-2">mdi-arrow-left</v-icon>
                <h4 class="title font-weight-light mb-0" style="margin:0 !important">Emails</h4>
              </v-layout>
            </div>
            <v-layout row v-if="loading" justify-center>
              <v-icon  class="rotate mt-5 mb-5">
                mdi-reload
              </v-icon>
            </v-layout>
              <v-layout row align-center justify-center fill-height v-if="!loading">
                <v-flex xs12 v-if="messages">
                  <v-expansion-panel >
                    <v-expansion-panel-content
                      v-for="message in messages"
                      :key="message.id"
                      v-model="message.active"
                    >
                      <template v-slot:header>
                        Subject: {{ message.subject }}
                      </template>
                      <v-list>
                        <v-list-tile>
                          From: <v-chip>{{message.fromName}} ({{message.fromAddress}})</v-chip>
                        </v-list-tile>
                        <v-list-tile>
                          To: <v-chip v-for="recipient in message.sendTo" :key="recipient">{{recipient}}</v-chip>
                        </v-list-tile>
                      </v-list>
                      <v-flex xs12>
                        <iframe :srcdoc="message.content" height="100%" width="100%" style="background:#fff; border: 1px solid #ccc;border-radius: 3px;flex-grow:1"></iframe>
                      </v-flex>
                    </v-expansion-panel-content>
                  </v-expansion-panel>
                </v-flex>
                <template v-if="!messages || (messages && messages.length == 0)">
                    <v-flex xs12>
                      None found.
                    </v-flex>
                </template>
              </v-layout> 
           </material-card>
          </v-scroll-x-transition>
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
import MessagePreview from '../components/MessagePreview.vue';
import { EventApi, ListenerApi } from '../../../clients/lib/typescript-axios'

export default {
    data: () => ({
        content:``,
        loading:false,
        showMessages:false,
        showingErrorItem:null,
        showingErrorDialog:false,
        messages:null,
        headers: [
            {
                sortable: true,
                text: 'Status',
                value: 'status'
          },
          {
            sortable: true,
            text: 'Event',
            value: 'event'
          },
          {
            sortable:true,
            text:'Parameters',
            value:'parameters'
          },
          {
            sortable:true,
            text:'Received On',
            value:'received'
          },
          {
            sortable:true,
            text:'Processed On',
            value:'processed'
          },
          {
            sortable:false,
            text:'E-mails',
            value:'code'
          },
        ],
        items: [],
  }),
  methods: {
    loadMessages(event) {
      var vm = this;
      this.loading = true;
      var api = new ListenerApi();
      vm.messages = [];
      var promises = event.actions.map((action) => {
        return api.listMessagesForAction(action.id, {withCredentials:true}).then((resp) =>{
          resp.data.forEach((message) => {
              console.log(vm);
              vm.messages.push(message);
              console.log(message);
          });
        }).catch((err) => {
          console.error(err);
          vm.$store.state.app.snackbar = err;
        });
      });
      Promise.all(promises).finally(() => {
        vm.loading = false;
      });
    }
  },
  mounted() {
      var vm = this;
      this.loading = true;
      
      new EventApi().listEvents(true, undefined, undefined, {withCredentials:true}).then((resp) => {
        vm.items = resp.data;
        vm.items.forEach((item)=> {
          item.status = "Completed";
          for(var i = 0; i < item.actions.length; i++) {
            if(item.actions[i].error != null) {
              item.status = "Error";
              break;
            }
            if(item.actions[i].completed != true) {
              item.status = "Pending";
              break;
            }
          }
        });
      }).catch((err) => {
        console.error(err);
        vm.$store.state.app.snackbar = err;
      }).finally(() => {
        vm.loading = false;
      })
  },
  components: {
    MessagePreview
  },
}
</script>
<style>
</style>