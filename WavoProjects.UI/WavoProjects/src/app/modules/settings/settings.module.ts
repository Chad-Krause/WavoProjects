import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared.module';
import { MainComponent } from './main/main.component';
import { SettingsRoutingModule } from './settings-routing.module';



@NgModule({
  declarations: [MainComponent],
  imports: [
    CommonModule,
    SettingsRoutingModule,
    SharedModule
  ]
})
export class SettingsModule { }
