import { Project } from "./project";

export class ProjectSortOrder {
    projectId: number;
    sortOrder: number;

    constructor(id: number, index: number) {
        this.projectId = id;
        this.sortOrder = index;
    }
}