import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { concatMap, map, catchError, tap } from 'rxjs/operators';

import { GeneralChatService } from 'src/app/services/api/chat-service/general-chat.service';
import { ChatActions } from 'src/app/store/actions/chat.actions';

@Injectable()
export class SendMessageEffects {
  sendMessageRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ChatActions.SendMessageRequest),
      concatMap((action) =>
        this.chatService.sendMessageRequest(action).pipe(
          map(() => {
            return ChatActions.GetChatMessagesRequest();
          }),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(ChatActions.SendMessageFail({ error: error.error }));
          })
        )
      )
    )
  );

  sendMessageFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(ChatActions.SendMessageFail),
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
