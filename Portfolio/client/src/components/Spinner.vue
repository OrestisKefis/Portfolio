<template>
    <div v-if="isActive" class="position-absolute z-2 w-100 h-100 top-0 d-flex justify-content-center align-items-center spinner-container">
        <div class="spinner">
            <svg width="100" height="100">
                <circle cx="50" cy="50" r="45" stroke-width="10" stroke="#eee" fill="none"></circle>

                <path d="M50 5 A 45 45 0 0 1 95 50" fill="none" stroke-width="10" stroke="#42b883"></path>
            </svg>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from "vue"

const isActive = ref<boolean>(false)

const show = () => isActive.value = true

const hide = () => isActive.value = false

defineExpose({
    show,
    hide
})
</script>

<style scoped>
.spinner-container {
    background: rgb(1, 1, 1, 0.3);
}

.spinner {
    animation: spinning linear 1s infinite;
    width: 100px;
    height: 100px;
}

@keyframes spinning {
    0% {
        transform: rotate(0deg);
    }

    25% {
        transform: rotate(90deg);
    }

    50% {
        transform: rotate(180deg);
    }

    75% {
        transform: rotate(270deg);
    }

    100% {
        transform: rotate(360deg);
    }
}
</style>