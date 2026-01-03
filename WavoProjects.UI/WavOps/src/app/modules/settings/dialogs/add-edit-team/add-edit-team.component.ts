import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Team } from '../../../../models/team';
import { Project } from '../../../../models/project';
import { ApiService } from '../../../../services/api.service';

@Component({
  selector: 'app-add-edit-team',
  templateUrl: './add-edit-team.component.html',
  styleUrls: ['./add-edit-team.component.scss']
})
export class AddEditTeamComponent implements OnInit {

  existingTeam?: Team;
  loading: boolean = false;
  exampleProject: Project = new Project({name: "T-Shirt Cannon Robot", teamId: 3, team: {id: 3, name: "Team Name", color: "#c32af3"}});

  teamForm = new FormGroup({
    id: new FormControl<number>(0),
    name: new FormControl<string>("", Validators.required),
    color: new FormControl<string>("#c32af3", Validators.required)
  });

  constructor(private api: ApiService, private dialogRef: MatDialogRef<AddEditTeamComponent>, @Inject(MAT_DIALOG_DATA) public data?: Team) {
    if(data != null) {
      this.existingTeam = data;
      this.teamForm.setValue({ id: data.id, name: data.name, color: data.color });
    }
  }
  
  ngOnInit(): void {
    this.teamForm.valueChanges.subscribe(res => {
      this.exampleProject.team = new Team(this.teamForm.value);
    })
  }

  addTeam() {
    if(this.teamForm.valid) {
      this.loading = true;
      this.api.createOrUpdateTeam(this.teamForm.value).subscribe(res => {
        this.dialogRef.close();
      });
    }
  }

  updateColor() {
    this.teamForm.setValue(this.teamForm.getRawValue())
  }

  onNoClick(): void {
    this.dialogRef.close();
  }


}
