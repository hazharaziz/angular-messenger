import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { concatMap, map, catchError } from 'rxjs/operators';

import { Chat } from 'src/app/models/data/chat.model';
import { GeneralChatService } from 'src/app/services/api/chat-service/general-chat.service';
import { ChatActions } from 'src/app/store/actions/chat.actions';
import { log } from 'src/app/utils/logger';
import { MessageMapper } from 'src/app/utils/message-mapper';

@Injectable()
export class GetGeneralChatMessagesEffects {
  getGeneralChatMessagesRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ChatActions.GetChatMessagesRequest),
      concatMap(() => {
        return this.chatService.getGeneralChatMessages().pipe(
          map((response) => {
            let result: Chat[] = MessageMapper.mapMessaegesToChat(response);
            return ChatActions.GetChatMessagesSuccess({ data: result });
          }),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(ChatActions.GetChatMessagesFail({ error: error.error }));
          })
        );
      })
    )
  );

  constructor(private actions$: Actions, private chatService: GeneralChatService) {}
}
