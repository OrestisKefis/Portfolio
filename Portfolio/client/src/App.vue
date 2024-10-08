<template>
  <RouterView />
  <ToastMessage ref="toast"/>
  <Spinner ref="spinner"/>
</template>

<script setup lang="ts">
import { onMounted, provide, ref } from 'vue';

import ToastMessage from './components/ToastMessage.vue';
import Spinner from "@/components/Spinner.vue"

const toast = ref<Components.ToastMessage>(null)
const windowWidth = ref<number>(window.innerWidth)
const spinner = ref<Components.Spinner>(null)

onMounted(() => window.addEventListener('resize', () => windowWidth.value = window.innerWidth))

provide('toast', toast)
provide('windowWidth', windowWidth)
provide('spinner', spinner)
</script>
