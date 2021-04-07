import { Team } from "./team";
import { PriorityLevel } from "./priority-level";

export class Project {
    id: number;
    name: string;
    priorityLevel?: PriorityLevel;
    team: Team;
}

export const ExampleProjects: Project[] = [
    {
        id: 1,
        name: "T-Shirt Cannon Robot",
        priorityLevel: {
            id: 1,
            name: "High"
        },
        team: {
            id: 1,
            name: "Design/CAD",
            color: "#4700d8"
        }
    },
    {
        id: 2,
        name: "Chairman's Video",
        priorityLevel: {
            id: 2,
            name: "Medium"
        },
        team: {
            id: 2,
            name: "Business",
            color: "#ef0000"
        }
    },
    {
        id: 3,
        name: "Signage",
        priorityLevel: {
            id: 3,
            name: "Low"
        },
        team: {
            id: 3,
            name: "Media",
            color: "#00ff2d"
        }
    }
];