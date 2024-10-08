<template>
    <Modal ref="modal" :title="projectStore.currentProject.id ? 'Edit' : 'Add'" :maxWidth="'28rem'"
        @onShow="project = { ...projectStore.currentProject }" @onHide="formStore.hasErrors = false">
        <div class="p-3">
            <form class="d-flex flex-column gap-4">
                <FormInput id="inpt-name" type="text" :labelText="'Name'" v-model="project.name"
                    :validators="nameValidators" />

                <div class="d-flex justify-content-between">
                    <label>Languages: </label>
                    <div class="w-75 d-flex flex-column gap-2 overflow-y-auto language-container">
                        <div class="d-flex gap-2 flex-wrap">
                            <Languages :languagesUsed="project.languagesUsed" @toggled="onToggled" />
                        </div>
                    </div>
                </div>

                <FormInput id="inpt-dateCreated" type="date" :labelText="'Date Created'" v-model="project.dateCreated"
                    :validators="dateCreatedValidators" />
                <FormInput id="inpt-gitPath" type="text" :labelText="'Git Path'" v-model="project.githubPath"
                    :validators="gitPathValidators" />
                <FormInput id="impt-imgUrl" type="text" :labelText="'Image URL'" v-model="project.imageUrl"
                    :validators="imgUrlValidators" />

                <div class="d-flex flex-column">
                    <label for="inpt-description">Description</label>
                    <textarea id="inpt-description" class="form-control" rows="5" v-model="project.description"
                        style="resize: none;"></textarea>
                </div>

                <div class="d-flex justify-content-center gap-3">
                    <div>
                        <OutlineButton @click.prevent="modal.hide">
                            Cancel
                        </OutlineButton>
                    </div>

                    <div>
                        <Button @click.prevent="onSubmitForm" class="yolo">
                            Confirm
                        </Button>
                    </div>
                </div>
            </form>
        </div>
    </Modal>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { type AddProjectRequest, type Project, type ProjectLanguage, type UpdateProjectRequest } from '@/types/project';
import { DateString, dateFormat } from '@/services/dateService';
import { useProjectStore } from '@/stores/projectStore';
import { addProject, updateProject } from '@/services/adminService';
import type { Validators } from '@/types/formInput';
import { useFormStore } from '@/stores/formStore';

import FormInput from '../FormInput.vue';
import Modal from '../Modal.vue';
import Languages from './Languages.vue';
import Button from '../Button.vue';
import OutlineButton from '../OutlineButton.vue';

const nameValidators: Validators[] = ['notEmpty', 'isOnlyAlphabetical']
const dateCreatedValidators: Validators[] = ['notEmpty']
const gitPathValidators: Validators[] = ['notEmpty']
const imgUrlValidators: Validators[] = ['notEmpty']

const emits = defineEmits(['updated', 'created'])

const projectStore = useProjectStore()
const formStore = useFormStore()

const modal = ref<Components.Modal>()

const project = ref<Project>(projectStore.currentProject)

const show = () => modal.value.show()

const hide = () => modal.value.hide()

const createProject = async () => {
    const request: AddProjectRequest = {
        name: project.value.name,
        languages: project.value.languagesUsed,
        dateCreated: DateString.create(dateFormat.yyyyMMdd, new Date(project.value.dateCreated)),
        githubPath: project.value.githubPath,
        imageurl: project.value.imageUrl,
        description: project.value.description
    }
    const response = await addProject(request)
    response && emits('created', response)
}

const editProject = async () => {
    const request: UpdateProjectRequest = {
        name: project.value.name,
        languages: project.value.languagesUsed,
        dateCreated: DateString.create(dateFormat.yyyyMMdd, new Date(project.value.dateCreated)),
        githubPath: project.value.githubPath,
        imageurl: project.value.imageUrl,
        description: project.value.description
    }
    const response = await updateProject(project.value.id, request)
    response && emits('updated', response)
}

const onToggled = (langs: ProjectLanguage[]) => project.value.languagesUsed = langs.map(l => l.value)

const onSubmitForm = async (event: Event) => {
    formStore.hasErrors = formStore.formInputs.some(i => i.errors.length > 0)
    if (formStore.hasErrors) {
        event.preventDefault()
        return
    }
    projectStore.currentProject.id ? await editProject() : await createProject()
}

defineExpose({
    show,
    hide
})
</script>

<style scoped>
.language-container{
    min-width: 7rem;
}
</style>