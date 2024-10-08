<template>
    <div class="w-100 d-flex justify-content-between navbar p-2">
        <div class="m-auto">
            <FindMe />
        </div>

        <div class="d-flex" :class="{
            'justify-content-end gap-4': isDesktop,
            'flex-column position-absolute box bg-dark': !isDesktop,
            'd-none': !isDesktop && isBoxHidden
        }">
            <RouterLink to="/" class="text-decoration-none d-flex align-items-center px-4 navbar-item">
                <div class="d-flex gap-2 align-items-center fs-6">
                    <i class="fa fa-home fs-6"></i>
                    <span>Home</span>
                </div>
            </RouterLink>

            <RouterLink to="/projects" class="text-decoration-none d-flex align-items-center px-4 navbar-item">
                <div class="d-flex gap-2 align-items-center fs-6">
                    <i class="fa-solid fa-diagram-project icon"></i>
                    <span>Projects</span>
                </div>
            </RouterLink>

            <RouterLink to="/experience" class="text-decoration-none d-flex align-items-center px-4 navbar-item">
                <div class="d-flex gap-2 align-items-center fs-6">
                    <i class="fa-solid fa-microchip icon"></i>
                    <span>Experience</span>
                </div>
            </RouterLink>

            <RouterLink to="/about" class="text-decoration-none d-flex align-items-center px-4 navbar-item">
                <div class="d-flex gap-2 align-items-center fs-6">
                    <i class="fa fa-circle-info icon"></i>
                    <span>About</span>
                </div>
            </RouterLink>

            <RouterLink to="/contact" class="text-decoration-none d-flex align-items-center px-4 navbar-item">
                <div class="d-flex gap-2 align-items-center fs-6">
                    <i class="fa fa-phone icon"></i>
                    <span>Contact</span>
                </div>
            </RouterLink>
        </div>

        <div v-if="!isDesktop" class="float-end" style="margin-right: 3rem;">
            <button class="fa fa-bars bg-transparent border-0 text-white fs-2" @click.prevent="toggleBox"></button>
        </div>
    </div>
</template>

<script setup lang="ts">
import { onMounted, onUnmounted, ref, watch } from 'vue';
import { useRoute } from 'vue-router';

import FindMe from './FindMe.vue';

const route = useRoute()

const isDesktop = ref<boolean>(window.innerWidth >= 930)
const isBoxHidden = ref<Boolean>(true)

const toggleBox = () => isBoxHidden.value = !isBoxHidden.value

const someFunctionICantNameRightNow = () => {
    isDesktop.value = window.innerWidth >= 930;
    (isDesktop.value && !isBoxHidden.value) && toggleBox()
}

onMounted(() => window.addEventListener('resize', someFunctionICantNameRightNow))

onUnmounted(() => window.removeEventListener('resize', someFunctionICantNameRightNow))

watch(route, () => (!isDesktop.value && !isBoxHidden.value) && toggleBox())
</script>

<style scoped>
.navbar {
    top: 0;
    left: 0;
    z-index: 10;
}

.box {
    top: 2.9rem;
    right: 0;
    z-index: 10;
}

.navbar-item {
    color: #fff;
    border: 2px solid transparent;
    font-size: 12px;
    height: 3rem;
}

.navbar-item:hover,
.router-link-exact-active {
    border-radius: 30px;
    border: 2px solid #42b883;
    color: #42b883;
}

.icon {
    font-size: 12px;
}
</style>