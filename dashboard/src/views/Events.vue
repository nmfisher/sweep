<template>
  <v-container
    fill-height
    fluid
    grid-list-xl
  >
    <v-dialog v-model="showingEmailDialog" max-width="660" scrollable>
       
    </v-dialog>
    <v-layout
      justify-center
      wrap
    >
      <v-flex
        md12
      >
        <material-card
          color="green"
          title="Logs"
        >
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
                <td>
                    <v-icon v-if="!item.error" color="green">mdi-check</v-icon>
                    <v-icon v-if="item.error" color="red">mdi-alert-circle</v-icon>
                </td>
                <td>{{ item.eventName }}</td>
                <td>{{ item.params }}</td>
                <td class="">{{item.receivedOn}}</td>
                <td class="">{{item.processedOn}}</td>
                <td>
                    <v-icon slot="activator" @click="showingEmailDialog = true" style="cursor:pointer" v-if='item.email'>
                        mdi-email
                    </v-icon>
                </td>
            </template>
          </v-data-table>
        </material-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Vue from 'vue'
import {EventApi} from '../../lib/api'
import {
  mapMutations,
  mapState
} from 'vuex'

export default {
    data: () => ({
        content:``,
        showingEmailDialog:false,
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
     
  },
  mounted() {
      var vm = this;
      new EventApi().listEvents({withCredentials:true}).then((resp) => {
        vm.items = resp.data;
      }).catch((err) => {
        vm.$store.state.app.snackbar = err;
      });
  },
  components: {},
}
</script>
<style lang="scss">
.ace_editor {
    border: 1px solid #ccc;
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