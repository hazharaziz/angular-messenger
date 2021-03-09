import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { catchError, concatMap, map, tap } from 'rxjs/operators';
import { ProfileService } from 'src/app/services/api/profileService/profile.service';
import { ProfileActions } from 'src/app/store/actions/profile.actions';
import { Messages } from 'src/assets/common/strings';

@Injectable()
export class ChangePasswordEffects {
  changePasswordRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProfileActions.ChangePasswordRequest),
      concatMap((payload) =>
        this.profileService.changePasswordRequest(payload.oldPassword, payload.newPassword).pipe(
          map(() => ProfileActions.ChangePasswordSuccess()),
          catchError((error) => {
            let errorMessage = '';
            let status: number = error.status;
            if (status == 404) {
              errorMessage = Messages.NoUserWithUsername;
            } else if (status == 401) {
              errorMessage = Messages.WrongAuthCredentials;
            } else {
              errorMessage = Messages.Error;
            }
            return of(ProfileActions.ChangePasswordFail({ error: errorMessage }));
          })
        )
      )
    )
  );

  changePasswordSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(ProfileActions.ChangePasswordSuccess),
        tap(() => {
          this.router.navigate(['/profile']);
        })
      ),
    { dispatch: false }
  );

  changePasswordFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(ProfileActions.GetProfileFail),
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
