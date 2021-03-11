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
export class DeleteMessageEffects {
  deleteMessageRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ChatActions.DeleteMessageRequest),
      concatMap(({ messageId }) => {
        return this.chatService.deleteMessageRequest(messageId).pipe(
          map(() => {
            return ChatActions.GetChatMessagesRequest();
          }),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(ChatActions.DeleteMessageFail({ error: error.error }));
          })
        );
      })
    )
  );

  deleteMessageFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(ChatActions.DeleteMessageFail),
        tap(({ error }) => {
          this.toast.warning(error);
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
