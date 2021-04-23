import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BoardComponent } from './modules/project-board/board/board.component';


const routes: Routes = [
  {
    path: "board",
    component: BoardComponent
  },
  {
    path: "settings",
    loadChildren: () => import('./modules/settings/settings.module').then(m => m.SettingsModule)
  },
  {
    path: "**",
    redirectTo: "/board",
    pathMatch: "full"
  },
  {
    path: "",
    redirectTo: "/board",
    pathMatch: "full"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
