import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { concatMap, map, catchError, tap } from 'rxjs/operators';

import { GeneralChatService } from 'src/app/services/api/chatService/general-chat.service';
import { ChatActions } from 'src/app/store/actions/chat.actions';
import { Messages } from 'src/assets/common/strings';

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
          catchError((error) => {
            let errorMessage = '';
            let status = (error as HttpErrorResponse).status;
            if (status == 401) {
              errorMessage = Messages.NotAllowedToAccessResource;
            } else if (status == 404) {
              errorMessage = Messages.NoUserWithUsername;
            } else {
              errorMessage = Messages.Error;
            }
            return of(ChatActions.GetChatMessagesFail({ error: errorMessage }));
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
          this.toast.warning(error, 'Error', { progressBar: false });
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
