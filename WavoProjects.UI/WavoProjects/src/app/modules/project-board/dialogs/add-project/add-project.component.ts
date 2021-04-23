import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ApiService } from 'src/app/services/api.service';
import { NameId } from 'src/app/models/name-id';
import { Team } from 'src/app/models/team';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.scss']
})
export class AddProjectComponent implements OnInit {
  projectForm = new FormGroup({
    name: new FormControl("", Validators.required),
    description: new FormControl(""),
    teamId: new FormControl(null, Validators.required)
  });

  teams: Team[] = [];

  constructor(public dialogRef: MatDialogRef<AddProjectComponent>, private api: ApiService) {
  }


  ngOnInit(): void {
    this.api.getTeams().subscribe(teams => this.teams = teams.map(i => new Team(i)));
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
