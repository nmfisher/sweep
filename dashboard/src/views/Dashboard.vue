<template>
  <v-container
    fill-height
    fluid
    grid-list-xl
  >
    <v-layout wrap>
      <v-flex
        md12
        sm12
        lg4
      >
        <material-chart-card
          v-if="weekEvents"
          :data="eventsChart.data"
          :options="eventsChart.options"
          color="info"
          type="Line"
        >
          <h4 class="title font-weight-light">Events received</h4>
        </material-chart-card>
      </v-flex>
      <v-flex
        md12
        sm12
        lg4
      >
        <material-chart-card
          v-if="weekEmails"
          :data="emailsChart.data"
          :options="emailsChart.options"
          color="blue"
          type="Line"
        >
          <h4 class="title font-weight-light">Emails sent</h4>
        </material-chart-card>
      </v-flex>
      <v-flex
        md12
        sm12
        lg4
      >
        <material-chart-card
          v-if="weekErrors"
          :data="errorsChart.data"
          :options="errorsChart.options"
          color="red"
          type="Line"
        >
          <h3 class="title font-weight-light">Errors</h3>
        </material-chart-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import axios from 'axios';
axios.defaults.withCredentials = true;
import { EventApiFp, EventApiFactory, EventApi, MessageApi } from '../../../clients/lib/typescript-axios/dist/api.js';
import { Configuration, ConfigurationParameters } from '../../../clients/lib/typescript-axios/dist/configuration.js';
import _ from 'lodash';

export default {
  data () {
    return {
      headers: [
        {
          sortable: false,
          text: 'ID',
          value: 'id'
        },
        {
          sortable: false,
          text: 'Name',
          value: 'name'
        },
        {
          sortable: false,
          text: 'Salary',
          value: 'salary',
          align: 'right'
        },
        {
          sortable: false,
          text: 'Country',
          value: 'country',
          align: 'right'
        },
        {
          sortable: false,
          text: 'City',
          value: 'city',
          align: 'right'
        }
      ],
      events: [],
      messages:[],
      tabs: 0,
      list: {
        0: false,
        1: false,
        2: false
      }
    }
  },
  computed:{
    lastWeek() {
      var today = new Date();
      var lastWeek = [...Array(7).keys()].map((d) => {
        var date = new Date();
        date.setDate(today.getDate()-d);
        return date.getDate() + "/" + (date.getMonth() + 1);
      }).reverse();
      return lastWeek;
    },
    eventsChart:(component) => { 
      return {
        data: {
          labels: component.lastWeek,
          series: [component.weekEvents]
        },
        options: {
          low: 0,
          high: 50, 
        }
      }
    },
    emailsChart(component) {
      console.log(component.weekEmails);
      return {
          data: {
            labels: component.lastWeek,
            series: [component.weekEmails]
          },
          options: {
            low: 0,
            high: 100, 
          }
      };
    },
    errorsChart:(component) => ({
        data: {
          labels: component.lastWeek,
          series: [component.weekErrors]
        },
        options: {
          low: 0,
          high: 50,
        },
    }),
    weekErrors() {
      if(this.events == null)
        return [];
      var errors = this.events.filter((event) => event.actions.filter((action) => action.error != null));
      var zeros = this.lastWeek.reduce((o, key) => ({ ...o, [key]: 0}), {});
      var grouped = _.groupBy(errors, (e) => {
        new Date(e).getDate();  
      });
      return Object.values({...zeros, ...grouped});
    },
    weekEvents() {
      if(this.events == null)
        return [];
      var zeros = this.lastWeek.reduce((o, key) => ({ ...o, [key]: 0}), {});

      var grouped = _.groupBy(this.events, (e) => {
        new Date(e).getDate();
      });
      return Object.values({...zeros, ...grouped});
    },
    weekEmails() {
      if(this.messages == null)
        return [];
      
      var zeros = this.lastWeek.reduce((o, key) => ({ ...o, [key]: 0}), {});

      var grouped = _.groupBy(this.messages, (m) => {
        new Date(m).getDate();
      });
      console.log(Object.values({...zeros, ...grouped}));

      return Object.values({...zeros, ...grouped});
    }
  },
  mounted() {
    var vm = this;
    var today = new Date();
    var lastWeek = new Date()
    lastWeek.setDate(today.getDate()-7);
    
    new EventApi().listEvents(true, lastWeek, today, {withCredentials:true}).then((resp) => {
      vm.events = resp.data;
    }).catch((err) => {
      console.error(err);
      vm.$store.state.app.snackbar = err;
    });

    new MessageApi().listMessages(lastWeek, today, {withCredentials:true}).then((resp) => {
      vm.messages = resp.data;
    }).catch((err) => {
      console.error(err);
      vm.$store.state.app.snackbar = err;
    });
  }
}
</script>
