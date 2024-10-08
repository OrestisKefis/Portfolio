<template>
    <PageTitle :title="'Projects'"></PageTitle>

    <div class="container d-flex flex-column justify-content-center align-items-center gap-3 mt-5">
        <ProjectFilters v-if="projectStore.projects" :is-desktop="isWindowWidthDesktop" />

        <div v-if="pagination" class="d-grid gap-4 mt-3 card-container">
            <div v-for="project in (pagination.page as ProjectCard[])"
                class="mb-4 rounded position-relative shadow cover-image project-card"
                :style="{ background: `url(${(`src/assets/images/${project.imageUrl}`)})` }"
                @mouseover="(event: MouseEvent) => onMouseAction(event, project)"
                @mouseleave="(event: MouseEvent) => onMouseAction(event, project)">
                <div class="position-absolute text-white d-flex flex-column gap-2 justify-content-center align-items-center rounded-top overflow-hidden label"
                    :class="{
                        'default-project-label': !project.active,
                        'active-project-label': project.active
                    }">
                    <div :class="{
                        'd-flex gap-2': !isWindowWidthDesktop
                    }">
                        {{ project.name }}
                        <div v-if="!isWindowWidthDesktop">
                            <button class="fa-solid fa-circle-info bg-prim border-0 rounded p-1 text-light"
                                @click.prevent="onDetails(project)" title="Details"></button>
                        </div>
                    </div>

                    <div class="d-flex gap-2" :class="{
                        'd-none': !project.active
                    }">
                        <div>
                            <button class="fa-solid fa-circle-info bg-prim border-0 rounded p-1 text-light"
                                @click.prevent="onDetails(project)" title="Details"></button>
                        </div>

                        <div>
                            <a class="fa-brands fa fa-github border-0 bg-black text-light rounded p-1 shadow"
                                :href="project.githubPath" title="Github" target="_blank"></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="d-flex gap-5 justify-content-center">
            <div>
                <OutlineButton :isDisabled="pagination?.hasPreviousPage" @click="pagination?.previous">
                    <i class="fa fa-arrow-left"></i>
                </OutlineButton>
            </div>

            <div class="btn-pagination btn-pagination-right">
                <OutlineButton :isDisabled="pagination?.hasNextPage" @click="pagination?.next">
                    <i class="fa fa-arrow-right"></i>
                </OutlineButton>
            </div>
        </div>

        <Pagination v-if="projectStore.projects.length > 0" :items="projectStore.projects"
            :pageSize="6" ref="pagination" />
    </div>

    <ProjectDetails :project="currentProject || projectStore.currentProject" @close="currentProject = null" />
</template>

<script setup lang="ts">
import { inject, onMounted, ref, watch, type Ref } from 'vue';
import { type Project } from '@/types/project';
import { useProjectStore } from '@/stores/projectStore';

import Pagination from '@/components/Pagination.vue';
import OutlineButton from '@/components/OutlineButton.vue';
import ProjectDetails from '@/components/ProjectDetails.vue';
import PageTitle from '@/components/PageTitle.vue';
import Button from '@/components/Button.vue';
import ProjectFilters from '@/components/projects/ProjectFilters.vue';

const windowWidth: Ref<number> = inject('windowWidth')

interface ProjectCard extends Project { active: boolean }

const pagination = ref<Components.Pagination<Project>>();

const projectStore = useProjectStore()
const currentProject = ref<ProjectCard>(null)

const onDetails = (project: ProjectCard) => currentProject.value = project

const isWindowWidthDesktop = ref<boolean>(windowWidth.value > 1900)

const onMouseAction = (event: MouseEvent, project: ProjectCard) => {
    isWindowWidthDesktop.value && (project.active = (event.type == "mouseover"))
}

watch(windowWidth, () => isWindowWidthDesktop.value = windowWidth.value > 1900)

onMounted(async () => await projectStore.loadProjects())
</script>

<style scoped>
.default-project-label {
    transition: 0.6s;
    background: rgb(1, 1, 1);
    min-height: 20px;
}

@media screen and (max-width: 4000px) and (min-width: 1919px) {
    .card-container {
        grid-template-columns: repeat(4, 1fr);
    }

    .project-card:hover {
        transition: 0.2s;
        position: relative;
        transform: scale(1.1, 1.1);
    }

    .active-project-label {
        transition: 0.6s;
        background: rgb(1, 1, 1, 0.8);
        min-height: 100%;
    }
}

@media screen and (max-width: 1920px) and (min-width: 1720px) {
    .card-container {
        grid-template-columns: repeat(3, 1fr);
    }
}

@media screen and (max-width: 1719px) and (min-width: 1200px) {
    .card-container {
        grid-template-columns: repeat(2, 1fr);
    }
}

@media screen and (max-width: 1199px) {
    .card-container {
        grid-template-columns: repeat(1, 1fr);
    }
}

.project-card {
    transition: 0.2s;
    width: 400px;
    height: 300px;
    background-position: center !important;
}

.label {
    width: 100%;
    left: 0;
    top: 0;
}
</style>