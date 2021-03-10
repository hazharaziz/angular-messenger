import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { catchError, concatMap, map, tap } from 'rxjs/operators';

import { RelationService } from 'src/app/services/api/relation-service/relation.service';
import { RelationActions } from 'src/app/store/actions/relation.actinos';
import { Messages } from 'src/assets/common/strings';

@Injectable()
export class UnfollowEffects {
  unfollowRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(RelationActions.UnfollowRequest),
      concatMap((payload) =>
        this.relationService.unfollowRequest(payload.followingId).pipe(
          map(() => RelationActions.GetFollowingsRequest()),
          catchError((error) => {
            let errorMessage = '';
            let status = error.status;
            if (status == 401) {
              errorMessage = Messages.AuthorizationFailed;
            } else if (status == 404) {
              errorMessage = Messages.NotFound;
            } else {
              errorMessage = Messages.Error;
            }
            return of(RelationActions.UnfollowFail({ error: errorMessage }));
          })
        )
      )
    )
  );

  unfollowFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(RelationActions.UnfollowFail),
        tap(({ error }) => {
          this.toast.warning(error, undefined);
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
