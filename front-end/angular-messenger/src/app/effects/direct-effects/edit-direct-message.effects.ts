import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { concatMap, map, catchError, tap } from 'rxjs/operators';

import { DirectService } from 'src/app/services/api/direct-service/direct.service';
import { DirectActions } from 'src/app/store/actions/direct.actions';
import { log } from 'src/app/utils/logger';

@Injectable()
export class EditDirectMessageEffects {
  editDirectMessageRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DirectActions.EditDirectMessageRequest),
      concatMap((payload) => {
        return this.directService
          .editDirectMessageRequest(payload.directMessage.directId, payload.directMessage.id, {
            text: payload.directMessage.text
          })
          .pipe(
            map(() =>
              DirectActions.GetDirectMessagesRequest({ targetId: payload.directMessage.targetId })
            ),
            catchError((err) => {
              let error: HttpErrorResponse = err as HttpErrorResponse;
              return of(DirectActions.EditDirectMessageFail({ error: error.error }));
            })
          );
      })
    )
  );

  editDirectMessageFail = createEffect(() =>
    this.actions$.pipe(
      ofType(DirectActions.EditDirectMessageFail),
      tap(({ error }) => {
        this.toast.error(error, 'Error');
      })
    )
  );

  constructor(
    private actions$: Actions,
    private directService: DirectService,
    private toast: ToastrService
  ) {}
}
