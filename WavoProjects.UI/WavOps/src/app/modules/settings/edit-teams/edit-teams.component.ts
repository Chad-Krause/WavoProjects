import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ConfirmDeleteComponent } from '../../project-board/dialogs/confirm-delete/confirm-delete.component';
import { AddEditTeamComponent } from '../dialogs/add-edit-team/add-edit-team.component';
import { Team } from '../../../models/team';
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'app-edit-teams',
  templateUrl: './edit-teams.component.html',
  styleUrls: ['./edit-teams.component.scss']
})
export class EditTeamsComponent implements OnInit, AfterViewInit {
  @ViewChild(MatSort) sort!: MatSort;
  teams: Team[] = [];
  displayedColumns: string[] = ['name', 'color', 'action'];
  ds: MatTableDataSource<Team> = new MatTableDataSource<Team>();

  constructor(private api: ApiService, public dialog: MatDialog) { }

  ngOnInit(): void {
    //this.ds.sortingDataAccessor = (obj, property) => this.getProperty(obj, property);
    this.getData();
  }

  ngAfterViewInit() {
    this.ds.sort = this.sort;
  }

  getData() {
    this.api.getTeams().subscribe(res => {
      this.teams = res.map(i => new Team(i));
      this.ds.data = this.teams;
    });
  }

  newTeam() {
    const dialogRef = this.dialog.open(AddEditTeamComponent, {
      width: '500px'
    });

    dialogRef.afterClosed().subscribe(res => {
      this.getData();
    });
  }

  editTeam(team: Team) {
    const dialogRef = this.dialog.open(AddEditTeamComponent, {
      width: '500px',
      data: team
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getData();
    });
  }

  deleteTeam(team: Team) {
    const dialogRef = this.dialog.open(ConfirmDeleteComponent, {
      width: '500px',
      data: {name: team.name}
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) { // Delete project
        this.api.deleteTeam(team.id).subscribe(res => {
          this.getData();
        });
      }
    });
  }

  getColor(team: Team) {
    return {'border-color': team.color, 'background-color': team.backgroundColor};
  }



}
