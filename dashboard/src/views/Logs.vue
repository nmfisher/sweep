<template>
  <v-container
    fill-height
    fluid
    grid-list-xl
  >
    <v-dialog v-model="showingEmailDialog" max-width="660" scrollable>
        <v-card style="overflow:hidden;height:auto">
            <v-list>
                <v-list-tile>
                    <v-flex xs2>
                        To:
                    </v-flex>
                    <v-flex xs9>
                        <v-chip>bill@gates.com</v-chip>
                    </v-flex>
                </v-list-tile>
                <v-divider/>
                <v-list-tile>
                  <v-flex xs2>
                        From:
                    </v-flex>   
                    <v-flex xs9>
                        <v-chip>help@mysaas.com</v-chip>
                    </v-flex>
                </v-list-tile>
                <v-divider/>
                <v-list-tile>
                  <v-flex xs2>
                        Subject:
                    </v-flex>
                    <v-flex xs9>
                        <v-chip>Welcome to MySaaS!</v-chip>
                    </v-flex>
                </v-list-tile>
                <v-divider/>
            </v-list>
            <v-card tile style="border-radius:0;margin:15px;border-left:2px solid #9c27b0;padding-left:15px" class='elevation-0'>
                <h3>Hi Bill!</h3>
                <p>Thanks for signing up to MySaaS, the sassiest SaaS on the planet.</p>
                <p>For future reference, your username is bill@gates.com</p>
                <p>We're transforming the world through paradigm shifts in big data for  Enterprise SaaS on the Blockchain.</p>
                <p>We'll be in touch soon.</p>
                <p>The MySaaS Team.</p>
                <p>---</p>
                <p>MySaaS Inc.</p>
                <p>San Francisco.</p>
            </v-card>
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
                    <v-icon v-if="item.status == `ok`" color="green">mdi-check</v-icon>
                    <v-icon v-if="item.status == `error`" color="red">mdi-alert-circle</v-icon>
                </td>
                <td>{{ item.event }}</td>
                <td>{{ item.parameters }}</td>
                <td class="">{{item.receivedOn}}</td>
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


export default {
    methods: {
     
    },
    components: {},
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
            sortable:false,
            text:'E-mails',
            value:'code'
          },
        ],
        items: [
          {
            event: 'signup',
            receivedOn:'2019-01-17 04:35',
            status:'ok',
            parameters:{ username:'bill@gates.com', firstName:'Bill', lastName:'Gates' },
            email:true
          },
          {
            event: 'signup',
            receivedOn:'2019-01-17 05:15',
            status:'ok',
            parameters:{ username:'melinda@gates.com', firstName:'Melinda', lastName:'Gates' },
            email:true
          },
          {
            event: 'login',
            receivedOn:'2019-01-17 14:50',
            status:'error',
            parameters:{ username:'steve@ballmer.com', firstName:'Steve', lastName:'Ballmer' },
            email:true
          },
          {
            event: 'clickHelp',
            receivedOn:'2019-01-18 00:05',
            status:'error',
            parameters:{ username:'steve@ballmer.com', firstName:'Steve', lastName:'Ballmer' },
            email:true
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