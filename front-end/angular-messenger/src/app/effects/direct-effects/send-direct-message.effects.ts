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
export class SendDirectMessageEffects {
  sendDirectMessageRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DirectActions.SendDirectMessageRequest),
      concatMap((payload) => {
        return this.directService
          .sendDirectMessageRequest(payload.targetId, {
            ...payload,
            targetId: undefined
          })
          .pipe(
            map(() => DirectActions.GetDirectsRequest()),
            catchError((err) => {
              let error: HttpErrorResponse = err as HttpErrorResponse;
              return of(DirectActions.SendDirectMessageFail({ error: error.error }));
            })
          );
      })
    )
  );

  sendDirectMessageFail = createEffect(
    () =>
      this.actions$.pipe(
        ofType(DirectActions.SendDirectMessageFail),
        tap(({ error }) => {
          this.toast.error(error, 'Error');
        })
      ),
    { dispatch: false }
  );

  constructor(
    private actions$: Actions,
    private directService: DirectService,
    private toast: ToastrService
  ) {}
}
