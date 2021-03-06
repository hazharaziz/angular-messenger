import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Message } from 'src/app/models/data/message.model';

@Component({
  selector: 'app-message-container',
  templateUrl: './message-container.component.html',
  styleUrls: ['./message-container.component.css']
})
export class MessageContainerComponent implements OnInit {
  @Input() message: Message;
  @Output() scroll: EventEmitter<number> = new EventEmitter<number>();

  constructor() {}

  ngOnInit(): void {}

  scrollToElement(id: number) {
    this.scroll.emit(id);
  }
}
