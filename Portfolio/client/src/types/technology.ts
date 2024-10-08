import type { Project, ProjectLanguage } from "./project"

export type Level = 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10

export interface Technology {
    Language: ProjectLanguage
    About: string
    Projects?: Project[]
    LevelOfExperience: Level
    TimePassed: TimePassed
}

export interface TimePassed{
    Years: number
    Months: number
    Days: number
}

export interface TechnologyInfo extends Technology{
    IsExpanded: boolean
}

//TODO: Probably should be stored in the db and be fetched from there. Remains to be decided
export const getLevel: (l: number) => Level = (language: number) => {
    switch (language) {
        case 1: return 8  //C#
        case 2: return 1  //Python
        case 3: return 7  //SQL
        case 100: return 7  //Javascript
        case 101: return 6  //HTML
        case 102: return 5  //CSS
        case 200: return 6  //Vue
        case 201: return 0  //React
        case 202: return 0  //Angular
        case 203: return 6  //Typescript
        case 204: return 8  //NodeRed
        default: return 0
    }
}