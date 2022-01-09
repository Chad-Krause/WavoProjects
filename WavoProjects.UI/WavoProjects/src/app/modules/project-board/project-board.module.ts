import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectItemComponent } from './project-item/project-item.component';
import { BoardComponent } from './board/board.component';
import { AddProjectComponent } from './dialogs/add-project/add-project.component';
import { ConfirmCompletionComponent } from './dialogs/confirm-completion/confirm-completion.component';
import { SharedModule } from 'src/app/shared.module';
import { ConfirmDeleteComponent } from './dialogs/confirm-delete/confirm-delete.component';


@NgModule({
  declarations: [
    ProjectItemComponent,
    BoardComponent,
    AddProjectComponent,
    ConfirmCompletionComponent,
    ConfirmDeleteComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [ProjectItemComponent]
})
export class ProjectBoardModule { }
