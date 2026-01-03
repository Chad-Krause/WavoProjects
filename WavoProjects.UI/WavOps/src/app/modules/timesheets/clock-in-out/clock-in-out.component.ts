import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { TimesheetTeamMember } from '../../../models/timesheet-team-member';
import { ApiService } from '../../../services/api.service';
import { RealTimeService } from '../../../services/real-time.service';

@Component({
  selector: 'app-clock-in-out',
  templateUrl: './clock-in-out.component.html',
  styleUrls: ['./clock-in-out.component.scss']
})
export class ClockInOutComponent implements OnInit, OnDestroy {
  subscriptions: Subscription[] = [];
  here: TimesheetTeamMember[] = [];
  notHere: TimesheetTeamMember[] = [];

  constructor(private rts: RealTimeService, private api: ApiService) {
    let sub1 = rts.timesheetUpdates.subscribe((newData: TimesheetTeamMember[]) => {
      this.here = newData.filter(i => i.clockedIn);
      this.notHere = newData.filter(i => !i.clockedIn);
    });
    this.subscriptions.push(sub1);
  }

  ngOnInit(): void {
    this.rts.subscribeToTimesheetUpdates();
  }
  
  ngOnDestroy(): void {
    this.rts.unsubscribeToTimesheetUpdates();
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  drop(event: CdkDragDrop<TimesheetTeamMember[]>) {
    
    let teamMember: TimesheetTeamMember = event.previousContainer.data[event.previousIndex];
    console.log(event, teamMember);

    // update UI
    if (event.previousContainer === event.container) {
      if(event.previousIndex === event.currentIndex) { // User probably wanted to clock in/out by just clicking once
        
        if(teamMember.clockedIn) {
          this.api.clockOut(event.item.data.id).subscribe();
        } else {
          this.api.clockIn(event.item.data.id).subscribe();
        }

      } else {
        moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
      }
    } else {
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);

      if(teamMember.clockedIn) {
        this.api.clockOut(teamMember.id).subscribe();
      } else {
        this.api.clockIn(teamMember.id).subscribe();
      }
    }
  }


}
