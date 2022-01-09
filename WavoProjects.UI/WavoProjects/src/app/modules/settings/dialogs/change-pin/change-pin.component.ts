import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-change-pin',
  templateUrl: './change-pin.component.html',
  styleUrls: ['./change-pin.component.scss']
})
export class ChangePinComponent implements OnInit {

  pinForm = new FormGroup({
    id: new FormControl(null),
    pin: new FormControl("", [Validators.minLength(4), Validators.required])
  });

  loading: boolean = false;

  constructor(private api: ApiService, private dialogRef: MatDialogRef<ChangePinComponent>, @Inject(MAT_DIALOG_DATA) public id: number) {
    if(id == null) {
      this.dialogRef.close();
    }

    this.pinForm.setValue({ id: id, pin: '' });
  }
  
  ngOnInit(): void {
  }

  changePin() {
    if(this.pinForm.valid) {
      this.loading = true;
      this.api.changePin(this.pinForm.value).subscribe(res => {
        this.dialogRef.close();
      });
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
