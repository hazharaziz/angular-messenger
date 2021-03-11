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
export class ClearDirectChatHistoryEffects {
  clearDirectChatHistoryRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DirectActions.ClearDirectChatHistoryRequest),
      concatMap((payload) => {
        return this.directService.deleteDirectChatHistoryRequest(payload.targetId).pipe(
          map(() => DirectActions.GetDirectMessagesRequest({ targetId: payload.targetId })),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(DirectActions.ClearDirectChatHistoryFail({ error: error.error }));
          })
        );
      })
    )
  );

  clearDirectChatHistoryFail = createEffect(() =>
    this.actions$.pipe(
      ofType(DirectActions.ClearDirectChatHistoryFail),
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
