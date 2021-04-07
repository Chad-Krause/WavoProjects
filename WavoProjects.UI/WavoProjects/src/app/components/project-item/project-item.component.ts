import { Component, Input, OnInit } from '@angular/core';
import { Project } from 'src/app/models/project';

@Component({
  selector: 'project-item',
  templateUrl: './project-item.component.html',
  styleUrls: ['./project-item.component.scss']
})
export class ProjectItemComponent implements OnInit {
  @Input() project: Project;

  constructor() { }

  ngOnInit(): void {
  }

  getColor() {
    let backgroundColor = this.project.category.color + "16"
    return {'border-color': this.project.category.color, 'background-color': backgroundColor};
  }

}