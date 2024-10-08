<template>
    <PageTitle :title="'About'" />

    <div class="container">
        <div class="d-flex justify-content-between mt-5 align-items-center" :class="{
            'flex-column gap-5': isMobile
        }">
            <div class="d-flex flex-column text-white">
                <div class="fs-1">
                    About
                    <span class="text-prim">
                        {{ '< me />' }}
                    </span>
                </div>

                <div class="text-white fs-2 presentation">
                    After many years of performing various roles I decided to
                    persue my passion and become a part of the vast world of
                    technology and more specifically
                    <span class="text-prim">
                        software development.
                    </span>
                </div>

                <div class="mt-3">
                    <OutlineButton>
                        Download CV
                    </OutlineButton>
                </div>
            </div>

            <div :class="{
                        'position-relative bg-prim portrait': !isMobile
                    }">
                <div class="bg-danger cover-image portrait photo-portrait" :class="{
                    'position-absolute': !isMobile,
                    'm-auto': isMobile
                }"></div>
            </div>
        </div>

        <div class="d-flex flex-wrap justify-content-between qualities-container">
            <QualityCard v-if="qualities" v-for="quality in qualities" :key="quality.Label" class="position-relative"
                :label="quality.Label" :icon="quality.Icon" @mouseover="(e) => onAction(e, quality)"
                @mouseleave="(e) => onAction(e, quality)" @click.prevent="(e) => onAction(e, quality)">
                <div v-if="!isMobile" class="position-absolute info-box" :class="{
                    'info-box-active': quality.IsActive,
                    'info-box-inactive': !quality.IsActive,
                }">

                </div>
            </QualityCard>
        </div>

        <Modal v-if="isMobile" @onHide="onHide" ref="modal">
            <div>

            </div>
        </Modal>
    </div>
</template>

<script setup lang="ts">
import { inject, onMounted, ref, watch, type Ref } from 'vue';
import { type Quality, Qualities } from '@/types/qualities'

import PageTitle from '@/components/PageTitle.vue';
import QualityCard from '@/components/QualityCard.vue';
import Modal from '@/components/Modal.vue';
import OutlineButton from '@/components/OutlineButton.vue';

const windowWidth: Ref<number> = inject('windowWidth')

const qualities = ref<Quality[]>([])
const selectedQuality = ref<Quality>(null)

const isMobile = ref<boolean>(windowWidth.value <= 900)

const modal = ref<Components.Modal>()

const onAction = (event: Event, quality: Quality) => {
    const isClicked = (event.type == 'click')
    if (isMobile.value) {
        if (isClicked) {
            modal.value.show()
            selectedQuality.value = quality
        }
    } else {
        !isClicked && (quality.IsActive = (event.type == 'mouseover'))
    }
}

const onHide = () => { selectedQuality.value = null }

onMounted(() => qualities.value = Qualities)

watch(windowWidth, () => isMobile.value = windowWidth.value <= 900)
</script>

<style scoped>
.presentation {
    max-width: 40ch;
    border-top: 1px solid #42b883;
}

.photo-portrait {
    background: url("../assets/images/gray-me.jpg");
    background-position: center;
}

.qualities-container {
    margin-top: 10rem;
}

.info-box {
    background-color: black;
    left: 5.9rem;
    bottom: 5.9rem;
}

.info-box-inactive {
    transition: 0.6s;
    width: 0;
    height: 0;
}

.info-box-active {
    transition: 0.6s;
    width: 200px;
    height: 300px;
}

@media screen and (max-width: 2000px) and (min-width: 900px) {
    .portrait {
        width: 300px;
        height: 400px;
    }

    .photo-portrait {
        left: -70px;
        top: 70px;
    }
}

@media screen and (max-width: 899px) and (min-width: 300px) {
    .photo-portrait {
        width: 300px;
        height: 300px;
        border-radius: 50%;
    }
}
</style>