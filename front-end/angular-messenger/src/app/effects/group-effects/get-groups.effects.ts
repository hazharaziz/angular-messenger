import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, concatMap, delay, map } from 'rxjs/operators';
import { GroupService } from 'src/app/services/api/group-service/group.service';
import { GroupActions } from 'src/app/store/actions/group.actions';

@Injectable()
export class GetGroupsEffects {
  getGroupsRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(GroupActions.GetGroupsRequest),
      concatMap((payload) =>
        this.groupService.getGroupsRequest().pipe(
          map((response) => GroupActions.GetGroupsSuccess({ groups: response })),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(GroupActions.GetGroupsFail({ error: error.error }));
          })
        )
      )
    )
  );

  constructor(private actions$: Actions, private groupService: GroupService) {}
}
