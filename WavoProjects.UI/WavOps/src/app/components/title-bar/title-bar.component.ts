import { Component, OnInit } from '@angular/core';
//import { GLOBALS } from '../../../environments/globals';
import { RealTimeService } from '../../services/real-time.service';
import { GLOBALS } from '../../../environments/globals';

@Component({
  selector: 'title-bar',
  templateUrl: './title-bar.component.html',
  styleUrls: ['./title-bar.component.scss']
})
export class TitleBarComponent implements OnInit {

  connected: boolean = false;
  version: string = GLOBALS.version;

  constructor(private rt: RealTimeService) { }

  ngOnInit(): void {
    this.rt.isConnected.subscribe((i: boolean) => this.connected = i);
  }

  addProject() {

  }

}
