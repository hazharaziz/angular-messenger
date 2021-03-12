import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { catchError, concatMap, map, tap } from 'rxjs/operators';

import { GroupService } from 'src/app/services/api/group-service/group.service';
import { GroupActions } from 'src/app/store/actions/group.actions';
import { ProfileActions } from 'src/app/store/actions/profile.actions';

@Injectable()
export class CreateGroupEffects {
  createGroupRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(GroupActions.CreateGroupRequest),
      concatMap((payload) =>
        this.groupService.createGroupRequest(payload).pipe(
          map(() => GroupActions.GetGroupsRequest()),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(GroupActions.CreateGroupFail({ error: error.error }));
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
          this.toast.error(error, 'Error');
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
