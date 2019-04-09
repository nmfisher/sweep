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
          <div>Welcome to <b>Vue Material Dashboard</b> - a beautiful freebie for every web developer.</div>
          <v-icon
            size="16"
            @click="snackbar = false"
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
    ...mapState('app', ['snackbarContent']),
  }),
  computed: {
    ...mapState('app', ['snackbar']),
  },
}
</script>

<style lang="scss">
  
</style>
