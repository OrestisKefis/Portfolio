<template>
    <div class="container mt-5">
        <div class="d-flex justify-content-between align-items-center mb-5">
            <div class="fw-bold fs-4 text-light">
                Project Manager
            </div>

            <div>
                <OutlineButton class="fa fa-plus" @click.prevent="onCreate" title="Add new project" />
            </div>
        </div>

        <ProjectsTable :projects="projects" @onProjectEdit="onUpdate" @onProjectDelete="onDelete" />

        <EditProjectModal @created="created" @updated="updated" ref="formModal" />

        <DeleteProjectModal @deleted="deleted" ref="deleteModal" />
    </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { type Project } from '@/types/project';
import { useProjectStore } from '@/stores/projectStore';

import ProjectsTable from "@/components/admin/ProjectsTable.vue"
import EditProjectModal from '@/components/admin/EditProjectModal.vue';
import DeleteProjectModal from '@/components/admin/DeleteProjectModal.vue';
import OutlineButton from '@/components/OutlineButton.vue';

const store = useProjectStore()

const projects = ref<Project[]>([])

const formModal = ref<Components.Modal>()
const deleteModal = ref<Components.Modal>()

const onCreate = () => {
    store.clearCurrentProject()
    formModal.value.show()
}

const created = (repsonse: Project) => {
    projects.value.push(repsonse)
    formModal.value.hide()
}

const onUpdate = (project: Project) => {
    store.currentProject = project
    formModal.value?.show()
}

const updated = (repsonse: Project) => {
    store.currentProject.name = repsonse.name
    store.currentProject.dateCreated = repsonse.dateCreated
    store.currentProject.languagesUsed = repsonse.languagesUsed
    store.currentProject.githubPath = repsonse.githubPath
    store.currentProject.imageUrl = repsonse.imageUrl
    store.currentProject.description = repsonse.description
    formModal.value.hide()
}

const onDelete = (project: Project) => {
    store.currentProject = project
    deleteModal.value.show()
}

const deleted = () => {
    projects.value = projects.value.filter(p => p.id != store.currentProject.id)
    store.clearCurrentProject()
    deleteModal.value.hide()
}

onMounted(async () => {
    await store.loadProjects()
    projects.value = [...store.projects]    
})
</script>