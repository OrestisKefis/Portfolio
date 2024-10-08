export interface ContactMeRequest{
    email: string
    firstName: string
    lastName: string
    subject: string
    message: string
}

export interface AddEmailVerificationRequest{
    email: string
}