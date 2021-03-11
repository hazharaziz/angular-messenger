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
export class GetGroupMessagesEffects {
  getGroupMessagesRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(GroupActions.GetGroupMessagesRequest),
      concatMap((payload) =>
        this.groupService.getGroupMessagesRequest(payload.groupId).pipe(
          map((response) =>
            GroupActions.GetGroupMessagesSuccess({ groupId: payload.groupId, messages: response })
          ),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(GroupActions.GetGroupMessagesFail({ error: error.error }));
          })
        )
      )
    )
  );

  constructor(
    private actions$: Actions,
    private groupService: GroupService,
    private router: Router,
    private toast: ToastrService
  ) {}
}
