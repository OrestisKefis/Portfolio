<template>
    <div class="shadow border-bottom bg-dark text-light">
        <div class="d-flex justify-content-between p-4">
            <div class="d-flex gap-3">
                <RouterLink :to="'/admin'" class="text-light text-decoration-none navbar-item">Projects</RouterLink>
                <RouterLink :to="'/admin/users'" class="text-light text-decoration-none navbar-item">Users</RouterLink>
            </div>

            <div>
                <button class="border-0 bg-dark text-light fa fa-right-from-bracket navbar-item"
                    @click.prevent="$emit('logout')" title="Logout">
                </button>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
defineEmits(['logout'])
</script>

<style scoped>
.navbar-item:hover,
.router-link-exact-active{
    color: #42b883 !important;
}
</style>