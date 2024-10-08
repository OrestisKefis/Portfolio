<template>
    <button v-bind="$attrs" :disabled="isDisabled" class="rounded px-3 py-2" :class="{
        'bg-transparent br-prim text-prim': !isDisabled,
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
    background-color: #42b883 !important;
    color: rgb(33, 37, 41, 1)!important;
}
</style>