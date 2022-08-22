import { createApp } from 'vue'
import App from './App.vue'
import { loadFonts } from './plugins/webfontloader'
import 'vuetify/styles' // Global CSS has to be imported
import { createVuetify } from 'vuetify'
import router from './router'
import axios from 'axios'
import VueAxios from 'vue-axios'
import { aliases, mdi } from 'vuetify/iconsets/mdi'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
// import money from 'v-money3'

const vuetify = createVuetify({
  components,
  directives,
  theme: {
    defaultTheme: 'dark'
  },
  icons: {
    defaultSet: 'mdi',
    aliases,
    sets: {
      mdi,
    }
  },
})

const app = createApp(App)
// app.use(money)
app.use(vuetify, VueAxios, axios)
app.use(router)

app.mount('#app')

loadFonts()


