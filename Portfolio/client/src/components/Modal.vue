<template>
    <div v-if="isOpen" class="position-fixed bg-black bg-opacity-25 top-0 w-100 vh-100 modal-container">
        <div class="bg-dark text-light m-auto position-relative rounded d-flex flex-column shadow overflow-auto custom-modal"
            :style="{ maxWidth: maxWidth}">
            <div class="border-bottom border-light bg-dark text-light p-2 d-flex align-items-center" :class="{
                'justify-content-end': !title,
                'justify-content-between': title
            }">
                <slot name="title">
                    <div v-if="title" class="fw-bold fs-6 mx-4">
                        {{ title }}
                    </div>
                </slot>

                <div>
                    <button class="fa fa-close border-0 bg-transparent text-danger fs-4" @click="hide"></button>
                </div>
            </div>

            <slot />
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

defineProps<{
    title?: string,
    maxWidth?: string,
    minWidth?: string
}>()

const emits = defineEmits(['onHide', 'onShow'])

const isOpen = ref<boolean>(false);

const show = () => {
    isOpen.value = true
    emits('onShow')
}

const hide = () => {
    isOpen.value = false
    emits('onHide')
}

defineExpose({
    show,
    hide
})
</script>

<style scoped>
.modal-container {
    left: 0;
}

.custom-modal {
    top: 6rem;
    width: fit-content;
    max-width: 75%;
    border: 1px solid #888;
}
</style>