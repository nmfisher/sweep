<template>
  <v-snackbar
          color="red"
          :bottom="true"
          :left="true"
          v-model="snackbar"
          dark
        >
          <v-icon
            color="white"
            class="mr-3"
          >
            mdi-bell-plus
          </v-icon>
          <div>{{snackbar}}</div>
          <v-icon
            size="16"
            @click="$store.state.app.snackbar = null"
          >
            mdi-close-circle
          </v-icon>
        </v-snackbar>
</template>
<script>
// Utilities
import {
  mapMutations,
  mapState
} from 'vuex'

export default {
  data: () => ({
    
  }),
  computed: {
    snackbar:{
     get () {
        return this.$store.state.app.snackbar;
      },
      set (val) {
        //this.$store.state.app.snackbar = val;
      }
    }
  },
  methods:{
    setSnackbar (snackbar) {
      this.$store.state.app.snackbar = snackbar
    }
  }
}
</script>

<style lang="scss">
  
</style>
