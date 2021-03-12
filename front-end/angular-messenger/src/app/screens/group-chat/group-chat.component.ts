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
import { Group } from 'src/app/models/data/group.model';
import { AppState } from 'src/app/store';
import { GroupActions } from 'src/app/store/actions/group.actions';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { GroupSelectors } from 'src/app/store/selectors/group.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-group-chat',
  templateUrl: './group-chat.component.html',
  styleUrls: ['./group-chat.component.css']
})
export class GroupChatComponent implements OnInit, AfterViewChecked {
  @ViewChild('chatBox') chatBox: ElementRef;
  @ViewChildren('message') messages: QueryList<ElementRef>;
  messageForm: FormControl;
  groupParam: Group = this.getGroupParams();

  userId$: Observable<number> = this.store.select(AuthSelectors.selectUserId);
  chat$: Observable<Chat[]> = this.store.select(GroupSelectors.selectGroupMessages, {
    groupId: this.groupParam.groupId
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

  getGroupParams(): Group {
    let params = this.route.snapshot.paramMap;
    return {
      groupId: Number(params.get('id')),
      groupName: params.get('name'),
      creatorId: Number(params.get('creator'))
    };
  }

  ngOnInit(): void {
    this.fetchMessages(this.groupParam.groupId);
  }

  ngAfterViewChecked(): void {
    if (!this.doScrollToBottom) {
      this.doScrollToBottom = true;
      return;
    }
    this.scrollToBottom();
  }

  fetchMessages(groupId: number): void {
    if (groupId == undefined) return;
    this.store.dispatch(GroupActions.GetGroupMessagesRequest({ groupId }));
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
      GroupActions.SendGroupMessageRequest({
        groupId: this.groupParam.groupId,
        replyToId,
        text,
        dateTime: new Date().toJSON()
      })
    );
  }

  editMessage(id: number) {
    this.store.dispatch(
      GroupActions.EditGroupMessageRequest({
        groupId: this.groupParam.groupId,
        messageId: id,
        message: this.messageForm.value
      })
    );
  }

  deleteMessage(id: number) {
    this.store.dispatch(
      GroupActions.DeleteGroupMessageRequest({ groupId: this.groupParam.groupId, messageId: id })
    );
  }

  clearHistory() {
    this.store.dispatch(
      GroupActions.ClearGroupChatHistoryRequest({ groupId: this.groupParam.groupId })
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
