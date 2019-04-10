/* eslint-disable */
<template>
</template>
<script>
import Vue from 'vue'
import VuePrism from 'vue-prism'
Vue.use(VuePrism);
import 'prismjs/components/prism-csharp'
import 'prismjs/components/prism-fsharp'

import 'prismjs/components/prism-python'
import 'prismjs/themes/prism-twilight.css'
</script>
<style>
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