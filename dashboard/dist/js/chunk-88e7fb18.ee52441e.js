(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-88e7fb18"],{2009:function(e,t,a){"use strict";var r=a("44ef"),s=a.n(r);s.a},"27c9":function(e,t,a){"use strict";a.r(t);var r=a("f56f"),s=a.n(r);for(var i in r)"default"!==i&&function(e){a.d(t,e,function(){return r[e]})}(i);t["default"]=s.a},"44ef":function(e,t,a){},6299:function(e,t,a){"use strict";var r=function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("v-container",{attrs:{"fill-height":"",fluid:"","grid-list-xl":""}},[a("v-dialog",{attrs:{"max-width":"660",scrollable:""},model:{value:e.showingEmailDialog,callback:function(t){e.showingEmailDialog=t},expression:"showingEmailDialog"}},[a("v-card",{staticStyle:{overflow:"hidden",height:"auto"}},[a("v-list",[a("v-list-tile",[a("v-flex",{attrs:{xs2:""}},[e._v("\n                      To:\n                  ")]),a("v-flex",{attrs:{xs9:""}},[a("v-chip",[e._v("bill@gates.com")])],1)],1),a("v-divider"),a("v-list-tile",[a("v-flex",{attrs:{xs2:""}},[e._v("\n                      From:\n                  ")]),a("v-flex",{attrs:{xs9:""}},[a("v-chip",[e._v("help@mysaas.com")])],1)],1),a("v-divider"),a("v-list-tile",[a("v-flex",{attrs:{xs2:""}},[e._v("\n                      Subject:\n                  ")]),a("v-flex",{attrs:{xs9:""}},[a("v-chip",[e._v("Welcome to MySaaS!")])],1)],1),a("v-divider")],1),a("v-card",{staticClass:"elevation-0",staticStyle:{"border-radius":"0",margin:"15px","border-left":"2px solid #9c27b0","padding-left":"15px"},attrs:{tile:""}},[a("h3",[e._v("Hi Bill!")]),a("p",[e._v("Thanks for signing up to MySaaS, the sassiest SaaS on the planet.")]),a("p",[e._v("For future reference, your username is bill@gates.com")]),a("p",[e._v("We're transforming the world through paradigm shifts in big data for  Enterprise SaaS on the Blockchain.")]),a("p",[e._v("We'll be in touch soon.")]),a("p",[e._v("The MySaaS Team.")]),a("p",[e._v("---")]),a("p",[e._v("MySaaS Inc.")]),a("p",[e._v("San Francisco.")])])],1)],1),a("v-layout",{attrs:{"justify-center":"",wrap:""}},[a("v-flex",{attrs:{md12:""}},[a("material-card",{attrs:{color:"green",title:"Logs"}},[a("v-data-table",{attrs:{headers:e.headers,items:e.items,"hide-actions":""},scopedSlots:e._u([{key:"headerCell",fn:function(t){var r=t.header;return[a("span",{staticClass:"subheading font-weight-light text-success text--darken-3",domProps:{textContent:e._s(r.text)}})]}},{key:"items",fn:function(t){var r=t.item;return[a("td",["ok"==r.status?a("v-icon",{attrs:{color:"green"}},[e._v("mdi-check")]):e._e(),"error"==r.status?a("v-icon",{attrs:{color:"red"}},[e._v("mdi-alert-circle")]):e._e()],1),a("td",[e._v(e._s(r.event))]),a("td",[e._v(e._s(r.parameters))]),a("td",{},[e._v(e._s(r.receivedOn))]),a("td",[r.email?a("v-icon",{staticStyle:{cursor:"pointer"},attrs:{slot:"activator"},on:{click:function(t){e.showingEmailDialog=!0}},slot:"activator"},[e._v("\n                      mdi-email\n                  ")]):e._e()],1)]}}])})],1)],1)],1)],1)},s=[];a.d(t,"a",function(){return r}),a.d(t,"b",function(){return s})},bbae:function(e,t,a){"use strict";a.r(t);var r=a("6299"),s=a("27c9");for(var i in s)"default"!==i&&function(e){a.d(t,e,function(){return s[e]})}(i);a("2009");var n=a("2877"),l=Object(n["a"])(s["default"],r["a"],r["b"],!1,null,null,null);t["default"]=l.exports},f56f:function(e,t,a){"use strict";var r=a("288e");Object.defineProperty(t,"__esModule",{value:!0}),t.default=void 0;r(a("2b0e"));var s={methods:{},components:{},data:function(){return{content:"",showingEmailDialog:!1,headers:[{sortable:!0,text:"Status",value:"status"},{sortable:!0,text:"Event",value:"event"},{sortable:!0,text:"Parameters",value:"parameters"},{sortable:!0,text:"Received On",value:"received"},{sortable:!1,text:"E-mails",value:"code"}],items:[{event:"signup",receivedOn:"2019-01-17 04:35",status:"ok",parameters:{username:"bill@gates.com",firstName:"Bill",lastName:"Gates"},email:!0},{event:"signup",receivedOn:"2019-01-17 05:15",status:"ok",parameters:{username:"melinda@gates.com",firstName:"Melinda",lastName:"Gates"},email:!0},{event:"login",receivedOn:"2019-01-17 14:50",status:"error",parameters:{username:"steve@ballmer.com",firstName:"Steve",lastName:"Ballmer"},email:!0},{event:"clickHelp",receivedOn:"2019-01-18 00:05",status:"error",parameters:{username:"steve@ballmer.com",firstName:"Steve",lastName:"Ballmer"},email:!0}],tabs:0}}};t.default=s,e.exports=t.default}}]);
//# sourceMappingURL=chunk-88e7fb18.ee52441e.js.map