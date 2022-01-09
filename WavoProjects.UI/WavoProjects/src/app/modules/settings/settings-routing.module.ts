import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EditProjectsComponent } from './edit-projects/edit-projects.component';
import { EditTeamsComponent } from './edit-teams/edit-teams.component';
import { MainComponent } from './main/main.component';
import { EditTeamMembersComponent } from './edit-team-members/edit-team-members.component';


const routes: Routes = [
  {
    path: "",
    component: MainComponent,
    children: [
      {
        path: 'projects',
        component: EditProjectsComponent
      },
      {
        path: 'teams',
        component: EditTeamsComponent
      },
      {
        path: 'team-members',
        component: EditTeamMembersComponent
      },
      {
        path: 'misc',
        component: MainComponent
      },

    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule { }
