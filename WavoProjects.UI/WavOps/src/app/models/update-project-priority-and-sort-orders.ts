import { ProjectSortOrder } from "./project-sort-order";

export class UpdateProjectPriorityAndSortOrders {
    id: number;
    newPriorityId: number;
    sortOrders: ProjectSortOrder[];
}