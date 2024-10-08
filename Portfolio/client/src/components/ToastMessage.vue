<template>
    <div v-if="isVisible" class="position-absolute top-0 end-0 z-3 bg-light mt-5 me-5 rounded shadow toast-message">
        <div class="bg-danger rounded-top message-label">
            <div class="float-end p-0">
                <button class="btn border-0" @click="hide">
                    <i class="fa fa-xmark"></i>
                </button>
            </div>
        </div>

        <div class="d-flex gap-4 justify-content-between align-item p-3 info-container">
            <div>
                <i class="fa-solid fa-circle-exclamation text-danger fs-2"></i>
            </div>

            <div>
                {{ message }}
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const interval = ref<number>()

const message = ref<string>()
const label = ref<Types.ToastLabel>()
const timeoutMs = ref<number>(3000)

const isVisible = ref<boolean>(false)

const show = (msg: string, lbl: Types.ToastLabel, ms?: number) => {
    clearInterval(interval.value)
    message.value = msg
    label.value = lbl
    ms != undefined && ms > 0 && (timeoutMs.value = ms)
    isVisible.value = true
    interval.value = setInterval(hide, timeoutMs.value)
}

const hide = () => {
    isVisible.value = false
    clearInterval(interval.value)
}

defineExpose({
    show
})
</script>

<style scoped>
.toast-message {
    z-index: 20 !important;
}

.message-label {
    height: 1.8rem;
}

.info-container {
    max-width: 40ch;
}
</style>