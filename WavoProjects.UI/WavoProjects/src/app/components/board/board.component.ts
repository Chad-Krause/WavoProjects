import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Priority } from 'src/app/models/priority';
import { ExampleProjects, Project } from 'src/app/models/project';
import { RealTimeService } from 'src/app/services/real-time.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent implements OnInit, OnDestroy {
  subscriptions: Subscription[] = [];
  priorities: Priority[] = [];

  constructor(private rts: RealTimeService) { 
    let sub = rts.projectBoardUpdates.subscribe((newData: Priority[]) => {
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
