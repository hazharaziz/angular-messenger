import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { catchError, concatMap, map, tap } from 'rxjs/operators';

import { RelationService } from 'src/app/services/api/relation-service/relation.service';
import { RelationActions } from 'src/app/store/actions/relation.actinos';

@Injectable()
export class CancelRequestEffects {
  cancelRequestRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(RelationActions.CancelRequestRequest),
      concatMap((payload) =>
        this.relationService.cancelRequestRequest(payload.userId).pipe(
          map(() => RelationActions.GetRequestsSentRequest()),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(RelationActions.CancelRequestFail({ error: error.error }));
          })
        )
      )
    )
  );

  cancelRequestFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(RelationActions.CancelRequestFail),
        tap(({ error }) => {
          this.toast.error(error, 'Error');
        })
      ),
    { dispatch: false }
  );

  constructor(
    private actions$: Actions,
    private relationService: RelationService,
    private toast: ToastrService
  ) {}
}
