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
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { Chat } from 'src/app/models/data/chat.model';
import { Direct } from 'src/app/models/data/direct.model';
import { AppState } from 'src/app/store';
import { DirectActions } from 'src/app/store/actions/direct.actions';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { DirectSelectors } from 'src/app/store/selectors/direct.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-direct-chat',
  templateUrl: './direct-chat.component.html',
  styleUrls: ['./direct-chat.component.css']
})
export class DirectChatComponent implements OnInit, AfterViewChecked {
  @ViewChild('chatBox') chatBox: ElementRef;
  @ViewChildren('message') messages: QueryList<ElementRef>;
  messageForm: FormControl;
  directParam: Direct = this.getDirectParams();
  userId$: Observable<number> = this.store.select(AuthSelectors.selectUserId);
  chat$: Observable<Chat[]> = this.store.select(DirectSelectors.selectDirectMessages, {
    targetId: this.directParam.targetId
  });
  submitMode: 'send' | 'edit' | 'reply' | 'delete';
  data: { editId?: number; replyId?: number; replyToName?: string; deleteId?: number };
  doScrollToBottom: boolean;

  constructor(
    private store: Store<AppState>,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) {
    this.messageForm = this.fb.control('', Validators.required);
    this.submitMode = 'send';
    this.data = {};
    this.doScrollToBottom = true;
  }

  getDirectParams(): Direct {
    let params = this.route.snapshot.paramMap;
    return {
      targetId: Number(params.get('target')),
      targetName: params.get('name')
    };
  }

  ngOnInit(): void {
    this.fetchMessages(this.directParam.targetId);
  }

  ngAfterViewChecked(): void {
    if (!this.doScrollToBottom) {
      this.doScrollToBottom = true;
      return;
    }
    this.scrollToBottom();
  }

  fetchMessages(targetId: number): void {
    if (targetId == undefined) return;
    this.store.dispatch(DirectActions.GetDirectMessagesRequest({ targetId }));
  }

  configInfo(
    mode: 'send' | 'edit' | 'reply' | 'delete',
    data: { id: number; message?: string; name?: string }
  ) {
    this.data = {};
    this.submitMode = mode;
    this.data[`${mode}Id`] = data.id;
    switch (mode) {
      case 'edit':
        this.messageForm.patchValue(data.message ? data.message : '');
        break;
      case 'reply':
        this.data.replyToName = data.name;
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
      DirectActions.SendDirectMessageRequest({
        targetId: this.directParam.targetId,
        replyToId,
        text,
        dateTime: new Date().toJSON()
      })
    );
    setTimeout(() => {
      this.store.dispatch(
        DirectActions.GetDirectMessagesRequest({ targetId: this.directParam.targetId })
      );
    }, 100);
  }

  editMessage(id: number) {
    this.store.dispatch(
      DirectActions.EditDirectMessageRequest({
        targetId: this.directParam.targetId,
        id: id,
        text: this.messageForm.value
      })
    );
  }

  deleteMessage(id: number) {
    this.store.dispatch(
      DirectActions.DeleteDirectMessageRequest({
        targetId: this.directParam.targetId,
        messageId: id
      })
    );
  }

  clearHistory() {
    this.store.dispatch(
      DirectActions.ClearDirectChatHistoryRequest({ targetId: this.directParam.targetId })
    );
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
