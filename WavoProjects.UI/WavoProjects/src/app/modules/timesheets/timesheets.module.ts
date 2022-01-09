import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClockInOutComponent } from './clock-in-out/clock-in-out.component';
import { TimesheetsRoutingModule } from './timesheets-routing.module';
import { SharedModule } from 'src/app/shared.module';



@NgModule({
  declarations: [ClockInOutComponent],
  imports: [
    CommonModule,
    TimesheetsRoutingModule,
    SharedModule
  ]
})
export class TimesheetsModule { }
