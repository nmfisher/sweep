// Styles

// Extensions
import { VCombobox } from '../../../../node_modules/vuetify/es5/components/VCombobox'

/* @vue/component */
export default {
  name:"combobox2",
  extends: VCombobox,
  props: {
    preventCapture:Boolean
  },
  methods: {
    // Requires a manual definition
    // to overwrite removal in v-autocomplete
    onEnterDown (e) {
      if(!this.preventCapture) {
        VCombobox.methods.onEnterDown.call(this, e)
      }
    },
    onTabDown (e) {
      if(!this.preventCapture) {
        VCombobox.methods.onTabDown.call(this, e)
      }
    },
  },
}