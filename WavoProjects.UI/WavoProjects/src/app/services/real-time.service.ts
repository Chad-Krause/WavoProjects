import { EventEmitter, Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import * as signalR from "@microsoft/signalr";
import { environment } from 'src/environments/environment';
import { GLOBALS } from 'src/environments/globals';
import { Priority } from '../models/priority';
import { ProjectDrag } from '../models/project-drag';
import { TimesheetTeamMember } from '../models/timesheet-team-member';

@Injectable({
  providedIn: 'root'
})
export class RealTimeService {
  private connection: signalR.HubConnection;
  private state: RealTimeSignalRState = new RealTimeSignalRState();
  public isCurrentlyConnected: boolean;
  public isConnected: EventEmitter<boolean> = new EventEmitter<boolean>();
  public projectBoardUpdates: EventEmitter<Priority[]> = new EventEmitter<Priority[]>();
  public projectDrags: EventEmitter<ProjectDrag> = new EventEmitter<ProjectDrag>();
  public timesheetUpdates: EventEmitter<TimesheetTeamMember[]> = new EventEmitter<TimesheetTeamMember[]>();

  private disconnectedSince: Date = new Date();

  constructor(private _snackBar: MatSnackBar) { 
    this.connection = new signalR.HubConnectionBuilder()
                                  .withUrl(environment.hubUrl)
                                  .withAutomaticReconnect()
                                  .build();

    this.connection.onreconnecting(() => this.isConnected.emit(false));
    this.connection.onclose(() => this.isConnected.emit(false));
    this.connection.onreconnected(() => this.isConnected.emit(true));

    this.isConnected.subscribe(connected => {
      this.isCurrentlyConnected = connected;

      if(connected) {
        if(this.state.isListeningToProjectBoardUpdates) {
          this.subscribeToProjectBoardUpdates();
        }

        if(this.state.isListeningToProjectDrags) {
          this.subscribeToProjectDrags();
        }

        if(this.state.isListeningToTimesheetUpdates) {
          this.subscribeToTimesheetUpdates();
        }
      } else {
        this.disconnectedSince = new Date();
      }
    })

    this.connection.on("UpdateProjectBoard", (data: Priority[]) => {
      this.projectBoardUpdates.emit(data);
    });

    this.connection.on("ProjectDrag", (data: ProjectDrag) => {
      this.projectDrags.emit(data);
    });

    this.connection.on("UpdateTimesheets", (data: TimesheetTeamMember[]) => {
      this.timesheetUpdates.emit(data);
    });

    this.connection.on("Refresh", () => {
      location.reload();
    });

    this.startConnection()

    /**
     * Check to see if we a connected. If not, reconnect. If disconnected for too long, do a refresh
     */
    setInterval(() => {
      if(!this.isCurrentlyConnected){

        let currentTime = new Date();
        if(currentTime.getTime() - this.disconnectedSince.getTime() > GLOBALS.refreshIfDisconnectedForMS) {
          console.error("Disconnected from the server for too long. Refreshing...");
          _snackBar.open("Disconnected from the server for too long. Refreshing...");
          setTimeout(() => {
            location.reload();
          }, 5000)
        }

        this.startConnection();
      }
    }, 15000)
  }

  public refresh() {
    this.connection.invoke("GlobalRefresh");
  }

  public startConnection() {
    this.connection.start()
    .then(i => this.isConnected.emit(true))
    .catch(i => {
      this.isConnected.emit(false)
    });
  }

  //#region Project Board Updates
  public subscribeToProjectBoardUpdates() {
    this.state.isListeningToProjectBoardUpdates = true;

    if(this.isCurrentlyConnected) {
      this.connection.send("SubscribeToProjectPage");
    }
  }

  public unsubscribeToProjectBoardUpdates() {
    this.state.isListeningToProjectBoardUpdates = false;

    if(this.isCurrentlyConnected) {
      this.connection.send("UnsubscribeToProjectPage");
    }
  }
  //#endregion

  //#region Drag Updates
  public subscribeToProjectDrags() {
    this.state.isListeningToProjectDrags = true;

    if(this.isCurrentlyConnected) {
      this.connection.send("SubscribeToProjectDrags");
    }
  }

  public unsubscribeToProjectDrags() {
    this.state.isListeningToProjectDrags = false;

    if(this.isCurrentlyConnected) {
      this.connection.send("UnsubscribeToProjectDrags");
    }
  }

  public dragProject(x: number, y: number, projectId: number) {
    this.connection.send("DragProject", {x, y, projectId});
  }
  //#endregion

  //#region Timesheets
  public subscribeToTimesheetUpdates() {
    this.state.isListeningToTimesheetUpdates = true;

    if(this.isCurrentlyConnected) {
      this.connection.send("SubscribeToTimesheetUpdates");
    }
  }

  public unsubscribeToTimesheetUpdates() {
    this.state.isListeningToTimesheetUpdates = false;

    if(this.isCurrentlyConnected) {
      this.connection.send("UnsubscribeFromTimesheetUpdates");
    }
  }
  //#endregion
}

/**
 * Class for Retrying. Normal SignalR Retry policy will quit after 4 attempts
 * Since this is a screen that will most likely always be open, it should always try
 * and reconnect
 */
 export class UnlimitedRetryPolicy implements signalR.IRetryPolicy {

  MAX_RETRY_DELAY: number = 30000;

  UnlimitedRetryPolicy(maxDelayMilliseconds: number) {
    if(maxDelayMilliseconds > 1) {
      this.MAX_RETRY_DELAY = maxDelayMilliseconds;
    }
  }
  nextRetryDelayInMilliseconds(retryContext: signalR.RetryContext): number {
    let delay: number = retryContext.previousRetryCount * 2 * 1000;
    return delay > this.MAX_RETRY_DELAY ? this.MAX_RETRY_DELAY : delay;
  }

}

class RealTimeSignalRState {
  constructor() {
    this.isListeningToProjectBoardUpdates = false;
    this.isListeningToProjectDrags = false;
    this.isListeningToProjectBoardUpdates = false;
  }

  isListeningToProjectBoardUpdates: boolean;
  isListeningToProjectDrags: boolean;
  isListeningToTimesheetUpdates: boolean;
}

