<template>
    <div class="d-flex flex-column text-center gap-3 text-light">
        {{ label }}
        <div v-bind="$attrs" class="text-prim br-prim fa-solid fs-1 p-2 rounded d-flex justify-content-center align-items-center quality"
            :class="icon">
            <slot/>
        </div>
    </div>
</template>

<script setup lang="ts">
defineProps<{
    label: string,
    icon: string
}>()
</script>

<style scoped>
.quality {
    width: 6rem;
    height: 6rem;
    background: rgb(33, 37, 41, 0.8);
    transition: 0.2s;
}

.quality:hover {
    transition: 0.2s;
    transform: scale(1.3, 1.3);
}
</style>