export const role = {
    Administrator: 0,
    TestUser: 1
} as const

export type Role = typeof role[keyof typeof role]

export interface User {
    id: number,
    userName: string
    role: Role
}

export interface LoginRequest {
    email: string
    password: string
}

export interface UserInfo {
    isAuthenticated: boolean
    role?: Role[]
}

export const userRole = (value: Number) => {
    switch (value) {
        case 0: return "Admin"
        case 1: return "Test user"
    }
}

export interface UserRole {
    key: string,
    value: Role
}

export const getAllRoles = () => {
    const keys: number[] = Object.values(role)
    const users: UserRole[] = []
    keys.forEach(k => {
        users.push(
            {
                key: userRole(k),
                value: k
            } as UserRole
        )
    })
    return users
}

export interface UpdateUserRequest{
    userRole: Role
}