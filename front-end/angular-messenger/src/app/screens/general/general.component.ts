import {
  AfterViewChecked,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren
} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Chat } from 'src/app/models/data/chat.model';
import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';
import { ChatActions } from 'src/app/store/actions/chat.actions';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { ChatSelectors } from 'src/app/store/selectors/chat.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-general',
  templateUrl: './general.component.html',
  styleUrls: ['./general.component.css']
})
export class GeneralComponent implements OnInit, AfterViewChecked, OnDestroy {
  @ViewChild('chatBox') chatBox: ElementRef;
  @ViewChildren('message') messages: QueryList<ElementRef>;
  messageForm: FormControl;
  user$: Observable<User> = this.store.select(AuthSelectors.selectUser);
  chat$: Observable<Chat[]> = this.store.select(ChatSelectors.selectChatMessages);
  doScrollToBottom: boolean;

  constructor(private store: Store<AppState>, fb: FormBuilder) {
    this.messageForm = fb.control('', Validators.required);
    this.doScrollToBottom = true;
  }

  ngOnInit(): void {
    log('general component init');
    this.fetchMessages();
  }

  ngAfterViewChecked(): void {
    if (!this.doScrollToBottom) {
      this.doScrollToBottom = true;
      return;
    }
    this.scrollToBottom();
  }

  ngOnDestroy(): void {}

  fetchMessages() {
    this.store.select(AuthSelectors.selectToken).subscribe((token) => {
      log(token);
      log('dispatch action');
      this.store.dispatch(ChatActions.GetChatMessagesRequest({ token }));
    });
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
