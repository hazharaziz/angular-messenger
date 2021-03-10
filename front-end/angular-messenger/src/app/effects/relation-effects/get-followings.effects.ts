import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { catchError, concatMap, map, tap } from 'rxjs/operators';

import { RelationService } from 'src/app/services/api/relation-service/relation.service';
import { RelationActions } from 'src/app/store/actions/relation.actinos';
import { Messages } from 'src/assets/common/strings';

@Injectable()
export class GetFollowingsEffects {
  getFollowingsRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(RelationActions.GetFollowingsRequest),
      concatMap(() =>
        this.relationService.getFollowingsRequest().pipe(
          map((response) => RelationActions.GetFollowingsSucces({ followings: response })),
          catchError((error) => {
            let errorMessage = '';
            let status = error.status;
            if (status == 401) {
              errorMessage = Messages.AuthorizationFailed;
            } else if (status == 404) {
              errorMessage = Messages.NoUserWithUsername;
            } else {
              errorMessage = Messages.Error;
            }
            return of(RelationActions.GetFollowingsFail({ error: errorMessage }));
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
