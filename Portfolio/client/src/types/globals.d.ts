declare namespace Types{
    export interface FormActions{
        create?: boolean
        edit?: boolean
    }

    export type Social = "Facebook" | "Instagram" | "LinkedIn" | "Github"
    
    export type ToastLabel = "warning" | "error" | "success"

    type Order = 'a-b' | 'b-a'
    export type Keys<T> = `${T} ${Order}` | T
}

declare namespace Components{
    export interface Modal{
        show: () => void
        hide: () => void
    }

    export interface Pagination<T>{
        next: () => void
        previous: () => void
        page: Array<T>
        hasNextPage: boolean
        hasPreviousPage: boolean
    }

    export interface ToastMessage{
        show: (msg: string, lbl: Types.ToastLabel, ms?: Number) => void
    }

    export interface Spinner{
        show: () => void
        hide: () => void
    }

    export interface Select<T>{
        clear: () => void
    }
        
    export interface SelectOption<T>{
        key: Types.Keys<T>
        option: any
        value?: string
        selected: boolean
    }
}