import { Team } from "./team";
import { Priority } from "./priority";

export class Project {
    id: number;
    name: string;
    description?: string;
    teamId: number;
    team: Team;
    priorityId?: number;
    priority?: Priority;
    sortOrder?: number;
    startedOn?: Date;
    createdOn: Date;
    updatedOn: Date;
}

export const ExampleProjects: Project[] = [
    {
        id: 1,
        name: "T-Shirt Cannon Robot",
        priorityId: 1,
        priority: {
            id: 1,
            name: "High"
        },
        teamId: 1,
        team: {
            id: 1,
            name: "Design/CAD",
            color: "#4700d8"
        },
        sortOrder: 1,
        createdOn: new Date(),
        updatedOn: new Date(),
    },
    {
        id: 2,
        name: "Chairman's Video",
        priorityId: 2,
        priority: {
            id: 2,
            name: "Medium"
        },
        teamId: 2,
        team: {
            id: 2,
            name: "Business",
            color: "#ef0000"
        },
        sortOrder: 2,
        createdOn: new Date(),
        updatedOn: new Date(),
    },
    {
        id: 3,
        name: "Signage",
        priorityId: 3,
        priority: {
            id: 3,
            name: "Low"
        },
        teamId: 2,
        team: {
            id: 3,
            name: "Media",
            color: "#00ff2d"
        },
        sortOrder: 3,
        createdOn: new Date(),
        updatedOn: new Date(),
    }
];