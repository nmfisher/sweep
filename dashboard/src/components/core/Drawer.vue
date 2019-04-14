/* eslint-disable */
<template>
  <v-navigation-drawer
    id="app-drawer"
    v-model="inputValue"
    app
    dark
    floating
    persistent
    mobile-break-point="991"
    width="260"
  >
      <v-layout
        class="fill-height"
        tag="v-list"
        column
      >
      <v-list-tile style="text-align:center;margin-top:25px;margin-bottom:15px;">
          <v-list-tile-title class="title">
            <p style="font-size: 36px !important;color:#4950F6;line-height: 56px;letter-spacing: 0px;font-weight: 600;">sweep</p>
          </v-list-tile-title>
        </v-list-tile>
        <v-divider/>
        <v-list-tile
          v-for="(link, i) in links"
          :key="i"
          :to="link.to"
          :href="link.href"
          :active-class="color"
          avatar
          class="v-list-item"
        >
          <v-list-tile-action>
            <v-icon>{{ link.icon }}</v-icon>
          </v-list-tile-action>
          <v-list-tile-title
            v-text="link.text"
          />
        </v-list-tile>
        <h5>API Key</h5>
        <p>{{apiKey}}</p>
      </v-layout>
  </v-navigation-drawer>
</template>

<script>
// Utilities
import {
  mapMutations,
  mapState
} from 'vuex'
import { UserApiAxiosParamCreator, UserApiFp } from '../../../lib/api';
import { UserApiFactory } from '../../../lib/api';
import { UserApi } from '../../../lib/api';

export default {
  data: () => ({
    apiKey:null,
    logo: './img/vuetifylogo.png',
    links: [
      {
        to: '/dashboard',
        icon: 'mdi-view-dashboard',
        text: 'Dashboard'
      },
      {
        to: '/listeners',
        icon: 'mdi-clipboard-outline',
        text: 'Listeners'
      },
      {
        to: '/events',
        icon: 'mdi-format-font',
        text: 'Events'
      },
      {
        to: '/user-profile',
        icon: 'mdi-account',
        text: 'Account',
      },
      {
        icon: 'mdi-logout',
        href:'/logout',
        text: 'Logout',
      },
    ],
    responsive: false
  }),
  computed: {
    ...mapState('app', ['image', 'color']),
    inputValue: {
      get () {
        return this.$store.state.app.drawer
      },
      set (val) {
        this.setDrawer(val)
      }
    },
    items () {
      return this.$t('Layout.View.items')
    }
  },
  mounted () {
    var vm = this;
    new UserApi().getUserInfo({withCredentials:true}).then((resp) => {
        vm.apiKey = resp.data.apiKey;
    }).catch((err) => {
      console.error(err);
    })
    this.onResponsiveInverted()
    window.addEventListener('resize', this.onResponsiveInverted)

  },
  beforeDestroy () {
    window.removeEventListener('resize', this.onResponsiveInverted)
  },
  methods: {
    ...mapMutations('app', ['setDrawer', 'toggleDrawer']),
    onResponsiveInverted () {
      if (window.innerWidth < 991) {
        this.responsive = true
      } else {
        this.responsive = false
      }
    }
  }
}
</script>

<style lang="scss">
  #app-drawer {
    .v-list__tile {
      border-radius: 4px;

      &--buy {
        margin-top: auto;
        margin-bottom: 17px;
      }
    }

    .v-image__image--contain {
      top: 9px;
      height: 60%;
    }

    .search-input {
      margin-bottom: 30px !important;
      padding-left: 15px;
      padding-right: 15px;
    }
  }
</style>
