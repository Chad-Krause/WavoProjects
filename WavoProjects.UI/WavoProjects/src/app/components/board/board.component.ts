import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Priority } from 'src/app/models/priority';
import { ExampleProjects, Project } from 'src/app/models/project';
import { ProjectSortOrder } from 'src/app/models/project-sort-order';
import { ApiService } from 'src/app/services/api.service';
import { RealTimeService } from 'src/app/services/real-time.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent implements OnInit, OnDestroy {
  subscriptions: Subscription[] = [];
  priorities: Priority[] = [];

  constructor(private rts: RealTimeService, private api: ApiService) { 
    let sub = rts.projectBoardUpdates.subscribe((newData: Priority[]) => {
      console.log(newData)
      this.priorities = newData;
    });

    this.subscriptions.push(sub);
  }

  ngOnInit(): void {
    this.rts.subscribeToProjectBoardUpdates();
  }
  
  ngOnDestroy(): void {
    this.rts.unsubscribeToProjectBoardUpdates();
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }


  drop(event: CdkDragDrop<Project[]>) {
    // update UI
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }

    let containerId: number = (+event.container.id.substr(14)) % this.priorities.length;
    let priorityId = this.priorities[containerId].id;
    let project = event.container.data[event.currentIndex];
    // Update Backend
    let newProjectsOrder: ProjectSortOrder[] = event.container.data.map((project, index) => new ProjectSortOrder(project.id, index));
    
    this.api.updateProjectPriorityAndSortOrders(project.id, priorityId, newProjectsOrder).subscribe(res => {
      console.log(res);
    });
    
  }
}
