import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TitleBarComponent } from './components/title-bar/title-bar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProjectBoardModule } from './modules/project-board/project-board.module';
import { SharedModule } from './shared.module';
import { SettingsModule } from './modules/settings/settings.module';

@NgModule({
  declarations: [
    AppComponent,
    TitleBarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ProjectBoardModule,
    SettingsModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
