import { getProjects } from "@/services/projectService";
import type { Project } from "@/types/project";
import { defineStore } from "pinia";
import { inject, ref, type Ref } from "vue";

export const useProjectStore = defineStore('project', () => {
    const spinner:Ref<Components.Spinner> = inject('spinner')
    const toast:Ref<Components.ToastMessage> = inject('toast')

    const defaultProject: Project = {
        id: null,
        name: '',
        dateCreated: '',
        languagesUsed: [],
        githubPath: '',
        imageUrl: '',
        description: ''
    }
    const currentProject = ref<Project>(defaultProject)
    const projects = ref<Project[]>([])

    const loadProjects = async () => {
        spinner.value.show()
        await getProjects()
        .then(r => {
            projects.value = r
        })
        .catch(err => toast.value.show( err, 'error', 4000))
        .finally(() => spinner.value.hide())
    }

    const clearCurrentProject = () => currentProject.value = defaultProject

    return { currentProject, projects, clearCurrentProject, loadProjects }
})