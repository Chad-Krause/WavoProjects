import { CdkDragDrop, CdkDragEnd, CdkDragMove, CdkDragStart, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Observable, pipe, Subject, Subscription } from 'rxjs';
import { Priority } from 'src/app/models/priority';
import { ProjectSortOrder } from 'src/app/models/project-sort-order';
import { ApiService } from 'src/app/services/api.service';
import { RealTimeService } from 'src/app/services/real-time.service';
import { throttleTime } from 'rxjs/operators';
import { ExampleProjects, Project } from 'src/app/models/project';
import { ProjectDrag } from 'src/app/models/project-drag';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmCompletionComponent } from '../dialogs/confirm-completion/confirm-completion.component';
import { AddProjectComponent } from '../dialogs/add-project/add-project.component';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent implements OnInit, OnDestroy {
  subscriptions: Subscription[] = [];
  priorities: Priority[] = [];
  onlyProjects: Project[] = [];
  dragSubject: Subject<CdkDragMove> = new Subject<CdkDragMove>();
  dragThrottled: Observable<CdkDragMove> = this.dragSubject.pipe(throttleTime(environment.dragUpdateDelayMS));
  remoteProjectDrags = {};
  exampleProject: Project = ExampleProjects[0];
  screenWidth: number;
  screenHeight: number;

  @HostListener('window:resize', ['$event'])
onResize(event?) {
   this.screenHeight = window.innerHeight;
   this.screenWidth = window.innerWidth;
}

  constructor(private rts: RealTimeService, private api: ApiService, public dialog: MatDialog) { 
    let sub1 = rts.projectBoardUpdates.subscribe((newData: Priority[]) => {
      this.priorities = newData;
      // 1D array of projects
      this.onlyProjects = [].concat(...this.priorities.map(i => i.projects));
    });
    this.subscriptions.push(sub1);


    let sub2 = this.dragThrottled.subscribe(res => {
      this.rts.dragProject(res.pointerPosition.x, res.pointerPosition.y, res.source.data.id);
    });
    this.subscriptions.push(sub2);


    let sub3 = this.rts.projectDrags.subscribe(drag => {
      this.otherUserDragged(drag);
    });
    this.subscriptions.push(sub3);

    this.onResize();
  }

  ngOnInit(): void {
    this.rts.subscribeToProjectBoardUpdates();
    this.rts.subscribeToProjectDrags();
  }
  
  ngOnDestroy(): void {
    this.rts.unsubscribeToProjectBoardUpdates();
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }


  drop(event: CdkDragDrop<Project[]>) {
    let containerId: number = (+event.container.id.substr(14)) % this.priorities.length;
    let priorityId = this.priorities[containerId].id;
    let project = event.item.data;

    if(priorityId == 5) {
      const dialogRef = this.dialog.open(ConfirmCompletionComponent, {
        width: '500px',
        data: { projectName: project.name }
      });
  
      dialogRef.afterClosed().subscribe(confirm => {
        if(confirm) {
          this.moveItems(event, priorityId, project);
        }
      });
    } else {
      this.moveItems(event, priorityId, project);
    }
  }

  moveItems(event: CdkDragDrop<Project[]>, priorityId: number, project: Project) {
    // update UI
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }
    // Update Backend
    let newProjectsOrder: ProjectSortOrder[] = event.container.data.map((project, index) => new ProjectSortOrder(project.id, index));

    this.api.updateProjectPriorityAndSortOrders(project.id, priorityId, newProjectsOrder).subscribe();
  }


  onDragMoved(e: CdkDragMove) {
    e.pointerPosition.x = (e.pointerPosition.x * 100)/this.screenWidth - 12.5;
    e.pointerPosition.y = (e.pointerPosition.y * 100)/this.screenHeight - 12.5;
    this.dragSubject.next(e);
  }

  onDragEnd(e: any) {
    this.rts.dragProject(-100, -100, e.source.data.id);
  }

  otherUserDragged(drag: ProjectDrag) {
    drag.project = this.onlyProjects.find(i => i.id == drag.projectId);
    drag.style = {"top": drag.y+ "%", "left": drag.x + "%", "display": drag.x > -100 || drag.y + 50 > this.screenHeight ? "block" : "none"}
    this.remoteProjectDrags[drag.clientId] = drag;
  }

  newProject() {
    const dialogRef = this.dialog.open(AddProjectComponent, {
      width: '500px'
    });

    dialogRef.afterClosed().subscribe(project => {
    });
  }


}
