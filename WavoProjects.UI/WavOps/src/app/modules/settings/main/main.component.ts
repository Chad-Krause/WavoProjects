import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { RealTimeService } from 'src/app/services/real-time.service';
import { GLOBALS } from '../../../environments/globals';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  uiVersion: string = GLOBALS.version;
  apiVersion: string = "";

  constructor(private api: ApiService, private rt: RealTimeService) { }

  ngOnInit(): void {
    this.api.getAPIVersion().subscribe(res => {
      this.apiVersion = res.versionString;
      console.log(`Version ${this.apiVersion}`);
    });
  }

  refresh() {
    this.rt.refresh();
  }

}
