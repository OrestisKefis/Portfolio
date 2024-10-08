<template>
    <label role="button" v-for="language in languages" @click="onLanguageToggle(language)"
        class="br-prim text-prim rounded-5 px-2 py-1 fs-7" :class="{
            'bg-prim text-white': language.isActive
        }">
        {{ language.key }}
    </label>
</template>

<script setup lang="ts">
import { getAllLanguages, type Language, type ProjectLanguage } from '@/types/project';
import { computed, onMounted, ref } from 'vue';

const props = defineProps<{ languagesUsed: Language[] }>()

const emits = defineEmits(['toggled'])

const languages = ref<ProjectLanguage[]>([])
const pushedLanguages = computed(() => languages.value.filter(l => l.isActive))

const onLanguageToggle = (language: ProjectLanguage) => {
    language.isActive = !language.isActive
    emits('toggled', pushedLanguages.value)
}

onMounted(() => {
    languages.value = getAllLanguages()
    props.languagesUsed.forEach(lang => languages.value
        .filter(l => l.value == lang)
        .forEach(l => l.isActive = true)
    )
    emits('toggled', pushedLanguages.value)
})
</script>