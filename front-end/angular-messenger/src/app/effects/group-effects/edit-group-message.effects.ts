import { HttpErrorResponse } from '@angular/common/http';
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
export class EditGroupMessageEffects {
  editGroupMessageRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(GroupActions.EditGroupMessageRequest),
      concatMap((payload) =>
        this.groupService
          .editGroupMessageRequest(payload.message.groupId, payload.message.id, {
            ...payload.message,
            id: undefined,
            groupId: undefined
          })
          .pipe(
            map(() => GroupActions.GetGroupMessagesRequest({ groupId: payload.message.groupId })),
            catchError((err) => {
              let error: HttpErrorResponse = err as HttpErrorResponse;
              return of(GroupActions.EditGroupMessageFail({ error: error.error }));
            })
          )
      )
    )
  );

  editGroupMessageFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(GroupActions.EditGroupMessageFail),
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
