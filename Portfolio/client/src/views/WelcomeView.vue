<template>
    <PageTitle :title="'Welcome'"></PageTitle>

    <div class="d-flex justify-content-around text-white w-100">
        <div class="intro">
            <div class="fs-1">
                <span class="text-prim">{{ '<' }} </span>
                        <span>Hello there</span>
                        <span class="text-prim">{{ '/>,' }}</span>
            </div>

            <div class="d-flex flex-column gap-4">
                <div class="fs-1" style="width: 25ch;">
                    I am <span class="text-prim fw-bold">Orestis Kefis</span>, a full-stack Software Developer
                    with experience in web-applications
                </div>

                <div>
                    <RouterLink :to="'/about'" class="text-decoration-none text-white bg-prim rounded-2 p-3 router-link">
                        Get to know me
                    </RouterLink>
                </div>
            </div>
        </div>

        <div>
            <!--TODO: My photo-->
        </div>
    </div>
</template>

<script setup lang="ts">
import PageTitle from '@/components/PageTitle.vue';
</script>

<style scoped>
.intro {
    margin-top: 6rem;
}
</style>