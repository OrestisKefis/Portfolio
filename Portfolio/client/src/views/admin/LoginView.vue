<template>
    <div class="d-flex flex-column justify-content-center align-items-center login-background">
        <form class="d-flex flex-column gap-4 shadow p-5 form-container rounded form-admin-login">
            <div class="text-center fs-2 text-light">
                Sign in
            </div>

            <div class="d-flex flex-column gap-3">
                <div class="w-100 form-floating">
                    <input id="inpt-username" class="form-control" type="text" placeholder="name@example.com"
                        v-model="email">
                    <label for="inpt-username">Email</label>
                </div>

                <div class="w-100 form-floating">
                    <input id="inpt-password" class="form-control" type="password" placeholder="S0mePasSW0rd73#"
                        v-model="password">
                    <label for="inpt-password">Password</label>
                </div>
            </div>

            <Button type="submit" @click.prevent="onSubmit">
                Log in
            </Button>
        </form>
    </div>
</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores/authStore';
import type { LoginRequest } from '@/types/user';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

import Button from '@/components/Button.vue';

const autStore = useAuthStore()
const router = useRouter()

const email = ref<string>('')
const password = ref<string>('')

const request = ref<LoginRequest>()

//TODO: Should be used on form submit
const onSubmit = async () => {
    //TODO: Create custom validators for the given values
    request.value = {
        email: email.value,
        password: password.value
    }
    await autStore.loginService(request.value)
    if (autStore.user.isAuthenticated) {
        router.push({ name: "ProjectManager" })
    }
}
</script>

<style scoped>
.form-admin-login {
    background: rgb(33, 37, 41, 0.9);
}

@media screen and (min-width: 1200px) {
    .form-admin-login {
        width: 25%;
    }
}

@media screen and (min-width: 600px) and (max-width: 1099px) {
    .form-admin-login {
        width: 50%;
    }
}

@media screen and (max-width: 599px) {
    .form-admin-login {
        width: 75%;
    }
}

.login-background {
    background: url("../../assets/images/pen-laptop-admin.jpg");
    background-size: cover;
    background-repeat: no-repeat;
    background-attachment: fixed;
    min-height: 100vh;
}
</style>