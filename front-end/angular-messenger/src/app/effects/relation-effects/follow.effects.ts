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
export class FollowEffects {
  followRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(RelationActions.FollowRequest),
      concatMap((payload) =>
        this.relationService.followRequest(payload.userId).pipe(
          map(() => RelationActions.GetFollowersRequest()),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(RelationActions.FollowFail({ error: error.error }));
          })
        )
      )
    )
  );

  followFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(RelationActions.FollowFail),
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
