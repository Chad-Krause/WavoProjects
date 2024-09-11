import { formatDate } from '@angular/common';
import { AfterContentInit, AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ConfirmDeleteComponent } from '../../project-board/dialogs/confirm-delete/confirm-delete.component';
import { AddEditTimesheetComponent } from '../dialogs/add-edit-timesheet/add-edit-timesheet.component';
import { TeamMemberTimesheetRow } from '../../../models/team-member-timesheet-row';
import { Timesheet } from '../../../models/timesheet';
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'team-member-timesheet-detail',
  templateUrl: './team-member-timesheet-detail.component.html',
  styleUrls: ['./team-member-timesheet-detail.component.scss']
})
export class TeamMemberTimesheetDetailComponent implements OnInit, AfterViewInit {
  @Input() teamMember!: TeamMemberTimesheetRow;

  @ViewChild(MatSort) sort!: MatSort;
  timesheets: Timesheet[] = [];
  displayedColumns: string[] = ['date', 'duration', 'clockIn', 'clockOut', 'autoClockOut', 'action'];
  ds: MatTableDataSource<Timesheet> = new MatTableDataSource<Timesheet>();

  loading: boolean = false;
  error: boolean = false;

  constructor(private api: ApiService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getData()
  }

  ngAfterViewInit() {
    this.ds.sort = this.sort;
  }

  getData() {
    this.loading = true;
    this.api.getTimesheets(this.teamMember.id).subscribe(res => {
      this.timesheets = res.map(i => new Timesheet(i));
      this.ds.data = this.timesheets;
      this.loading = false;
    }, (err) => {
      this.error = true;
      this.loading = false;
    });
  }

  calculateTotalHours() {
    let sum = 0;
    this.timesheets.forEach(i => sum += i.hours);
    return sum;
  }

  newTimesheet() {
    const dialogRef = this.dialog.open(AddEditTimesheetComponent, {
      width: '600px',
      data: { teamMember: this.teamMember }
    });

    dialogRef.afterClosed().subscribe(res => {
      this.getData();
    });
  }

  editTimesheet(tm: Timesheet) {
    const dialogRef = this.dialog.open(AddEditTimesheetComponent, {
      width: '600px',
      data: { teamMember: this.teamMember, timesheet: tm }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getData();
    });
  }

  deleteTimesheet(tm: Timesheet) {
    const dialogRef = this.dialog.open(ConfirmDeleteComponent, {
      width: '600px',
      data: {name: `the ${formatDate(tm.clockIn, "shortDate", "en-US")} timesheet record for ${this.teamMember.name}`}
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.api.deleteTimesheet(tm.id).subscribe(res => {
          this.getData();
        });
      }
    });
  }

}
