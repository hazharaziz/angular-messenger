import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { concatMap, map, catchError, tap } from 'rxjs/operators';

import { AuthService } from 'src/app/services/api/authService/auth.service';
import { AuthActions } from 'src/app/store/actions/auth.actions';
import { Messages } from 'src/assets/common/strings';

@Injectable()
export class LoginEffects {
  loginRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.LoginRequest),
      concatMap((action) => {
        return this.authService.login(action).pipe(
          map((response) => AuthActions.LoginSuccess(response)),
          catchError((error) => {
            let errorMessage = '';
            if (error as HttpErrorResponse) {
              let status = (error as HttpErrorResponse).status;
              if (status == 404) {
                errorMessage = Messages.NoUserWithUsername;
              } else if (status == 401) {
                errorMessage = Messages.WrongAuthCredentials;
              } else {
                errorMessage = Messages.Error;
              }
            }
            return of(AuthActions.LoginFail({ error: errorMessage }));
          })
        );
      })
    )
  );

  loginSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.LoginSuccess),
        tap(() => {
          this.router.navigate(['/general']);
        })
      ),
    { dispatch: false }
  );

  loginFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.LoginFail),
        tap(({ error }) => {
          this.toast.error(error, 'Error');
        })
      ),
    { dispatch: false }
  );

  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private router: Router,
    private toast: ToastrService
  ) {}
}
