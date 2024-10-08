<template>
    <div class="d-flex flex-column text-center w-100">
        <div class="fs-5 border-bottom border-1">
            {{ name }}
        </div>

        <div class="mt-2" v-bind="$attrs">
            <slot />
        </div>
    </div>
</template>

<script setup lang="ts">
defineProps<{ name: string }>()
</script>