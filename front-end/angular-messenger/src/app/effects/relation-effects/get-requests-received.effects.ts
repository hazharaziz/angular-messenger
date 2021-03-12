import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { catchError, concatMap, map } from 'rxjs/operators';

import { RelationService } from 'src/app/services/api/relation-service/relation.service';
import { RelationActions } from 'src/app/store/actions/relation.actinos';

@Injectable()
export class GetRequestsReceivedEffects {
  getRequestsReceivedRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(RelationActions.GetRequestsReceivedRequest),
      concatMap(() =>
        this.relationService.getRequestsReceivedRequest().pipe(
          map((response) =>
            RelationActions.GetRequestsReceivedSuccess({ receivedRequests: response })
          ),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(RelationActions.GetRequestsReceivedFail({ error: error.error }));
          })
        )
      )
    )
  );

  constructor(
    private actions$: Actions,
    private relationService: RelationService,
    private toast: ToastrService
  ) {}
}
