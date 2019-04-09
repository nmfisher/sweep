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
    ...mapState('app', ['snackbar']), 
  },
}
</script>

<style lang="scss">
  
</style>
