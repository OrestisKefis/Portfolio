<template>
    <div class="d-flex gap-3 w-fit-content" v-bind="$attrs">
        <FindMeIcon v-for="media in medias" :key="media" :social="media" />
    </div>
</template>

<script setup lang="ts">
import FindMeIcon from './FindMeIcon.vue';

const props = defineProps<{ media?: Types.Social[] }>()

const medias: Types.Social[] = props.media || ["Facebook", "Instagram", "LinkedIn", "Github"]
</script>