<template>
    <div class="ff-sans-serif layout">
        <Navbar />
        <RouterView />
        <Footer class="position-fixed bottom-0" />
    </div>
</template>

<script setup lang="ts">
import Navbar from '@/components/Navbar.vue';
import Footer from '@/components/Footer.vue';
</script>

<style scoped>
.layout {
    min-height: 100vh;
    position: relative;
}

.layout::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: url("../assets/images/pen_laptop.jpg");
    background-size: cover;
    background-repeat: no-repeat;
    background-attachment: fixed;
    z-index: -1;
    opacity: 0.3;
}

.layout::after {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: black;
    z-index: -2;
    opacity: 0.9;
}
</style>