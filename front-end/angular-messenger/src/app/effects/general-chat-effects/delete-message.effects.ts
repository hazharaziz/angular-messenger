import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { concatMap, map, catchError, tap } from 'rxjs/operators';

import { GeneralChatService } from 'src/app/services/api/chat-service/general-chat.service';
import { ChatActions } from 'src/app/store/actions/chat.actions';
import { log } from 'src/app/utils/logger';
import { Messages } from 'src/assets/common/strings';

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
          catchError((error) => {
            let errorMessage = '';
            let status: number = error.status;
            if (status == 404) {
              errorMessage = Messages.MessageNotFound;
            } else if (status == 405) {
              errorMessage = Messages.DeleteMessageNotAllowed;
            } else {
              errorMessage = Messages.Error;
            }
            return of(ChatActions.GetChatMessagesFail({ error: errorMessage }));
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
