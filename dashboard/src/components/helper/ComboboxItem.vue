<template>
  <v-chip
      :color="!valid ? 'red' : ''"
      :class="!valid ? 'white--text' : ''"
    >
        {{ item }}
    <v-icon
        :class="!valid ? 'white--text' : ''"
        small
        @click="$emit('remove')"
        >mdi-close</v-icon>
    </v-chip>
</template>
<script>
export default {
  props:{
    rules:Array,
    item:String
  },
  data:() => ({
      valid:true
  }),
  mounted() {
    var vm = this;
    Object.values(this.rules).forEach((rule) => {
      var valid = rule(this.item);
      if(valid !== true) {
        console.error(valid);
        vm.valid = false;
      }
    });
  }
}
</script>