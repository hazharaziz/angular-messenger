import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, concatMap, map } from 'rxjs/operators';

import { GroupService } from 'src/app/services/api/group-service/group.service';
import { GroupActions } from 'src/app/store/actions/group.actions';

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
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(GroupActions.GetAvailableFriendsFail({ error: error.error }));
          })
        )
      )
    )
  );

  constructor(private actions$: Actions, private groupService: GroupService) {}
}
