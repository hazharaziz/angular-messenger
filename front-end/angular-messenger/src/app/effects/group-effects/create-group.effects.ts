import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { catchError, concatMap, map, tap } from 'rxjs/operators';

import { GroupService } from 'src/app/services/api/group-service/group.service';
import { GroupActions } from 'src/app/store/actions/group.actions';
import { Messages } from 'src/assets/common/strings';

@Injectable()
export class CreateGroupEffects {
  createGroupRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(GroupActions.CreateGroupRequest),
      concatMap((payload) =>
        this.groupService.createGroupRequest(payload).pipe(
          map(() => GroupActions.GetGroupsRequest()),
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
            return of(GroupActions.CreateGroupFail({ error: errorMessage }));
          })
        )
      )
    )
  );

  createGroupFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(GroupActions.CreateGroupFail),
        tap(({ error }) => {
          this.toast.warning(error, undefined);
        })
      ),
    { dispatch: false }
  );

  constructor(
    private actions$: Actions,
    private groupService: GroupService,
    private router: Router,
    private toast: ToastrService
  ) {}
}
