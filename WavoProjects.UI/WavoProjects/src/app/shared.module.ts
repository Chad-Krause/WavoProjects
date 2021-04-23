import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatListModule } from '@angular/material/list'

import { DragDropModule } from '@angular/cdk/drag-drop';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
    declarations: [
    ],
    imports: [
        MatIconModule,
        MatToolbarModule,
        MatButtonModule,
        DragDropModule,
        HttpClientModule,
        MatMenuModule,
        MatDialogModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        FormsModule,
        MatInputModule,
        MatSelectModule,
        MatListModule
    ],
    providers: [],
    bootstrap: [],
    exports: [
        MatIconModule,
        MatToolbarModule,
        MatButtonModule,
        DragDropModule,
        HttpClientModule,
        MatMenuModule,
        MatDialogModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        FormsModule,
        MatInputModule,
        MatSelectModule,
        MatListModule
    ]

  })
  export class SharedModule { }