<template>
    <div v-bind="$attrs"
        class="w-100 bg-dark position-relative d-flex flex-column justify-content-between shadow text-white footer" :class="{
            'hidden': isHidden,
            'shown': !isHidden
        }">
        <div class="d-flex justify-content-around align-items-center mt-4">
            <div v-if="!isMobile">
                <div class="d-flex align-items-center gap-3">                    
                    <RouterLink to="/contact" class="text-decoration-none text-white bg-prim py-2 px-3 border-0 rounded router-link">
                        Say Hello
                    </RouterLink>
                </div>
            </div>

            <div class="text-center">
                Contact
                <div class="text-secondary d-flex flex-column fs-7">
                    <div>orestis.kefis@hotmail.com</div>
                    <div>+30 6972733697</div>
                </div>
            </div>

            <div>
                <OutlineButton>
                    Download CV
                </OutlineButton>
            </div>

            <div class="position-absolute toggle-footer-container">
                <button class="bg-dark rounded-top-2 border-0 fs-5" @click.prevent="isHidden = !isHidden">
                    <i class="fa-solid text-white" :class="{
                        'fa-angle-up': isHidden,
                        'fa-angle-down': !isHidden
                    }"></i>
                </button>
            </div>
        </div>

        <div>
            <hr class="text-white">

            <div class="text-secondary text-center fs-7">
                Copyright @ 2024. All rights reservered
            </div>

            <div>
                <FindMe class="m-auto" />
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { inject, onMounted, ref, watch, type Ref } from 'vue';

import FindMe from './FindMe.vue';
import Button from './Button.vue';
import OutlineButton from './OutlineButton.vue';

const windowWidth:Ref<number> = inject("windowWidth")

const isHidden = ref<boolean>(false)
const isMobile = ref<boolean>(windowWidth.value < 770)

onMounted(() => setTimeout(() => isHidden.value = true, 3000))

watch(windowWidth, () => isMobile.value = windowWidth.value < 770)
</script>

<style scoped>
.toggle-footer-container {
    top: -25px;
}

.hidden {
    height: 0px;
    transition: 600ms;
}

.shown {
    height: 200px;
    transition: 600ms;
}
</style>