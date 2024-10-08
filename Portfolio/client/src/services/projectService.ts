import type { GetFilteredProjectsRequest, Project } from "@/types/project";
import axios, { type AxiosRequestConfig } from "axios";

export const getProjects = async (config?: AxiosRequestConfig) => (await axios.get<Project[]>(`/api/projects`, config)).data

export const getProjectById = async (id: number, config?: AxiosRequestConfig) => (await axios.get<Project>(`/api/projects/${id}`, config)).data

export const getFilteredProjects = async (request: GetFilteredProjectsRequest, config?: AxiosRequestConfig) => (await axios.post<Project[]>("/api/projects/filter", request, config)).data