<template>
  <v-container
    fill-height
    fluid
    grid-list-xl>
    <v-layout
      justify-center
      wrap
    >
      <v-flex
        xs12
        md8
      >
        <material-card
          color="tertiary"
          title="Profile"
        >
          <v-form>
            <v-container py-0>
              <v-layout wrap>
                <v-flex
                  xs12
                  md4
                >
                  <v-text-field
                    label="Username"
                    v-model="username"
                    disabled/>
                </v-flex>
              </v-layout>
              <v-layout wrap>
                <v-flex
                  xs12
                  md4
                >
                  <v-select
                    :items="plans"
                    item-text="name"
                    item-value="name"
                    label="Plan"
                    v-model="plan"
                  />
                </v-flex>
                <v-flex
                  xs12
                  md4
                >
                  <v-select
                    :items="[`Monthly`, `Annually`]"
                    label="Billing period"
                    v-model="billingPeriod"
                  />
                </v-flex>
              </v-layout>
              <v-layout wrap>
                <p v-if="success">Thanks! Your plan will be upgraded in the next 24-48 hours. Any events above quota will continue to be queued (but not processed) once the plan change is processed.</p>
              </v-layout>
              <v-layout wrap>
                <v-btn
                  color="success"
                  @click="update"
                  round
                  class="font-weight-light"
                >Update</v-btn>
              </v-layout>
            </v-container>
          </v-form>
        </material-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { UserApi, EventApi } from '../../../clients/lib/typescript-axios';

export default {
  data:() => ({
    success:false,
    billingPeriod:'Monthly',
    plan:"Basic",
    username:null,
    plans:[{name:'Basic',emails:9,annualPrice:'Free', monthlyPrice:'Free'},
            {name:'Pro', emails:99,annualPrice:'$14/month', monthlyPrice:'$19/month'},
            {name:'Enterprise', emails:999,annualPrice:'$79/month', monthlyPrice:'$99/month'}]
  }),
  mounted () {
    var vm = this;
    new UserApi().getUserInfo({withCredentials:true}).then((resp) => {
        console.log(resp.data);
        vm.username = resp.data.id;
    }).catch((err) => {
      console.error(err);
      vm.$store.state.app.snackbar = err;
    });
  },
  methods:{
    update() {
      var vm = this;
      this.success = false;
      new EventApi().addEvent(
        {eventName:"upgrade_plan", params:{"plan":this.plan, "billingPeriod":this.billingPeriod}}, 
        process.env.VUE_APP_SWEEP_APIKEY, 
        {withCredentials:true}).then((resp) => {
            vm.success = true;
      }).catch((err) => {
        console.error(err);
        vm.$store.state.app.snackbar = err;
      });
    }
  }
}
</script>
