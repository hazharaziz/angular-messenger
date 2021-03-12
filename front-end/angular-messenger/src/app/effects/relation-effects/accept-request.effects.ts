import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { catchError, concatMap, map, tap } from 'rxjs/operators';

import { RelationService } from 'src/app/services/api/relation-service/relation.service';
import { RelationActions } from 'src/app/store/actions/relation.actinos';
import { log } from 'src/app/utils/logger';

@Injectable()
export class AcceptRequestEffects {
  acceptRequestRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(RelationActions.AcceptRequestRequest),
      concatMap((payload) =>
        this.relationService.acceptRequestRequest(payload.userId).pipe(
          map(() => RelationActions.GetRequestsReceivedRequest()),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(RelationActions.AcceptRequestFail({ error: error.error }));
          })
        )
      )
    )
  );

  acceptRequestFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(RelationActions.AcceptRequestFail),
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
