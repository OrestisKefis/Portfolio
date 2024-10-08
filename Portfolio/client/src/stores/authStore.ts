import { claimsPresence, login, logout } from "@/services/adminService"
import type { LoginRequest, UserInfo } from "@/types/user"
import { defineStore } from "pinia"
import { ref } from "vue"

export const useAuthStore = defineStore('auth', () => {
    const user = ref<UserInfo>({ isAuthenticated: false })

    const setState = (stateValue: boolean) => {
        user.value.isAuthenticated = stateValue
    }

    const loginService = async (request: LoginRequest) => {
        const response = await login(request)
        response.status < 300 && setState(true)
    }

    const logoutService = async () => {
        const response = await logout()
        response.status < 300 && setState(false)
    }

    const checkClaimsPresence = async () => {
        const response = await claimsPresence()
        switch(response.status){
            case 200: setState(true); break;
            case 204: setState(false); break;
        }
    }

    return { user, loginService, logoutService, checkClaimsPresence }
})