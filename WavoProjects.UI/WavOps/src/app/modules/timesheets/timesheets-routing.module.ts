import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClockInOutComponent } from './clock-in-out/clock-in-out.component';


const routes: Routes = [
  {
    path: "",
    component: ClockInOutComponent,
  },
  {
    path: "**",
    redirectTo: "",
    pathMatch: "full"
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TimesheetsRoutingModule { }
