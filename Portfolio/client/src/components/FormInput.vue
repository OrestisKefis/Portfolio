<template>
    <slot>
        <div class="d-flex" :class="{
            'justify-content-between align-items-center': flex == 'horizontal',
            'flex-column': flex == 'vertical'
        }"> 
            <label v-if="labelText">{{ labelText }}</label>

            <div class="position-relative">
                <input :id="id" v-bind="$attrs" v-model="value" class="form-control" @input="validateInput()">

                <div v-if="(store.hasErrors && errors.length > 0) || showError"
                    class="text-danger fs-7 position-absolute">
                    {{ errors.sort((a, b) => b.priority - a.priority)[0].message }}
                </div>
            </div>
        </div>
    </slot>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { FormError, Validators } from '@/types/formInput';
import { useFormStore } from "@/stores/formStore"
import { formErrors } from "@/types/formInput"

type FlexStyle = "vertical" | "horizontal"

const props = withDefaults(defineProps<{
    id?: string
    labelText?: string
    validators?: Validators[]
    flex?: FlexStyle
}>(), {
    flex: 'vertical'
})

const store = useFormStore()

const value = defineModel<string>()

const showError = ref<boolean>(false)
const errors = ref<FormError[]>([])

const validateInput = (show: boolean = null) => {
    const currentFormInput = store.formInputs.find(i => i.id == props.id)
    let isError = false
    let regex: any;;
    let error: FormError;
    let condition: boolean;

    // if (!props.validators) return

    if (!props.validators || props.validators.length <= 0) return

    props.validators.forEach(v => {
        error = formErrors.find(e => e.name == v)
        switch (v) {
            case 'notEmpty': condition = value.value.length == 0; break
            case 'inRange': condition = value.value.length < 2 || value.value.length > 12; break
            case 'minChar': condition = value.value.length < 2; break
            case 'maxChar': condition = value.value.length > 12; break
            case 'isOnlyAlphabetical':
                // regex = /^[a-zA-Z]+$/
                regex = /^[a-zA-Z\s]*$/g
                condition = !value.value.match(regex);
                break
            case 'isEmail':
                regex = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/
                condition = !value.value.match(regex);
                break
            case 'containsSpecialChar':
                regex = /[!@#$]/
                condition = !value.value.match(regex);
                break
            case 'mustNotContainSpecialChar':
                regex = /[,.?'"{}_+-=^&*()~`<>:;|]/
                condition = !value.value.match(regex);
                break
            // default: condition = false
        }
        handleError(error, condition)
    })
    isError = errors.value.length > 0
    showError.value = show != null ? show : isError
    currentFormInput.errors = errors.value
}

const handleError = (error: FormError, condition: boolean) => {
    condition
        ? errors.value.indexOf(error) === -1 && errors.value.push(error)
        : errors.value = errors.value.filter(e => e.name != error.name)
}

onMounted(() => {
    !store.formInputs.find(i => i.id == props.id) && store.formInputs.push({ id: props.id })
    validateInput(false)
})
</script>

<style scoped>
.form-control::placeholder{
    color: gray;
}

.form-control::placeholder {
    font-size: 16px;
    transition: 200ms;
}

.form-control:focus::placeholder {
    font-size: 12px;
    transition: 200ms;
}

input{
    border: 1px solid rgb(124, 121, 121);    
}

::-webkit-input-placeholder{
    color: dark
}
</style>