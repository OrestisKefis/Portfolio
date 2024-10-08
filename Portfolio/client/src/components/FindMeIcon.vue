<template>
    <a v-if="element" :href="element.href" class="fa-brands text-decoration-none p-2 rounded text-white link"
        :class="element.class" target="_blank" title="Github profile"></a>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';

interface Element {
    href: string,
    class: string
}

const props = defineProps<{ social: Types.Social }>()

const element = ref<Element>(null)

onMounted(() => {
    if (props.social == 'Facebook') {
        element.value = {
            href: "https://google.com",
            class: "fa fa-facebook-f"
        }
    } else if (props.social == 'Instagram') {
        element.value = {
            href: "https://google.com",
            class: "fa fa-instagram"
        }
    } else if (props.social == 'LinkedIn') {
        element.value = {
            href: "https://google.com",
            class: "fa fa-linkedin-in"
        }
    } else if (props.social == 'Github') {
        element.value = {
            href: "https://github.com/OrestisKefis",
            class: "fa fa-github"
        }
    }
})
</script>

<style scoped>
.link {
    border: 1px solid transparent;
}

.link:hover {
    border: 1px solid #42b883;
    color: #42b883 !important;
}
</style>