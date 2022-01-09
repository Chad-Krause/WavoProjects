import { formatDate, getLocaleDateFormat } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Timesheet } from 'src/app/models/timesheet';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-add-edit-timesheet',
  templateUrl: './add-edit-timesheet.component.html',
  styleUrls: ['./add-edit-timesheet.component.scss']
})
export class AddEditTimesheetComponent implements OnInit {
  existingTimesheet?: Timesheet;
  loading: boolean = false;
  showClockOutWarning: boolean = false;
  calculatedHours: number = 0;

  tsForm = new FormGroup({
    id: new FormControl(null),
    teamMemberId: new FormControl(0, Validators.required),
    clockIn: new FormControl(new Date(), Validators.required),
    clockOut: new FormControl(new Date(), Validators.required)
  });

  constructor(private api: ApiService, private dialogRef: MatDialogRef<AddEditTimesheetComponent>, @Inject(MAT_DIALOG_DATA) public data?: any) {
    
    let temp = { id: null, teamMemberId: data.teamMember.id, clockIn: null, clockOut: null }

    if(data.timesheet != null) {
      this.existingTimesheet = data.timesheet;

      temp.id = data.timesheet.id;
      temp.clockIn = data.timesheet.clockIn;
      temp.clockOut = data.timesheet.clockOut;
    }

    temp.clockIn = this.toBrowserFormat(temp.clockIn);

    if(temp.clockOut) {
      temp.clockOut = this.toBrowserFormat(temp.clockOut);
    }
    
    this.tsForm.setValue(temp);
  }
  
  ngOnInit(): void {
    this.tsForm.valueChanges.subscribe(res => {
      this.calculateTime();
    });
    this.calculateTime();
  }

  calculateTime() {
    if(this.tsForm.value.clockOut == null) return 0;
    
    let ci = new Date(this.tsForm.value.clockIn);
    let co = new Date(this.tsForm.value.clockOut);
    this.calculatedHours =  Math.abs(ci.getTime() - co.getTime()) / 36e5;
  }

  addTimesheet() {
    if(this.tsForm.valid) {
      this.loading = true;
      this.api.createOrUpdateTimesheet(this.tsForm.value).subscribe(res => {
        this.dialogRef.close();
      });
    }
  }

  toBrowserFormat(date: Date) {
    if(date == null) {
      date = new Date();
    }
    return formatDate(date, 'yyyy-MM-ddThh:mm', 'en-US');
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
