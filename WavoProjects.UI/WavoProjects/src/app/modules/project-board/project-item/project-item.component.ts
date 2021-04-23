import { Component, Input, OnInit } from '@angular/core';
import { Project } from 'src/app/models/project';
import { Team } from 'src/app/models/team';

@Component({
  selector: 'project-item',
  templateUrl: './project-item.component.html',
  styleUrls: ['./project-item.component.scss']
})
export class ProjectItemComponent implements OnInit {
  @Input() project: Project;

  constructor() { }

  ngOnInit(): void {
    this.project.team = new Team(this.project.team);
  }

  getColor() {
    return {'border-color': this.project.team.color, 'background-color': this.project.team.backgroundColor};
  }

}
