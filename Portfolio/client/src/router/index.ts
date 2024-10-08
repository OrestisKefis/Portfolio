import { createRouter, createWebHistory } from 'vue-router'
import GuestLayout from '@/layouts/GuestLayout.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Guest',
      component: GuestLayout,
      children: [
        {
          path: '/',
          name: 'Welcome',
          component: () => import('@/views/WelcomeView.vue')
        },

        {
          path: '/projects',
          name: 'Projects',
          component: () => import('@/views/ProjectsView.vue')
        },

        {
          path: '/about',
          name: 'About',
          component: () => import('@/views/AboutView.vue')
        },

        {
          path: '/contact',
          name: 'Contact',
          component: () => import('@/views/ContactView.vue')
        },

        {
          path: '/experience',
          name: 'Experience',
          component: () => import('@/views/SkillsView.vue')
        },
        
      ]
    },

    {
      path: '/admin',
      name: "Admin",
      children: [
        { path: "login", name: "Login", component: () => import("@/views/admin/LoginView.vue") },
        {
          path: "",
          name: "AdminLayout",
          children: [
            { path: "", name: "ProjectManager", component: () => import("@/views/admin/ProjectManagerView.vue") },
            { path: "users", name: "Users", component: () => import("@/views/admin/UsersView.vue") },
          ],
          component: () => import("@/layouts/AdminLayout.vue"),
        },
      ],
      component: () => import("@/layouts/EmptyLayout.vue")
    }
  ]
})

export default router