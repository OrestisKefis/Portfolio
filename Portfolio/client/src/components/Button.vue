<template>
    <button v-bind="$attrs" :disabled="isDisabled" class="rounded px-3 py-2 text-white" :class="{
        'bg-prim br-prim': !isDisabled,
        'btn-disabled': isDisabled
    }">
        <slot />
    </button>
</template>

<script setup lang="ts">
withDefaults(defineProps<{ isDisabled?: boolean }>(), { isDisabled: false })
</script>

<style scoped>
button:hover:not(.btn-disabled) {
    background-color: #3b966d !important;
    border-color: #3b966d!important;
}
</style>