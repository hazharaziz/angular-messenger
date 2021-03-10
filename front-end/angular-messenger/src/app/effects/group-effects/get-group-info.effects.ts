import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, concatMap, map } from 'rxjs/operators';
import { GroupService } from 'src/app/services/api/group-service/group.service';
import { GroupActions } from 'src/app/store/actions/group.actions';
import { Messages } from 'src/assets/common/strings';

@Injectable()
export class GetGroupInfoEffects {
  getGroupInfoRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(GroupActions.GetGroupInfoRequest),
      concatMap((payload) =>
        this.groupService.getGroupInfoRequest(payload.groupId).pipe(
          map((response) => GroupActions.GetGroupInfoSuccess({ groupInfo: response })),
          catchError((error) => {
            let errorMessage = '';
            let status = error.status;
            if (status == 401) {
              errorMessage = Messages.AuthorizationFailed;
            } else if (status == 404) {
              errorMessage = Messages.NotFound;
            } else if (status == 405) {
              errorMessage = Messages.NotAllowed;
            } else {
              errorMessage = Messages.Error;
            }
            return of(GroupActions.GetGroupInfoFail({ error: errorMessage }));
          })
        )
      )
    )
  );

  constructor(private actions$: Actions, private groupService: GroupService) {}
}
