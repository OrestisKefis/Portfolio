<template>
    <div class="position-relative">
        <slot />

        <div v-if="expanded" class="position-absolute bg-dark text-light z-3 custom-select">
            <div v-if="options" v-for="option in options.value" :key="option.option" class="p-2 sort-option" :class="{
                'bg-prim': option.selected
            }" @click.prevent="onOptionClick(option)">
                {{ option.option }}
                <slot name="option-content-element" />
            </div>
        </div>
    </div>
</template>

<script setup lang="ts" generic="T">
import { onMounted, type Ref } from 'vue';

const emits = defineEmits(['window-click', 'option-click'])

const props = withDefaults(defineProps<{
    options: Ref<Components.SelectOption<T>[]>,
    expanded?: boolean,
    allowMultiple?: boolean
}>(), {
    expanded: false,
    allowMultiple: false
})

const onOptionClick = (selectedOption: Components.SelectOption<T>) => {
    if (!props.allowMultiple) {
        clear()
        selectedOption.selected = true        
    }else{
        //TODO: Create multiple selections functionality
        //1) Dont clear
        //2) If selected option is already selected then unselect
        selectedOption.selected = !props.options.value.find(o => o.key == selectedOption.key).selected
    }
    emits('option-click', selectedOption)
}

const clear = () => {
    props.options.value
        .filter(o => o.selected == true)
        .map(o => o.selected = false)
}

// onMounted(() => {
//     document.addEventListener('click', () => {
//         console.log('clicked')
//         emits('window-click')
//     })
// })

defineExpose({
    clear
})
</script>

<style scoped>
.custom-select {
    width: 30ch;
    border: 1px solid #42b883;
}

.sort-option:focus,
.sort-option:hover {
    background-color: #42b883;
    color: rgb(33, 37, 41);
    cursor: pointer;
}
</style>