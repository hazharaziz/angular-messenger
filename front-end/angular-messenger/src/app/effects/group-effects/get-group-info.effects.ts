import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, concatMap, map } from 'rxjs/operators';
import { GroupService } from 'src/app/services/api/group-service/group.service';
import { GroupActions } from 'src/app/store/actions/group.actions';

@Injectable()
export class GetGroupInfoEffects {
  getGroupInfoRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(GroupActions.GetGroupInfoRequest),
      concatMap((payload) =>
        this.groupService.getGroupInfoRequest(payload.groupId).pipe(
          map((response) => GroupActions.GetGroupInfoSuccess({ groupInfo: response })),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(GroupActions.GetGroupInfoFail({ error: error.error }));
          })
        )
      )
    )
  );

  constructor(private actions$: Actions, private groupService: GroupService) {}
}
