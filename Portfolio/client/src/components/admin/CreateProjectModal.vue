<template>
    <Modal ref="formModal" :title="'Add'" :minWidth="'33rem'" @onHide="clearForm">
        <div class="p-3">
            <form class="d-flex flex-column gap-4">
                <FormInput id="inpt-name" type="text" :labelText="'Name: '" v-model="name" />

                <div class="d-flex justify-content-between">
                    <label>Languages: </label>
                    <div class="w-75 d-flex flex-column gap-2">
                        <div class="d-flex gap-3 flex-wrap">
                            <Languages :languagesUsed="languages" @toggled="onToggled" />
                        </div>
                    </div>
                </div>

                <FormInput id="inpt-dateCreated" type="date" :labelText="'Date Created: '" v-model="date" />
                <FormInput id="inpt-gitPath" type="text" :labelText="'Git Path: '" v-model="gitPath" />
                <FormInput id="impt-imgUrl" type="text" :labelText="'Image URL: '" v-model="imgUrl" />

                <div class="d-flex flex-column">
                    <label for="inpt-description">Description:</label>
                    <textarea id="inpt-description" class="form-control" rows="5" v-model="desc"
                        style="resize: none;"></textarea>
                </div>

                <div class="d-flex justify-content-center gap-3">
                    <div>
                        <button @click.prevent="emits('cancel')">
                            Cancel
                        </button>
                    </div>

                    <div>
                        <input type="submit" value="Confirm" @click.prevent="onSubmitForm">
                    </div>
                </div>
            </form>
        </div>
    </Modal>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { type AddProjectRequest, type Language, type Project, type ProjectLanguage } from '@/types/project';
import { DateString, dateFormat } from '@/services/dateService';
import { addProject } from '@/services/adminService';

import FormInput from '../FormInput.vue';
import Modal from '../Modal.vue';
import Languages from './Languages.vue';

const emits = defineEmits(['cancel', 'created'])

const formModal = ref<Components.Modal>()

const name = ref<string>('')
const date = ref<string>('')
const languages = ref<Language[]>([])
const gitPath = ref<string>('')
const imgUrl = ref<string>('')
const desc = ref<string>('')

const show = () => formModal.value.show()

const hide = () => formModal.value.hide()

const onCreateProject = async () => {
    const request: AddProjectRequest = {
        name: name.value,
        languages: languages.value,
        dateCreated: DateString.create(dateFormat.yyyyMMdd, new Date(date.value)),
        githubPath: gitPath.value,
        imageurl: imgUrl.value,
        description: desc.value
    }
    console.log(languages.value)
    // const response = await addProject(request)
    // response && emits('created', response)
}

const onToggled = (language: ProjectLanguage) => {
    if (!languages.value.includes(language.value) && language.isActive) {
        languages.value.push(language.value)
    } else {
        languages.value = languages.value.filter(l => l != language.value)
    }
}

const onSubmitForm = async () => await onCreateProject()

const clearForm = () => {
    name.value = ''
    date.value = ''
    gitPath.value = ''
    imgUrl.value = ''
    desc.value = ''
    languages.value = []
}

defineExpose({
    show,
    hide
})
</script>