import { Category } from "./category";
import { PriorityLevel } from "./priority-level";

export class Project {
    id: number;
    name: string;
    priorityLevel?: PriorityLevel;
    category: Category;
}

export const ExampleProjects: Project[] = [
    {
        id: 1,
        name: "T-Shirt Cannon Robot",
        priorityLevel: {
            id: 1,
            name: "High"
        },
        category: {
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
        category: {
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
        category: {
            id: 3,
            name: "Media",
            color: "#00ff2d"
        }
    }
];