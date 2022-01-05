import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared.module';
import { MainComponent } from './main/main.component';
import { SettingsRoutingModule } from './settings-routing.module';
import { MatTableModule } from '@angular/material/table';
import { EditProjectsComponent } from './edit-projects/edit-projects.component';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [MainComponent, EditProjectsComponent],
  imports: [
    MatTableModule,
    CommonModule,
    SettingsRoutingModule,
    SharedModule,
    MatTableModule,
    MatSortModule
  ]
})
export class SettingsModule { }
