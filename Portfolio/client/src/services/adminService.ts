import type { AddProjectRequest, Project, UpdateProjectRequest } from "@/types/project"
import type { LoginRequest, UpdateUserRequest, User } from "@/types/user"
import type { AxiosRequestConfig } from "axios"
import axios from "axios"

const prefix = "/api/admin"

export const getUsers = async (config?: AxiosRequestConfig) => (await axios.get<User[]>(`${prefix}/users`, config)).data

export const updateUser = async (id: number, request: UpdateUserRequest, config? :AxiosRequestConfig) => await axios.put(`${prefix}/users/${id}`, request, config)

export const deleteUser = async (id: number, config?: AxiosRequestConfig) => await axios.delete(`${prefix}/users/${id}`, config)

export const login = (request: LoginRequest, config?: AxiosRequestConfig) => axios.post("/admin/login", request, config)

export const logout = (config?: AxiosRequestConfig) => axios.post("/admin/logout", config)

export const claimsPresence = (config?: AxiosRequestConfig) => axios.post("/admin/claims", config)

export const addProject = async (request: AddProjectRequest, config?: AxiosRequestConfig) => (await axios.post<Project>(`${prefix}/projects`, request, config)).data

export const deleteProject = async (id:number, config?: AxiosRequestConfig) => await axios.delete<Project>(`${prefix}/projects/${id}`, config)

export const updateProject = async (id: number, request: UpdateProjectRequest, config?: AxiosRequestConfig) => (await axios.put<Project>(`${prefix}/projects/${id}`, request, config)).data
