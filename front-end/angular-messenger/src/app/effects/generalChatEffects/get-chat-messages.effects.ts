import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { concatMap, map, catchError, tap } from 'rxjs/operators';
import { Chat } from 'src/app/models/data/chat.model';

import { GeneralChatService } from 'src/app/services/chatService/general-chat.service';
import { ChatActions } from 'src/app/store/actions/chat.actions';
import { log } from 'src/app/utils/logger';
import { MessageMapper } from 'src/app/utils/messageMapper';
import { Messages } from 'src/assets/common/strings';

@Injectable()
export class GetChatMessagesEffects {
  GetGeneralChatMessagesRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ChatActions.GetChatMessagesRequest),
      concatMap((action) => {
        return this.chatService.getGeneralChatMessages(action).pipe(
          map((response) => {
            log(response);
            let result: Chat[] = MessageMapper.mapMessaegesToChat(response);
            return ChatActions.GetChatMessagesSuccess({ data: result });
          }),
          catchError((error) => {
            let errorMessage = '';
            if (error as HttpErrorResponse) {
              let status = (error as HttpErrorResponse).status;
              if (status == 404) {
                errorMessage = Messages.NoUserWithUsername;
              } else {
                errorMessage = Messages.Error;
              }
            }
            return of(ChatActions.GetChatMessagesFail({ error: errorMessage }));
          })
        );
      })
    )
  );

  GetGeneralChatMessagesFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(ChatActions.GetChatMessagesFail),
        tap(({ error }) => {
          this.toast.error(error, 'Error');
        })
      ),
    { dispatch: false }
  );

  constructor(
    private actions$: Actions,
    private chatService: GeneralChatService,
    private toast: ToastrService
  ) {}
}
