import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-completion',
  templateUrl: './confirm-completion.component.html',
  styleUrls: ['./confirm-completion.component.scss']
})
export class ConfirmCompletionComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<ConfirmCompletionComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CompletionDialogData) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit(): void {
  }

}

export interface CompletionDialogData {
  projectName: string;
}