<template>
    <PageTitle :title="'Admin Tool'" />

    <div class="bg-dark layout">
        <Navbar @logout="onLogout" />
        <RouterView />
    </div>
</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores/authStore';
import { watchEffect } from 'vue';
import { useRouter } from 'vue-router';

import PageTitle from '@/components/PageTitle.vue';
import Navbar from '@/components/admin/Navbar.vue';

const auStore = useAuthStore()
const router = useRouter()

const onLogout = async () => await auStore.logoutService()

watchEffect(() => !auStore.user.isAuthenticated && router.push("/admin/login"))
</script>

<style scoped>
.layout {
    min-height: 100vh;
    position: relative!important;
    /* background: url("../assets/images/dots.jpg");
    background-size: cover;
    background-repeat: no-repeat;
    background-attachment: scroll; */
}
</style>