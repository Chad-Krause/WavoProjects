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
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTableModule } from '@angular/material/table';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { DragDropModule } from '@angular/cdk/drag-drop';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSortModule } from '@angular/material/sort';
import { NgxColorsModule } from 'ngx-colors';



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
        MatListModule,
        MatTooltipModule,
        NgxColorsModule,
        MatSlideToggleModule,
        MatSnackBarModule
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
        MatListModule,
        MatTooltipModule,
        NgxColorsModule,
        MatSlideToggleModule,
        MatSnackBarModule
    ]

  })
  export class SharedModule { }
