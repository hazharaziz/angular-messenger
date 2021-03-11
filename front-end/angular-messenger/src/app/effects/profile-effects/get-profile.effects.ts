import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, concatMap, map } from 'rxjs/operators';

import { ProfileService } from 'src/app/services/api/profile-service/profile.service';
import { ProfileActions } from 'src/app/store/actions/profile.actions';

@Injectable()
export class GetProfileEffects {
  getProfileRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProfileActions.GetProfileRequest),
      concatMap(() =>
        this.profileService.getProfileRequest().pipe(
          map((response) => ProfileActions.GetProfileSuccess(response)),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(ProfileActions.GetProfileFail({ error: error.error }));
          })
        )
      )
    )
  );

  constructor(private actions$: Actions, private profileService: ProfileService) {}
}
