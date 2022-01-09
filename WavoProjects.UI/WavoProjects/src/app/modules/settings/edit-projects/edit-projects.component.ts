import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Project } from 'src/app/models/project';
import { ApiService } from 'src/app/services/api.service';
import { AddProjectComponent } from '../../project-board/dialogs/add-project/add-project.component';
import { ConfirmDeleteComponent } from '../../project-board/dialogs/confirm-delete/confirm-delete.component';

@Component({
  selector: 'app-edit-projects',
  templateUrl: './edit-projects.component.html',
  styleUrls: ['./edit-projects.component.scss']
})
export class EditProjectsComponent implements OnInit, AfterViewInit {
  @ViewChild(MatSort) sort: MatSort;

  projects: Project[] = [];
  displayedColumns: string[] = ['name', 'description', 'team.name', 'projectOwner.name', 'action'];
  ds: MatTableDataSource<Project> = new MatTableDataSource<Project>();

  constructor(private api: ApiService, public dialog: MatDialog) { }

  ngOnInit(): void {

    this.ds.sortingDataAccessor = (obj, property) => this.getProperty(obj, property);
    this.getData();
  }

  ngAfterViewInit() {
    this.ds.sort = this.sort;
  }

  getData() {
    this.api.getProjects().subscribe(results => {
      this.projects = results.map(i => new Project(i));
      this.ds.data = this.projects;
    });
  }

  editProject(project: Project) {
    const dialogRef = this.dialog.open(AddProjectComponent, {
      width: '500px',
      data: project
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getData();
    });
  }

  deleteProject(p: Project) {
    const dialogRef = this.dialog.open(ConfirmDeleteComponent, {
      width: '500px',
      data: {name: p.name}
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result) { // Delete project
        this.api.deleteProject(p.id).subscribe(res => {
          this.getData();
        });
      }
    });
  }

  newProject() {
    const dialogRef = this.dialog.open(AddProjectComponent, {
      width: '500px'
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getData();
    });
  }

  getProperty = (obj, path) => (
    path.split('.').reduce((o, p) => o && o[p], obj)
  )

}
