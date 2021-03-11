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
export class RemoveDirectEffects {
  removeDirectRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DirectActions.RemoveDirectRequest),
      concatMap((payload) => {
        return this.directService.deleteDirectRequest(payload.directId).pipe(
          map(() => DirectActions.GetDirectsRequest()),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(DirectActions.RemoveDirectFail({ error: error.error }));
          })
        );
      })
    )
  );

  removeDirectFail = createEffect(
    () =>
      this.actions$.pipe(
        ofType(DirectActions.RemoveDirectFail),
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
