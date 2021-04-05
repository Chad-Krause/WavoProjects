import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { ExampleProjects, Project } from 'src/app/models/project';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent implements OnInit {
  projects1: Project[] = ExampleProjects;
  projects2: Project[] = ExampleProjects;
  projects3: Project[] = ExampleProjects;
  projects4: Project[] = ExampleProjects;

  constructor() { }

  ngOnInit(): void {
  }


  drop(event: CdkDragDrop<Project[]>) {
    console.log(event);
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }
  }
}
