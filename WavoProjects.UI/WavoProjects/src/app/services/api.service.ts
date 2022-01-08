import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { env } from 'node:process';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { NameId } from '../models/name-id';
import { Project } from '../models/project';
import { ProjectSortOrder } from '../models/project-sort-order';
import { Team } from '../models/team';
import { TeamMember } from '../models/team-member';
import { TeamMemberTimesheetRow } from '../models/team-member-timesheet-row';
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
    return this.http.get<Team[]>(this.baseUrl + "Reference/GetTeams");
  }

  getTeamMembers(): Observable<NameId[]> {
    return this.http.get<NameId[]>(this.baseUrl + "Reference/GetTeamMembersNameId");
  }

  getProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(this.baseUrl + "Settings/GetProjects");
  }

  createOrUpdateProject(project): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + "ProjectBoard/CreateOrUpdateProject", project);
  }

  deleteProject(id: number): Observable<boolean> {
    return this.http.delete<boolean>(this.baseUrl + "Settings/DeleteProject", { params: {id: id.toString()}});
  }

  createOrUpdateTeam(team): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + "Settings/CreateOrUpdateTeam", team);
  }

  deleteTeam(id: number): Observable<boolean> {
    return this.http.delete<boolean>(this.baseUrl + "Settings/DeleteTeam", { params: {id: id.toString()}});
  }

  getTeamMembersTimesheetData(): Observable<TeamMemberTimesheetRow[]> {
    return this.http.get<TeamMemberTimesheetRow[]>(this.baseUrl + "Settings/GetTeamMembersWithTimesheetInformation");
  }

  createOrUpdateTeamMember(teamMember): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + "Settings/CreateOrUpdateTeamMember", teamMember);
  }

  deleteTeamMember(id: number): Observable<boolean> {
    return this.http.delete<boolean>(this.baseUrl + "Settings/DeleteTeamMember", { params: {id: id.toString()}});
  }

  changePin(form): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + "Settings/ChangePin", form);
  }
}
