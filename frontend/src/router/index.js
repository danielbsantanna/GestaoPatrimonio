import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../components/Home.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/login',
      name: 'Login',
      component: () => import('../components/Login.vue'),
    },
    {
      path: '/usuario',
      name: 'Usuarios',
      component: () => import('../components/Usuarios.vue'),
    },
    {
      path: '/usuario/:id',
      name: 'UsuarioCadastrar',
      component: () => import('../components/UsuarioCadastrar.vue'),
    },
    {
      path: '/grupo',
      name: 'Grupos',
      component: () => import('../components/Grupos.vue'),
    },
    {
      path: '/grupo/:id',
      name: 'GrupoCadastrar',
      component: () => import('../components/GrupoCadastrar.vue'),
    }, {
      path: '/nota',
      name: 'Notas',
      component: () => import('../components/Notas.vue'),
    },
    {
      path: '/nota/:id',
      name: 'NotaCadastrar',
      component: () => import('../components/NotaCadastrar.vue'),
    }
  ]
})

export default router
