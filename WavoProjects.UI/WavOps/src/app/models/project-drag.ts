import { Project } from "./project";

export class ProjectDrag {
    x: number = 0;
    y: number = 0;
    clientId: string = "";
    projectId: number = 0;
    project: Project = new Project(null);
    style;
}