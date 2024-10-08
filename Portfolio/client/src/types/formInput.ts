export interface FormInput{
    id: string
    errors?: FormError[]
}

export interface FormError {
    readonly name: Validators
    readonly priority: number
    readonly message: string
}

export type Validators = 
    "notEmpty"
    | "minChar"
    | "maxChar"
    | "inRange"
    | "containsSpecialChar"
    | "isEmail"
    | "mustNotContainSpecialChar"
    | "isOnlyAlphabetical"

export const formErrors: FormError[] = [
    {
        name: 'notEmpty',
        priority: 100,
        message: "*Cannot be empty"
    },
    {
        name: 'minChar',
        priority: 90,
        message: "*Must be at least 2 characters long"
    },
    {
        name: 'inRange',
        priority: 95,
        message: "*Must be between 2-12 characters"
    },
    {
        name: 'maxChar',
        priority: 92,
        message: "*Must not exceed 12 characters"
    },
    {
        name: 'containsSpecialChar',
        priority: 60,
        message: "*Must contain at least on special character(!@#$)"
    },
    {
        name: 'isEmail',
        priority: 99,
        message: "*Must be an email"
    },
    {
        name: 'mustNotContainSpecialChar',
        priority: 60,
        message: "*Can only contain the following special characters(!@#$)"
    },
    {
        name: 'isOnlyAlphabetical',
        priority: 89,
        message: "*Can only contain letters"
    }
]    
