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
export class EditMessageEffects {
  EditMessageRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ChatActions.EditMessageRequest),
      concatMap((message) => {
        return this.chatService.editMessageRequest(message?.id, { text: message.text }).pipe(
          map(() => {
            return ChatActions.GetChatMessagesRequest();
          }),
          catchError((error) => {
            let errorMessage = '';
            let status: number = error.status;
            if (status == 404) {
              errorMessage = Messages.MessageNotFound;
            } else if (status == 405) {
              errorMessage = Messages.EditMessageNotAllowed;
            } else {
              errorMessage = Messages.Error;
            }
            return of(ChatActions.GetChatMessagesFail({ error: errorMessage }));
          })
        );
      })
    )
  );

  SendMessageFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(ChatActions.EditMessageFail),
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
