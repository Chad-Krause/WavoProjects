import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NameId } from '../models/name-id';
import { Project } from '../models/project';
import { ProjectSortOrder } from '../models/project-sort-order';
import { Team } from '../models/team';
import { TeamMember } from '../models/team-member';
import { TeamMemberTimesheetRow } from '../models/team-member-timesheet-row';
import { Timesheet } from '../models/timesheet';
import { UpdateProjectPriorityAndSortOrders } from '../models/update-project-priority-and-sort-orders';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getAPIVersion(): Observable<any> {
    return this.http.get<any>(this.baseUrl + "Reference/GetAPIVersion");
  }

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

  getTimesheets(teamMemberId: number): Observable<Timesheet[]> {
    return this.http.get<Timesheet[]>(this.baseUrl + "Settings/GetTimesheets", { params: {teamMemberId: teamMemberId.toString()}});
  }

  createOrUpdateTimesheet(timesheet): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + "Settings/CreateOrUpdateTimesheet", timesheet);
  }

  deleteTimesheet(id: number): Observable<boolean> {
    return this.http.delete<boolean>(this.baseUrl + "Settings/DeleteTimesheet", { params: {id: id.toString()}});
  }

  clockIn(teamMemberId: number): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + "Timesheet/ClockIn", teamMemberId);
  }

  clockOut(teamMemberId: number): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + "Timesheet/ClockOut", teamMemberId);
  }
}
