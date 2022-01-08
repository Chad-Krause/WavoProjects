import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { TeamMember } from 'src/app/models/team-member';
import { TeamMemberTimesheetRow } from 'src/app/models/team-member-timesheet-row';
import { ApiService } from 'src/app/services/api.service';
import { ConfirmDeleteComponent } from '../../project-board/dialogs/confirm-delete/confirm-delete.component';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { AddEditTeamMemberComponent } from '../dialogs/add-edit-team-member/add-edit-team-member.component';
import { ChangePinComponent } from '../dialogs/change-pin/change-pin.component';


@Component({
  selector: 'app-edit-team-members',
  templateUrl: './edit-team-members.component.html',
  styleUrls: ['./edit-team-members.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class EditTeamMembersComponent implements OnInit {

  @ViewChild(MatSort) sort: MatSort;
  teamMembers: TeamMemberTimesheetRow[] = [];
  displayedColumns: string[] = ['name', 'trackTime', 'hours', 'distinctDays', 'action'];
  ds: MatTableDataSource<TeamMemberTimesheetRow> = new MatTableDataSource<TeamMemberTimesheetRow>();
  expandedElement: TeamMemberTimesheetRow | null;

  constructor(private api: ApiService, public dialog: MatDialog) { }

  ngOnInit(): void {
    //this.ds.sortingDataAccessor = (obj, property) => this.getProperty(obj, property);
    this.getData();
  }

  ngAfterViewInit() {
    this.ds.sort = this.sort;
  }

  getData() {
    this.api.getTeamMembersTimesheetData().subscribe(res => {
      this.teamMembers = res.map(i => new TeamMemberTimesheetRow(i));
      this.ds.data = this.teamMembers;
    });
  }

  newTeamMember() {
    const dialogRef = this.dialog.open(AddEditTeamMemberComponent, {
      width: '500px'
    });

    dialogRef.afterClosed().subscribe(res => {
      this.getData();
    });
  }

  editTeamMember(tm: TeamMemberTimesheetRow) {
    const dialogRef = this.dialog.open(AddEditTeamMemberComponent, {
      width: '500px',
      data: tm
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getData();
    });
  }

  deleteTeamMember(tm: TeamMemberTimesheetRow) {
    const dialogRef = this.dialog.open(ConfirmDeleteComponent, {
      width: '500px',
      data: {name: tm.name}
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.api.deleteTeamMember(tm.id).subscribe(res => {
          this.getData();
        });
      }
    });
  }

  changePin(tm: TeamMemberTimesheetRow) {
    const dialogRef = this.dialog.open(ChangePinComponent, {
      width: '500px',
      data: tm.id
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getData();
    });
  }
}
