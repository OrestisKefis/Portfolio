<template>
    <div class="position-fixed top-0 end-0 vh-100 bg-dark shadow overflow-x-hidden max-w-100 details-card" :class="{
        'details-card-active': project.name != '',
        'details-card-hidden': project.name == ''
    }">

        <div>
            <button class="fa fa-xmark float-end bg-transparent text-danger border-0 p-3"
                @click.prevent="emits('close')"></button>
        </div>

        <div class="d-flex flex-column align-items-center m-auto gap-4 mt-5 text-light content">
            <div class="fs-4">
                {{ project.name }}
            </div>

            <div class="d-flex flex-column bg-black">
                <div class="cover-image shadow details-image"
                    :style="{ background: `url(${(`src/assets/images/${project.imageUrl}`)})` }">

                </div>
            </div>

            <ProjectDetail :name="'Created'">
                {{ DateString.create(dateFormat.ddMMyyyy, new Date(project.dateCreated)) }}
            </ProjectDetail>

            <ProjectDetail :name="'Description'">
                {{ project.description }}
            </ProjectDetail>

            <ProjectDetail :name="'Technologies'" class="d-flex flex-wrap gap-3">
                <div v-for="language in project.languagesUsed" class="br-prim text-prim text-center rounded-5 px-2 py-1 fs-7">
                    {{ languageDescription(language) }}
                </div>
            </ProjectDetail>

            <ProjectDetail :name="'Github'">
                <a target="_blank" :href="project.githubPath">
                    {{ project.githubPath }}
                </a>
            </ProjectDetail>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { Project } from '@/types/project';
import { languageDescription } from '@/types/project';
import { DateString } from '@/services/dateService';
import { dateFormat } from '@/services/dateService';

import ProjectDetail from '@/components/ProjectDetail.vue';

const emits = defineEmits(['close'])

withDefaults(defineProps<{ project: Project }>(), { project: null })
</script>

<style scoped>
.details-card-active {
    transition: 0.3s;    
    width: 500px;
}

.details-card-hidden {
    transition: 0.3s;
    width: 0px;
    height: 0px;
}

.details-card {
    z-index: 10;
}

.details-image {
    width: 23rem;
    height: 230px;
    background-position: center!important;
}

.content {
    max-width: 45ch;
}
</style>