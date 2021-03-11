import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Group } from 'src/app/models/data/group.model';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-group-item',
  templateUrl: './group-item.component.html',
  styleUrls: ['./group-item.component.css']
})
export class GroupItemComponent implements OnInit {
  @Input() group?: Group;
  @Input() isCreator?: boolean = false;
  @Output() info: EventEmitter<number> = new EventEmitter<number>();
  @Output() leave: EventEmitter<number> = new EventEmitter<number>();
  @Output() delete: EventEmitter<number> = new EventEmitter<number>();

  constructor() {}

  ngOnInit(): void {}

  groupChat(id: number) {}

  groupInfo(id: number) {
    this.info.emit(id);
  }

  leaveGroup(id: number) {
    this.leave.emit(id);
  }

  deleteGroup(id: number) {
    this.delete.emit(id);
  }
}
