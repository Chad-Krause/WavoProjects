import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectItemComponent } from './project-item/project-item.component';
import { BoardComponent } from './board/board.component';
import { AddProjectComponent } from './dialogs/add-project/add-project.component';
import { ConfirmCompletionComponent } from './dialogs/confirm-completion/confirm-completion.component';
import { SharedModule } from 'src/app/shared.module';


@NgModule({
  declarations: [
    ProjectItemComponent,
    BoardComponent,
    AddProjectComponent,
    ConfirmCompletionComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class ProjectBoardModule { }
