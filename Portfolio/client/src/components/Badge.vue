<template>
    <div class="skill-badge text-white d-flex flex-column justify-content-center align-items-center rounded-2">
        {{ name }}
    </div>
</template>

<script setup lang="ts">
const props = defineProps<{
    name: string
}>()
</script>

<style scoped>
.skill-badge {
    width: 8rem;
    height: 8rem;
    background-color: brown;
}
</style>