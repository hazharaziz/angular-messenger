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
export class AddMemberToGroupEffects {
  addMemberToGroupRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(GroupActions.AddMemberToGroupRequest),
      concatMap((payload) =>
        this.groupService.addMemberToGroupRequest(payload.groupId, payload.members).pipe(
          map(() => GroupActions.GetGroupInfoRequest({ groupId: payload.groupId })),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(GroupActions.AddMemberToGroupFail({ error: error.error }));
          })
        )
      )
    )
  );

  addMemberToGroupFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(GroupActions.AddMemberToGroupFail),
        tap(({ error }) => {
          this.toast.warning(error);
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
