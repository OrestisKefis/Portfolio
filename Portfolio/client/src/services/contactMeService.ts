import type { ContactMeRequest } from "@/types/contactMe";
import axios, { type AxiosRequestConfig } from "axios";

export const send = async (request: ContactMeRequest, config?: AxiosRequestConfig) => axios.post<ContactMeRequest>("/api/contact-me", request, config)