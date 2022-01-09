import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService } from 'src/app/services/api.service';
import { NameId } from 'src/app/models/name-id';
import { Team } from 'src/app/models/team';
import { TeamMember } from 'src/app/models/team-member';
import { Project } from 'src/app/models/project';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.scss']
})
export class AddProjectComponent implements OnInit {
  exitingProject?: Project
  loading: boolean = false;

  projectForm = new FormGroup({
    id: new FormControl(null),
    name: new FormControl("", Validators.required),
    description: new FormControl(""),
    teamId: new FormControl(null, Validators.required),
    projectOwnerId: new FormControl(null)
  });

  teams: Team[] = [];
  teamMembers: NameId[] = [];

  constructor(
    public dialogRef: MatDialogRef<AddProjectComponent>, 
    private api: ApiService,
    @Inject(MAT_DIALOG_DATA) public data?: Project) {

      if(data != null) {
        this.exitingProject = data;
        let temp = {id: data.id, name: data.name, description: data.description, teamId: data.teamId, projectOwnerId: data.projectOwnerId}
        this.projectForm.setValue(temp);
      }
  }


  ngOnInit(): void {
    this.api.getTeams().subscribe(teams => this.teams = teams.map(i => new Team(i)));
    this.api.getTeamMembers().subscribe(tm => {
      this.teamMembers = tm.map(i => new NameId(i));
      this.teamMembers.unshift(new NameId({id: null, name: "None"}));
    });
  }

  addProject() {
    if(this.projectForm.valid) {
      this.loading = true;
      this.api.createOrUpdateProject(this.projectForm.value).subscribe(res => {
        this.dialogRef.close();
      });
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
