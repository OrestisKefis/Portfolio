<template>
    <PageTitle :title="'Experience'"></PageTitle>

    <div class="container d-grid gap-4 mt-5 text-white p-5 shadow skills-container scrollbar-prim">
        <div v-if="technologies.length > 0" v-for="tech in technologies" :key="tech.Language.value" :class="{
            'p-4 rounded h-fit-conent skill-card': (tech.Projects.length > 0 || tech.LevelOfExperience > 0)
        }">
            <div v-if="(tech.Projects.length > 0 && tech.LevelOfExperience > 0) || tech.LevelOfExperience > 0">
                <div>
                    {{ tech.Language.key }}
                    <span class="fs-7 text-prim" v-if="tech.Projects.length > 0">
                        {{ tech.Projects.length }} projects
                    </span>
                </div>

                <div>
                    <template v-if="tech.Projects.length > 0">
                        <span class="fs-7 text-prim">
                            <span v-if="tech.TimePassed.Years">
                                {{ tech.TimePassed.Years }} years
                            </span>

                            <span v-if="tech.TimePassed.Months">
                                {{ tech.TimePassed.Months }} months
                            </span>

                            <span v-if="tech.TimePassed.Days">
                                {{ tech.TimePassed.Days }} days
                            </span>
                        </span>
                    </template>
                </div>

                <div class="w-100 d-flex gap-1 p-1 border level">
                    <!--TODO: Should select width with a little math. Pass it possibly using v-bind on the css attribute-->
                    <div v-for="_ in tech.LevelOfExperience" class="h-100 bg-prim level-bar">
                        &nbsp;
                    </div>
                </div>

                <div>
                    <button :disabled="!(tech.Projects.length > 0)" class="btn text-white border-0"
                        @click.prevent="tech.IsExpanded = !tech.IsExpanded">
                        <i class="" :class="{
                            'fa fa-angle-up': tech.IsExpanded,
                            'fa fa-angle-down': !tech.IsExpanded
                        }"></i>
                    </button>

                    <div class="border-top tech-content" :class="{
                        'overflow-hidden downscale': !tech.IsExpanded,
                        'expand': tech.IsExpanded
                    }">
                        <div class="d-flex flex-wrap p-3 gap-2">
                            <div v-for="project in tech.Projects"
                                class="text-center br-prim text-prim fs-7 rounded-5 px-2 py-1">
                                {{ project.name }}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { useProjectStore } from '@/stores/projectStore';
import { getAllLanguages, type Project } from '@/types/project';
import { type TechnologyInfo, getLevel, type TimePassed } from '@/types/technology';
import { onMounted, ref, type Ref } from 'vue';

import PageTitle from '@/components/PageTitle.vue';

const technologies: Ref<TechnologyInfo[]> = ref<TechnologyInfo[]>([])
const projectStore = useProjectStore()

const getTimePassed = (projects: Project[]): TimePassed => {
    let timePassed: TimePassed = {
        Years: 0,
        Months: 0,
        Days: 0
    }
    if (projects.length > 0) {
        const projectDate = new Date(
            projects.sort((a, b) =>
                new Date(a.dateCreated).getTime() - new Date(b.dateCreated).getTime()
            )[0].dateCreated)

        const currentDate = new Date()
        const diffInMs = currentDate.getTime() - projectDate.getTime()
        const msInDay = 1000 * 60 * 60 * 24

        let days = Math.floor(diffInMs / msInDay)

        const years = Math.floor(days / 365.25);
        days -= Math.floor(years * 365.25)

        const months = Math.floor(days / 30.44);
        days -= Math.floor(months * 30.44)

        timePassed = {
            Years: years,
            Months: months,
            Days: days
        }
    }
    return timePassed
}

onMounted(async () => {
    projectStore.projects.length == 0 && await projectStore.loadProjects()
    getAllLanguages()
        .map(l => {
            const projectsUsedIn: Project[] = projectStore.projects.length > 0
                ? projectStore.projects
                    .filter(p => p.languagesUsed
                        .some(lan => lan == l.value))
                : null
            technologies.value.push({
                Language: l,
                About: "",
                LevelOfExperience: getLevel(l.value),
                IsExpanded: false,
                Projects: projectsUsedIn,
                TimePassed: getTimePassed(projectsUsedIn)
            })
        })
    technologies.value.sort((a, b) => b.LevelOfExperience - a.LevelOfExperience)
})
</script>

<style scoped>
.skill-card {
    background: rgb(33, 37, 41, 0.9);
}

@media screen and (max-width: 1920px) and (min-width: 1370px) {
    .skills-container {
        grid-template-columns: repeat(3, 1fr);
    }

    @media screen and (max-width: 1920px) and (min-width: 1745px) {
        .skills-container {
            max-height: 46rem;
            overflow-y: scroll;
        }
    }
}

@media screen and (max-width: 1744px) and (min-width: 1280px) {
    .skills-container {
        max-height: 30rem;
        overflow-y: scroll;
    }
}

@media screen and (max-width: 1369px) and (min-width: 800px) {
    .skills-container {
        grid-template-columns: repeat(2, 1fr);
    }
}

.level {
    height: 60px;
}

.level-bar {
    width: 10%;
}

.expand {
    height: fit-content;
    max-height: 200px;
    overflow-y: scroll;
    overflow-y: hidden;
    transition: 0.7s;
}

.downscale {
    max-height: 0;
    transition: 0.4s;
}

.projects-container {
    grid-template-columns: repeat(2, 1fr);
}
</style>