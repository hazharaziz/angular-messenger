import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { catchError, concatMap, map, tap } from 'rxjs/operators';

import { GroupService } from 'src/app/services/api/group-service/group.service';
import { GroupActions } from 'src/app/store/actions/group.actions';

@Injectable()
export class DeleteGroupEffects {
  deleteGroupRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(GroupActions.DeleteGroupRequest),
      concatMap((payload) =>
        this.groupService.deleteGroupRequest(payload.groupId).pipe(
          map(() => GroupActions.GetGroupsRequest()),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(GroupActions.DeleteGroupFail({ error: error.error }));
          })
        )
      )
    )
  );

  deleteGroupFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(GroupActions.DeleteGroupFail),
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
