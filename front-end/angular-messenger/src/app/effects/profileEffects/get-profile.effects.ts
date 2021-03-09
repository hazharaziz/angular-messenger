import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, concatMap, map } from 'rxjs/operators';

import { ProfileService } from 'src/app/services/api/profileService/profile.service';
import { ProfileActions } from 'src/app/store/actions/profile.actions';
import { Messages } from 'src/assets/common/strings';

@Injectable()
export class GetProfileEffects {
  getProfileRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProfileActions.GetProfileRequest),
      concatMap(() =>
        this.profileService.getProfileRequest().pipe(
          map((response) => ProfileActions.GetProfileSuccess(response)),
          catchError((error) => {
            let errorMessage = '';
            let status = error.status;
            if (status == 404) {
              errorMessage = Messages.NoUserWithUsername;
            } else {
              errorMessage = Messages.Error;
            }
            return of(ProfileActions.GetProfileFail({ error: errorMessage }));
          })
        )
      )
    )
  );

  constructor(private actions$: Actions, private profileService: ProfileService) {}
}
