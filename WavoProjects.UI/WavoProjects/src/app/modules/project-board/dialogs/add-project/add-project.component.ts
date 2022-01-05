import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService } from 'src/app/services/api.service';
import { NameId } from 'src/app/models/name-id';
import { Team } from 'src/app/models/team';
import { TeamMember } from 'src/app/models/team-member';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.scss']
})
export class AddProjectComponent implements OnInit {
  projectForm = new FormGroup({
    name: new FormControl("", Validators.required),
    description: new FormControl(""),
    teamId: new FormControl(null, Validators.required),
    projectOwnerId: new FormControl(null)
  });

  teams: Team[] = [];
  teamMembers: TeamMember[] = [];

  constructor(public dialogRef: MatDialogRef<AddProjectComponent>, private api: ApiService) {
  }


  ngOnInit(): void {
    this.api.getTeams().subscribe(teams => this.teams = teams.map(i => new Team(i)));
    this.api.getTeamMembers().subscribe(tm => {
      this.teamMembers = tm.map(i => new TeamMember(i));
      this.teamMembers.unshift(new TeamMember({id: null, name: "None"}));
      console.log(this.teamMembers)
    });
  }

  addProject() {
    if(this.projectForm.valid) {
      this.api.createProject(this.projectForm.value).subscribe();
      this.dialogRef.close();
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
