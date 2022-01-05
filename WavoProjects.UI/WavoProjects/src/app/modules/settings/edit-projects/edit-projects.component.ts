import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Project } from 'src/app/models/project';
import { ApiService } from 'src/app/services/api.service';

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

  constructor(private api: ApiService) { }

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

  editProject(p: Project) {
    console.log(p);
  }

  deleteProject(p: Project) {

  }

  getProperty = (obj, path) => (
    path.split('.').reduce((o, p) => o && o[p], obj)
  )

}
