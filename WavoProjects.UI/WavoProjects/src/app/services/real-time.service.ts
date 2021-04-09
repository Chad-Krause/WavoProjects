import { EventEmitter, Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { environment } from 'src/environments/environment';
import { Priority } from '../models/priority';

@Injectable({
  providedIn: 'root'
})
export class RealTimeService {
  private connection: signalR.HubConnection;
  private state: RealTimeSignalRState = new RealTimeSignalRState();
  public isCurrentlyConnected: boolean;
  public projectBoardUpdates: EventEmitter<Priority[]> = new EventEmitter<Priority[]>();

  constructor() { 
    this.connection = new signalR.HubConnectionBuilder()
                                  .withUrl(environment.hubUrl)
                                  .withAutomaticReconnect()
                                  .build();

    this.connection.onreconnecting(() => this.isCurrentlyConnected = false);
    this.connection.onclose(() => this.isCurrentlyConnected = false);
    this.connection.onreconnected(() => {
      this.isCurrentlyConnected = true;

      if(this.state.isListeningToProjectBoardUpdates) {

      }
    });

    this.connection.on("UpdateProjectBoard", (data: Priority[]) => {
      this.projectBoardUpdates.emit(data);
    });

    this.connection.start();
  }

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
  }

  isListeningToProjectBoardUpdates: boolean;
}

