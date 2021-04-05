import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BoardTesComponent } from './components/board-tes/board-tes.component';
import { BoardComponent } from './components/board/board.component';


const routes: Routes = [
  {
    path: "board",
    component: BoardComponent
  },
  {
    path: "test",
    component: BoardTesComponent
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
