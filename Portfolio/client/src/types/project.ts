export const language = {
    CSharp: 1,
    Python: 2,
    SQL: 3,
    Javascript: 100,
    HTML: 101,
    Css: 102,
    Vue: 200,
    React: 201,
    Angular: 202,
    Typescript: 203,
    NodeRed: 204
} as const

export type Language = typeof language[keyof typeof language]

export interface Project {
    id: number
    name: string
    dateCreated: string
    languagesUsed: Language[]
    githubPath: string
    imageUrl: string
    description: string
}

export const languageDescription = (value: number) => {
    switch (value) {
        case 1: return "C#";
        case 2: return "Python";
        case 3: return "SQL";
        case 100: return "Javascript";
        case 101: return "HTML";
        case 102: return "CSS";
        case 200: return "Vue";
        case 201: return "React";
        case 202: return "Angular";
        case 203: return "Typescript";
        case 204: return "NodeRed";
    }
}

export interface ProjectLanguage {
    key: string
    value: Language
    isActive?: boolean
}

export const getAllLanguages = () => {
    const keys: number[] = Object.values(language)
    const Languages: ProjectLanguage[] = []
    keys.forEach(k => {
        Languages.push(
            {
                key: languageDescription(k),
                value: k
            } as ProjectLanguage
        )
    })
    return Languages
}

export interface AddProjectRequest {
    name: string
    dateCreated: string
    languages: Language[]
    githubPath: string
    imageurl: string
    description: string
}

export interface UpdateProjectRequest{
    name: string
    dateCreated: string
    languages: Language[]
    githubPath: string
    imageurl: string
    description: string
}

export interface GetFilteredProjectsRequest{
    searchValue: string
    languages: Language[]
}