import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Message } from 'src/app/models/data/message.model';

@Component({
  selector: 'app-message-container',
  templateUrl: './message-container.component.html',
  styleUrls: ['./message-container.component.css']
})
export class MessageContainerComponent implements OnInit {
  @Input() message: Message;
  @Input() editInfoAccessibility?: boolean;
  @Output() scroll: EventEmitter<number> = new EventEmitter<number>();
  @Output() reply: EventEmitter<number> = new EventEmitter<number>();
  @Output() edit: EventEmitter<{ id: number; message: string }> = new EventEmitter<{
    id: number;
    message: string;
  }>();
  @Output() delete: EventEmitter<number> = new EventEmitter<number>();

  constructor() {}

  ngOnInit(): void {}

  scrollToElement(id: number) {
    if (this.message.replyToName == undefined) return;
    this.scroll.emit(id);
  }

  replyTo(id: number) {
    this.reply.emit(id);
  }

  editMessage(id: number, message: string) {
    this.edit.emit({ id, message });
  }

  deleteMessage(id: number) {
    this.delete.emit(id);
  }
}
