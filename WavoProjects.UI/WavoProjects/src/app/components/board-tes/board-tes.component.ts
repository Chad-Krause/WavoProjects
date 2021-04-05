import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-board-tes',
  templateUrl: './board-tes.component.html',
  styleUrls: ['./board-tes.component.scss']
})
export class BoardTesComponent implements OnInit {
  projects1: string[] = ["Test 1", "Chad", "Kyle", "Mom", "Dad"];
  projects2: string[] = ["Test 1", "Chad", "Kyle", "Mom", "Dad"];
  projects3: string[] = ["Test 1", "Chad", "Kyle", "Mom", "Dad"];
  projects4: string[] = ["Test 1", "Chad", "Kyle", "Mom", "Dad"];
  constructor() { }

  ngOnInit(): void {
  }

  
  drop(event: CdkDragDrop<string[]>) {
    console.log(event);
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }
  }
}
