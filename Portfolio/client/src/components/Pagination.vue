<template>
    <div>
        <slot />
    </div>
</template>

<script setup lang="ts" generic="T">
import { computed, ref } from 'vue';

const props = defineProps<{
    items: Array<T>,
    pageSize: number
}>();

const startindex = ref<number>(0);
const lastIndex = ref<number>(props.pageSize);
const hasNextPage = computed(() => (lastIndex.value >= props.items.length));
const hasPreviousPage = computed(() => startindex.value == 0);

const page = computed(() => props.items.slice(startindex.value, lastIndex.value));

const next = () => {
    if (lastIndex.value < props.items.length) {
        startindex.value += props.pageSize;
        lastIndex.value += props.pageSize;
    }
};

const previous = () => {
    if (startindex.value > 0) {
        startindex.value -= props.pageSize;
        lastIndex.value -= props.pageSize;
    }
};

defineExpose({
    next,
    previous,
    page,
    hasNextPage,
    hasPreviousPage
});
</script>