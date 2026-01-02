import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TeamMember } from '../../../../models/team-member';
import { ApiService } from '../../../../services/api.service';
import { TeamMemberTimesheetRow } from '../../../../models/team-member-timesheet-row';

@Component({
  selector: 'app-add-edit-team-member',
  templateUrl: './add-edit-team-member.component.html',
  styleUrls: ['./add-edit-team-member.component.scss']
})
export class AddEditTeamMemberComponent implements OnInit {

  exitingTeamMember?: TeamMember;
  loading: boolean = false;

  exampleNames: string[] = [ // A bunch of names that came to me randomly
    'Chaddy McChaddingham', 'Dan The Man', 'Thorin', 'Tabitha 👸', 'Tabitha 👿', 'Becky',
    'Palpatine', 'R2-D2', 'C-3PO', 'Anakin', 'Darth Vader', 'Obi-Wan Kenobi', 'The Mandalorian', 'Baby Yoda',
    'Margaret Thatcher', 'Betty White', 'Dean Kamen', 'PewDiePie', 'Amongus', 'Dwayne "The Rock" Johnson',
    'Shrek', 'Lord Farquaad', 'Donkey', 'The Gingerbread Man', 'Cinderella', 'Avatar The Last Airbender',
    'Aang', 'Tom Nook', 'K. K. Slider', 'Mario', 'Luigi', 'Toad', 'Princess Peach', 'Naruto', 'Pickle Rick',
    'Rick', 'Morty', 'Master Chief', 'The Arbitor', 'Sgt. Johnson', 'Cortana', 'Link', 'Zelda', 'Ganon',
    'Iron Man', 'Thor', 'Spiderman', 'Captain America', 'The Hulk', 'Deadpool', 'Thanos', 'Dr. Strange', 
    'Lightning McQueen', 'Nemo 🐠', 'Dory 🐟', 'Mr. Incredible', 'Mrs. Incredible', 'Mike Wazowski', 'Wall-E',
    'Buzz Lightyear', 'Woody', 'Costco', 'Tim Apple', 'Marie Curie', 'Mark Zuckerberg', 'Steve Jobs', 'Bill Gates',
    'Pikachu', 'Squirtle', 'Bulbasaur', 'Charmander', 'Blue-Eyes White Dragon', 'Yu-Gi-Oh', 'Spongebob', 'Patrick Star',
    'Mr. Krabs', 'Squidward', 'Plankton', 'Hannah Montana', 'Dr. Zoidberg', 'Bender', 'Fry', 'Peter Griffin', 'Timmy Turner'];
  selectedExampleName: string = "";

  tmForm = new FormGroup({
    id: new FormControl<number>(0),
    name: new FormControl("", Validators.required),
    trackTime: new FormControl(true),
    pin: new FormControl(""),
    hoursAdjustment: new FormControl(0)
  });

  constructor(private api: ApiService, private dialogRef: MatDialogRef<AddEditTeamMemberComponent>, @Inject(MAT_DIALOG_DATA) public data?: TeamMemberTimesheetRow) {
    if(data != null) {
      this.exitingTeamMember = data;
      this.tmForm.setValue({ id: data.id, name: data.name, trackTime: data.trackTime, hoursAdjustment: data.hoursAdjustment, pin: '' });
    }
    this.selectedExampleName = this.exampleNames[Math.floor(Math.random() * this.exampleNames.length)];
  }
  
  ngOnInit(): void {
  }

  addTeamMember() {
    if(this.tmForm.valid) {
      this.loading = true;
      this.api.createOrUpdateTeamMember(this.tmForm.value).subscribe(res => {
        this.dialogRef.close();
      });
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
