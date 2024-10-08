<template>
    <table v-if="projects.length > 0" class="table shadow">
        <thead>
            <tr>
                <th class="bg-tert py-4 text-light">Name</th>
                <th class="bg-tert py-4 text-light">Languages</th>
                <th class="bg-tert py-4 text-light">Date Created</th>
                <th class="bg-tert py-4 text-light"></th>
            </tr>
        </thead>

        <tbody>
            <tr v-if="pagination" v-for="project in pagination?.page">
                <td class="align-middle bg-dark text-light">{{ project.name }}</td>
                <td class="align-middle bg-dark text-light">
                    <div class="d-flex gap-2 flex-wrap">
                        <div v-for="language in project.languagesUsed" class="rounded-5 br-prim text-prim fs-7 px-2">
                            {{ languageDescription(language) }}
                        </div>
                    </div>
                </td>
                <td class="align-middle bg-dark text-light">{{ project.dateCreated }}</td>
                <td class="align-middle bg-dark">
                    <div class="d-flex gap-3 align-items-center ">
                        <div>
                            <a class="fa-brands fa fa-github text-light" :href="project.githubPath"
                                :title="project.githubPath" target="_blank"></a>
                        </div>

                        <div>
                            <button class="fa fa-edit border-0 bg-transparent text-light"
                                @click="emits('onProjectEdit', project)">
                            </button>
                        </div>

                        <div>
                            <button class="fa fa-trash border-0 bg-transparent text-danger"
                                @click="emits('onProjectDelete', project)">
                            </button>
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>

    <Pagination v-if="projects.length > 0" :items="projects" :pageSize="8" ref="pagination" />

    <div class="d-flex gap-4 float-end">
        <div>
            <OutlineButton class="fa fa-arrow-left" :isDisabled="pagination?.hasPreviousPage"
                @click="pagination?.previous" />
        </div>

        <div>
            <OutlineButton class="fa fa-arrow-right" :isDisabled="pagination?.hasNextPage" @click="pagination?.next" />
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { type Project, languageDescription } from "@/types/project"

import Pagination from "@/components/Pagination.vue"
import OutlineButton from "../OutlineButton.vue";

defineProps<{ projects: Project[] }>()

const emits = defineEmits(['onProjectEdit', 'onProjectDelete'])

const pagination = ref<Components.Pagination<Project>>()
</script>