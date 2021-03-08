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
import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';
import { ChatActions } from 'src/app/store/actions/chat.actions';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { ChatSelectors } from 'src/app/store/selectors/chat.selectors';

@Component({
  selector: 'app-general',
  templateUrl: './general.component.html',
  styleUrls: ['./general.component.css']
})
export class GeneralComponent implements OnInit, AfterViewChecked {
  @ViewChild('chatBox') chatBox: ElementRef;
  @ViewChildren('message') messages: QueryList<ElementRef>;
  messageForm: FormControl;
  user$: Observable<User> = this.store.select(AuthSelectors.selectUser);
  chat$: Observable<Chat[]> = this.store.select(ChatSelectors.selectChatMessages);
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
          .select(ChatSelectors.selectMessageComposerName, data.id)
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
      case 'delete':
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
    this.store.dispatch(ChatActions.EditMessageRequest({ id, text: this.messageForm.value }));
  }

  deleteMessage(id: number) {}

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
