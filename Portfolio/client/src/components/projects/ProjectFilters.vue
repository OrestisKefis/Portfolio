<template>
    <div class="d-flex gap-3 flex-wrap w-100" :class="{
        'justify-content-start': isDesktop,
        'justify-content-center': !isDesktop
    }">
        <div class="d-flex flex-wrap align-items-center gap-2">
            <Selection ref="sortSelect" :options="ref(sortOptions)" :expanded="isSorting" @option-click="sort">
                <button class="fs-5 bg-transparent br-prim text-prim rounded btn-filter" title="Sort by"
                    @click.prevent="onSort">
                    <i class="fa-solid fa-sort"></i>
                </button>
            </Selection>

            <Selection ref="filterSelect" :options="ref(filterOptions)" :expanded="isFiltering" :allow-multiple="true">
                <button class="fs-5 bg-prim border-0 text-light rounded btn-filter" title="Filters"
                    @click.prevent="onFilter">
                    <i class="fa-solid fa-filter"></i>
                </button>
            </Selection>

            <div>
                <input class="form-control text-light input-filters inpt-search" type="text"
                    placeholder="Search by project name" v-model="searchValue">
            </div>
        </div>

        <Button @click="onSearch" class="fa fa-search">
            <!-- <i class="fa fa-search"></i> -->
        </Button>

        <Button @click="onClear">
            Clear all filters
        </Button>
    </div>
</template>

<script setup lang="ts">
import { inject, onMounted, ref, type Ref } from 'vue';
import { useProjectStore } from '@/stores/projectStore';
import { language, languageDescription, type Language } from '@/types/project';
import { getFilteredProjects } from '@/services/projectService';

import Button from '../Button.vue';
import Selection from '../Selection.vue';

defineProps<{ isDesktop: boolean }>()

const projectStore = useProjectStore()

const sortSelect = ref<Components.Select<ProjectSorting>>()
const filterSelect = ref<Components.Select<ProjectFiltering>>()
const searchValue = ref<string>('')

const isSorting = ref<boolean>(false)
const isFiltering = ref<boolean>(false)

const hasSorting = ref<boolean>(false)

const spinner:Ref<Components.Spinner> = inject('spinner')

type ProjectSorting = 'Date created' | 'Name' | 'Tech length'
const sortOptions: Components.SelectOption<ProjectSorting>[] = [
    {
        key: 'Date created b-a',
        option: 'Date created(newest to oldest)',
        selected: false
    },
    {
        key: 'Date created a-b',
        option: 'Date created(oldest to newest)',
        selected: false
    },
    {
        key: 'Tech length b-a',
        option: 'Technologies used(more to less)',
        selected: false
    },
    {
        key: 'Tech length a-b',
        option: 'Technologies used(less to more)',
        selected: false
    },
]

const onSort = () => {
    isFiltering.value = false
    isSorting.value = !isSorting.value
}

const sort = () => {
    const sortSelected = sortOptions.find(o => o.selected)
    hasSorting.value = sortSelected != undefined

    if (hasSorting.value) {
        switch (sortSelected.key) {
            case 'Date created a-b':
                projectStore.projects.sort((a, b) => new Date(a.dateCreated).getTime() - new Date(b.dateCreated).getTime()); break
            case 'Date created b-a':
                projectStore.projects.sort((a, b) => new Date(b.dateCreated).getTime() - new Date(a.dateCreated).getTime()); break
            case 'Tech length a-b':
                projectStore.projects.sort((a, b) => a.languagesUsed.length - b.languagesUsed.length); break
            case 'Tech length b-a':
                projectStore.projects.sort((a, b) => b.languagesUsed.length - a.languagesUsed.length); break
        }
    }
    isSorting.value = false
}

//@ts-ignore
const languages = Object.values(language) as const
type ProjectFiltering = typeof languages[number]
const filterOptions: Components.SelectOption<ProjectFiltering>[] = [];

const onFilter = () => {
    isSorting.value = false
    isFiltering.value = !isFiltering.value
}

const onSearch = async () => {
    spinner.value.show()
    await getFilteredProjects({
        searchValue: searchValue.value,
        languages: filterOptions.filter(o => o.selected).map(o => o.key as Language)
    })
    .then(r => {
        projectStore.projects = r
        sort()
    })
    .catch(_ => { })
    .finally(() => spinner.value.hide())

    isSorting.value = false
    isFiltering.value = false
}

const onClear = async () => {
    isSorting.value = false
    isFiltering.value = false
    sortSelect.value.clear()
    filterSelect.value.clear()
    searchValue.value = ''
    projectStore.projects.sort((a, b) => a.id - b.id)
    projectStore.loadProjects()
}

onMounted(() => {
    languages.map(l => {
        filterOptions.push({
            key: l,
            option: languageDescription(l),
            selected: false
        })
    })

    //TODO: Not working properly
    window.addEventListener('keydown', async (event: KeyboardEvent) => {
        if (event.key == "Enter") {
            await onSearch()
        }
    })
})
</script>

<style scoped>
.input-filters {
    background: rgb(33, 37, 41, 0.5);
    border: 1px solid rgb(66, 184, 131, 0.5);
}

.inpt-search::placeholder {
    color: rgb(255, 255, 255, 0.6)
}

.btn-filter {
    max-width: 2rem;
    max-height: 2rem;
}
</style>