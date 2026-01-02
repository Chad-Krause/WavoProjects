import { Component } from '@angular/core';
import { RealTimeService } from './services/real-time.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'WavoProjects';
  constructor(private rts: RealTimeService) {
    rts.subscribeToProjectDrags()
  }
}
