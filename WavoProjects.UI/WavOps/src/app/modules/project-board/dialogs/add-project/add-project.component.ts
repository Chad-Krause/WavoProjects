import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Project } from '../../../../models/project';
import { Team } from '../../../../models/team';
import { NameId } from '../../../../models/name-id';
import { ApiService } from '../../../../services/api.service';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.scss']
})
export class AddProjectComponent implements OnInit {
  exitingProject?: Project;
  loading: boolean = false;

  projectForm = new FormGroup({
    id: new FormControl<number>(0),
    name: new FormControl("", Validators.required),
    description: new FormControl(""),
    teamId: new FormControl<number | null>(0, Validators.required),
    projectOwnerId: new FormControl<number | null>(null)
  });

  teams: Team[] = [];
  teamMembers: NameId[] = [];

  constructor(
    public dialogRef: MatDialogRef<AddProjectComponent>, 
    private api: ApiService,
    @Inject(MAT_DIALOG_DATA) public data?: Project) {

      if(data != null) {
        this.exitingProject = data;
        let temp = {id: data.id, name: data.name, description: data.description!, teamId: data.teamId!, projectOwnerId: data.projectOwnerId!}
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
