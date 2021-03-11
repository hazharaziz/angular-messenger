import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { concatMap, map, catchError } from 'rxjs/operators';

import { DirectService } from 'src/app/services/api/direct-service/direct.service';
import { DirectActions } from 'src/app/store/actions/direct.actions';
import { log } from 'src/app/utils/logger';
import { MessageMapper } from 'src/app/utils/message-mapper';

@Injectable()
export class GetDirectMessagesEffects {
  getDirectMessagesRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DirectActions.GetDirectMessagesRequest),
      concatMap((payload) => {
        return this.directService.getDirectMessagesRequest(payload.targetId).pipe(
          map((response) => {
            let result = MessageMapper.mapMessaegesToChat(response);
            return DirectActions.GetDirectMessagesSuccess({ messages: result });
          }),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(DirectActions.GetDirectMessagesFail({ error: error.error }));
          })
        );
      })
    )
  );

  constructor(private actions$: Actions, private directService: DirectService) {}
}
