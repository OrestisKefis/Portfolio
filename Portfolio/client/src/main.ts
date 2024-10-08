import './assets/main.scss'
import 'bootstrap/dist/css/bootstrap.min.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { useAuthStore } from './stores/authStore'

import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(createPinia())
const authStore = useAuthStore()
await authStore.checkClaimsPresence()

app.use(router)

app.mount('#app')