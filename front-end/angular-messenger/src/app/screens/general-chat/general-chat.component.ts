import {
  AfterViewChecked,
  Component,
  ElementRef,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren
} from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { Chat } from 'src/app/models/data/chat.model';
import { AppState } from 'src/app/store';
import { ChatActions } from 'src/app/store/actions/chat.actions';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { GeneralChatSelectors } from 'src/app/store/selectors/chat.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-general',
  templateUrl: './general-chat.component.html',
  styleUrls: ['./general-chat.component.css']
})
export class GeneralChatComponent implements OnInit, AfterViewChecked {
  @ViewChild('chatBox') chatBox: ElementRef;
  @ViewChildren('message') messages: QueryList<ElementRef>;
  messageForm: FormControl;
  userId$: Observable<number> = this.store.select(AuthSelectors.selectUserId);
  chat$: Observable<Chat[]> = this.store.select(GeneralChatSelectors.selectChatMessages);
  submitMode: 'send' | 'edit' | 'reply' | 'delete';
  data: { editId?: number; replyId?: number; replyToName?: string; deleteId?: number };
  doScrollToBottom: boolean;

  constructor(private store: Store<AppState>, fb: FormBuilder) {
    this.messageForm = fb.control('', Validators.required);
    this.submitMode = 'send';
    this.data = {};
    this.doScrollToBottom = true;
  }

  ngOnInit(): void {
    this.fetchMessages();
  }

  ngAfterViewChecked(): void {
    if (!this.doScrollToBottom) {
      this.doScrollToBottom = true;
      return;
    }
    this.scrollToBottom();
  }

  fetchMessages() {
    this.store.dispatch(ChatActions.GetChatMessagesRequest());
  }

  configInfo(mode: 'send' | 'edit' | 'reply' | 'delete', data: { id: number; message?: string }) {
    this.data = {};
    this.submitMode = mode;
    this.data[`${mode}Id`] = data.id;
    switch (mode) {
      case 'edit':
        this.messageForm.patchValue(data.message ? data.message : '');
        break;
      case 'reply':
        this.store
          .select(GeneralChatSelectors.selectMessageComposerName, data.id)
          .subscribe((name) => (this.data.replyToName = name));
        this.messageForm.patchValue('');
        break;
      case 'delete':
        this.messageForm.patchValue('');
        break;
      default:
        break;
    }
  }

  onSubmitMessage(): void {
    if (this.messageForm.invalid) {
      return;
    }
    switch (this.submitMode) {
      case 'edit':
        this.editMessage(this.data.editId);
        break;
      case 'reply':
        this.sendMessage(this.messageForm.value, this.data.replyId);
        break;
      default:
        this.sendMessage(this.messageForm.value);
        break;
    }
    this.submitMode = 'send';
    this.messageForm.patchValue('');
  }

  sendMessage(text: string, replyToId?: number) {
    this.store.dispatch(
      ChatActions.SendMessageRequest({
        replyToId,
        text,
        dateTime: new Date().toJSON()
      })
    );
  }

  editMessage(id: number) {
    this.store.dispatch(
      ChatActions.EditMessageRequest({ messageId: id, message: this.messageForm.value })
    );
  }

  deleteMessage(id: number) {
    this.store.dispatch(ChatActions.DeleteMessageRequest({ messageId: id }));
  }

  scrollToBottom(): void {
    this.chatBox.nativeElement.scroll({
      top: this.chatBox.nativeElement.scrollHeight,
      left: 0
    });
  }

  scrollToElement(id: number) {
    let element = this.messages.find((msg) => msg.nativeElement.id == id);
    element.nativeElement.scrollIntoView({ behavior: 'smooth' });
    this.doScrollToBottom = false;
  }
}
