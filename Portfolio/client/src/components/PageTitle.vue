<template></template>

<script setup lang="ts">
type Props = {
    title: string
}
const props = defineProps<Props>()
document.title = props.title
</script>