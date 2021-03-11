import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { concatMap, map, catchError } from 'rxjs/operators';

import { DirectService } from 'src/app/services/api/direct-service/direct.service';
import { DirectActions } from 'src/app/store/actions/direct.actions';
import { log } from 'src/app/utils/logger';

@Injectable()
export class GetDirectsEffects {
  getDirectsRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DirectActions.GetDirectsRequest),
      concatMap(() => {
        return this.directService.getDirectsRequest().pipe(
          map((response) => {
            return DirectActions.GetDirectsSuccess({ directs: response });
          }),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(DirectActions.GetDirectsFail({ error: error.error }));
          })
        );
      })
    )
  );

  constructor(private actions$: Actions, private directService: DirectService) {}
}
