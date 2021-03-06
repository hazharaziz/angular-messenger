import {
  AfterViewChecked,
  Component,
  ElementRef,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren
} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Chat } from 'src/app/models/data/chat.model';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-general',
  templateUrl: './general.component.html',
  styleUrls: ['./general.component.css']
})
export class GeneralComponent implements OnInit, AfterViewChecked {
  messageForm: FormControl;
  chat: Chat[];
  @ViewChild('chatBox') chatBox: ElementRef;
  @ViewChildren('message') messages: QueryList<ElementRef>;
  doScrollToBottom: boolean;

  constructor(fb: FormBuilder) {
    this.messageForm = fb.control('', Validators.required);
    this.doScrollToBottom = true;
  }

  ngOnInit(): void {
    this.chat = [
      {
        date: '02-02-2020',
        messages: [
          {
            id: 4,
            composerId: 1,
            composerName: 'hazhar',
            dateTime: '12:32',
            text: 'hello'
          },
          {
            id: 1,
            composerId: 1,
            composerName: 'hazhar',
            dateTime: '12:32',
            text: 'hello'
          },
          {
            id: 1,
            composerId: 1,
            composerName: 'hazhar',
            dateTime: '12:32',
            text: 'hello'
          },
          {
            id: 1,
            composerId: 1,
            composerName: 'hazhar',
            dateTime: '12:32',
            text: 'hello'
          },
          {
            id: 1,
            composerId: 1,
            composerName: 'hazhar',
            dateTime: '12:32',
            text: 'hello'
          },
          {
            id: 1,
            composerId: 1,
            composerName: 'hazhar',
            replyToId: 4,
            replyToName: 'hazhar',
            dateTime: '12:32',
            text: 'hello'
          },
          {
            id: 1,
            composerId: 1,
            composerName: 'hazhar',
            dateTime: '12:32',
            text: 'hello'
          },
          {
            id: 1,
            composerId: 1,
            composerName: 'hazhar',
            dateTime: '12:32',
            text: 'hello'
          },
          {
            id: 1,
            composerId: 1,
            composerName: 'hazhar',
            dateTime: '12:32',
            text: 'hello'
          },
          {
            id: 1,
            composerId: 1,
            composerName: 'hazhar',
            dateTime: '12:32',
            text: 'hello'
          },
          {
            id: 1,
            composerId: 1,
            composerName: 'hazhar',
            dateTime: '12:32',
            text: 'hello'
          },
          {
            id: 1,
            composerId: 1,
            composerName: 'hazhar',
            dateTime: '12:32',
            text: 'hello'
          }
        ]
      }
    ];
  }

  ngAfterViewChecked(): void {
    if (!this.doScrollToBottom) {
      this.doScrollToBottom = true;
      return;
    }
    this.scrollToBottom();
  }
  onSendMessage(): void {
    if (this.messageForm.invalid) {
      return;
    }
  }

  scrollToBottom(): void {
    this.chatBox.nativeElement.scroll({
      top: this.chatBox.nativeElement.scrollHeight,
      left: 0,
      behavior: 'smooth'
    });
  }

  scrollToElement(id: number) {
    let element = this.messages.find((msg) => msg.nativeElement.id == id);
    element.nativeElement.scrollIntoView({ behavior: 'smooth' });
    this.doScrollToBottom = false;
  }
}
