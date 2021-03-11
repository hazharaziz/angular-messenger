import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Direct } from 'src/app/models/data/direct.model';

@Component({
  selector: 'app-direct-item',
  templateUrl: './direct-item.component.html',
  styleUrls: ['./direct-item.component.css']
})
export class DirectItemComponent implements OnInit {
  @Input() direct?: Direct;
  @Output() chat: EventEmitter<{ id: number; name: string }> = new EventEmitter<{
    id: number;
    name: string;
  }>();
  @Output() delete: EventEmitter<number> = new EventEmitter<number>();

  constructor() {}

  ngOnInit(): void {}

  directChat(id: number, name: string) {
    this.chat.emit({ id, name });
  }

  deleteDirect(id: number) {
    this.delete.emit(id);
  }
}
