import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { catchError, concatMap, map, tap } from 'rxjs/operators';
import { ProfileService } from 'src/app/services/api/profile-service/profile.service';
import { ProfileActions } from 'src/app/store/actions/profile.actions';
import { Messages } from 'src/assets/common/strings';

@Injectable()
export class EditProfileEffects {
  editProfileRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProfileActions.EditProfileRequest),
      concatMap((editedUser) =>
        this.profileService.editProfileRequest(editedUser).pipe(
          map(() => ProfileActions.GetProfileRequest()),
          catchError((error) => {
            let errorMessage = '';
            let status: number = error.status;
            if (status == 401) {
              errorMessage = Messages.AuthorizationFailed;
            } else if (status == 404) {
              errorMessage = Messages.NoUserWithUsername;
            } else if (status == 409) {
              errorMessage = Messages.UserExists;
            } else {
              errorMessage = Messages.Error;
            }
            return of(ProfileActions.EditProfileFail({ error: errorMessage }));
          })
        )
      )
    )
  );

  editProfileFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(ProfileActions.EditProfileFail),
        tap(({ error }) => {
          this.toast.warning(error, 'Error');
        })
      ),
    { dispatch: false }
  );

  constructor(
    private actions$: Actions,
    private profileService: ProfileService,
    private router: Router,
    private toast: ToastrService
  ) {}
}
