import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, concatMap, map } from 'rxjs/operators';
import { GroupService } from 'src/app/services/api/group-service/group.service';
import { GroupActions } from 'src/app/store/actions/group.actions';
import { Messages } from 'src/assets/common/strings';

@Injectable()
export class GetAvailableFriendsEffects {
  getAvailableFriendsRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(GroupActions.GetAvailableFriendsRequest),
      concatMap((payload) =>
        this.groupService.getAvailableFriends(payload.groupId).pipe(
          map((response) =>
            GroupActions.GetAvailableFriendsSuccess({ groupId: payload.groupId, friends: response })
          ),
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
            return of(GroupActions.GetAvailableFriendsFail({ error: errorMessage }));
          })
        )
      )
    )
  );

  constructor(private actions$: Actions, private groupService: GroupService) {}
}
