import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { env } from 'node:process';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { NameId } from '../models/name-id';
import { Project } from '../models/project';
import { ProjectSortOrder } from '../models/project-sort-order';
import { Team } from '../models/team';
import { UpdateProjectPriorityAndSortOrders } from '../models/update-project-priority-and-sort-orders';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  updateProjectPriorityAndSortOrders(projectId: number, newPriorityId: number, newSortOrders: ProjectSortOrder[]): Observable<boolean> {
    let model: UpdateProjectPriorityAndSortOrders = {
      id: projectId,
      newPriorityId: newPriorityId,
      sortOrders: newSortOrders
    };
    return this.http.post<boolean>(this.baseUrl + "ProjectBoard/UpdateProjectPriorityAndSortOrders", model);
  }

  getTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(this.baseUrl + "Reference/GetTeams")
  }

  createProject(project): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + "ProjectBoard/CreateProject", project)
  }
}
