export interface Quality{
    Label: string;
    Icon: string;
    IsActive: boolean
}

export const Qualities:Quality[] = [
    {
        Label: 'Team player',
        Icon: 'fa-people-group',
        IsActive: false
    },
    {
        Label: 'Passionate',
        Icon: 'fa-fire-flame-curved',
        IsActive: false
    },
    {
        Label: 'Fast learner',
        Icon: 'fa-bolt',
        IsActive: false
    },
    {
        Label: 'Persistant',
        Icon: 'fa-magnet',
        IsActive: false
    },
    {
        Label: 'Gamer',
        Icon: 'fa-gamepad',
        IsActive: false
    },
]