import type { FormInput } from "@/types/formInput";
import { defineStore } from "pinia";
import { ref } from "vue";

export const useFormStore = defineStore('formError', () => {
    const hasErrors = ref<boolean>(false)
    const formInputs = ref<FormInput[]>([])

    const submit = (event: Event) => {
        hasErrors.value = formInputs.value.some(i => i.errors.length > 0)
        hasErrors.value && event.preventDefault()
    }

    return { hasErrors, formInputs, submit }
})