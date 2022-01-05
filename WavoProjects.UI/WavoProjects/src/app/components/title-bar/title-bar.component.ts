import { Component, OnInit } from '@angular/core';
import { RealTimeService } from 'src/app/services/real-time.service';

@Component({
  selector: 'title-bar',
  templateUrl: './title-bar.component.html',
  styleUrls: ['./title-bar.component.scss']
})
export class TitleBarComponent implements OnInit {

  connected: boolean = false;

  constructor(private rt: RealTimeService) { }

  ngOnInit(): void {
    this.rt.isConnected.subscribe(i => this.connected = i);
  }

  addProject() {

  }

}
