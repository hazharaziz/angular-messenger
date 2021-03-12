import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { concatMap, map, catchError, tap } from 'rxjs/operators';

import { GeneralChatService } from 'src/app/services/api/chat-service/general-chat.service';
import { ChatActions } from 'src/app/store/actions/chat.actions';
import { log } from 'src/app/utils/logger';

@Injectable()
export class EditMessageEffects {
  editMessageRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ChatActions.EditMessageRequest),
      concatMap(({ messageId, message }) => {
        return this.chatService.editMessageRequest(messageId, { text: message }).pipe(
          map(() => {
            return ChatActions.GetChatMessagesRequest();
          }),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(ChatActions.EditMessageFail({ error: error.error }));
          })
        );
      })
    )
  );

  editMessageFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(ChatActions.EditMessageFail),
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
