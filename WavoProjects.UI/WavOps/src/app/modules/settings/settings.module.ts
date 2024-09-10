import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared.module';
import { MainComponent } from './main/main.component';
import { SettingsRoutingModule } from './settings-routing.module';
import { MatTableModule } from '@angular/material/table';
import { EditProjectsComponent } from './edit-projects/edit-projects.component';
import { MatSortModule } from '@angular/material/sort';
import { EditTeamMembersComponent } from './edit-team-members/edit-team-members.component';
import { EditTeamsComponent } from './edit-teams/edit-teams.component';
import { AddEditTeamComponent } from './dialogs/add-edit-team/add-edit-team.component';
import { NgxColorsModule } from 'ngx-colors';
import { ProjectItemComponent } from '../project-board/project-item/project-item.component';
import { ProjectBoardModule } from '../project-board/project-board.module';
import { TeamMemberTimesheetDetailComponent } from './team-member-timesheet-detail/team-member-timesheet-detail.component';
import { AddEditTeamMemberComponent } from './dialogs/add-edit-team-member/add-edit-team-member.component';
import { ChangePinComponent } from './dialogs/change-pin/change-pin.component';
import { AddEditTimesheetComponent } from './dialogs/add-edit-timesheet/add-edit-timesheet.component';



@NgModule({
  declarations: [MainComponent, EditProjectsComponent, EditTeamMembersComponent, EditTeamsComponent, AddEditTeamComponent, TeamMemberTimesheetDetailComponent, AddEditTeamMemberComponent, ChangePinComponent, AddEditTimesheetComponent],
  imports: [
    MatTableModule,
    CommonModule,
    SettingsRoutingModule,
    SharedModule,
    MatTableModule,
    MatSortModule,
    NgxColorsModule,
    ProjectBoardModule
  ],
})
export class SettingsModule { }
