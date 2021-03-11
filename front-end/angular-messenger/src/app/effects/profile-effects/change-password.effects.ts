import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { catchError, concatMap, map, tap } from 'rxjs/operators';
import { ProfileService } from 'src/app/services/api/profile-service/profile.service';
import { ProfileActions } from 'src/app/store/actions/profile.actions';

@Injectable()
export class ChangePasswordEffects {
  changePasswordRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProfileActions.ChangePasswordRequest),
      concatMap((payload) =>
        this.profileService.changePasswordRequest(payload.oldPassword, payload.newPassword).pipe(
          map(() => ProfileActions.ChangePasswordSuccess()),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(ProfileActions.ChangePasswordFail({ error: error.error }));
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
        ofType(ProfileActions.ChangePasswordFail),
        tap(({ error }) => {
          this.toast.error(error, 'Error');
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
