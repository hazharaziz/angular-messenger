import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { exhaustMap, map, catchError, tap, concatMap } from 'rxjs/operators';

import { AuthService } from 'src/app/services/api/auth.service';
import { AuthActions } from 'src/app/store/actions/auth.actions';
import { Messages } from 'src/assets/common/strings';

@Injectable()
export class SignUpEffects {
  signUpRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.SignUpRequest),
      concatMap((action) => {
        return this.authService.signUp(action).pipe(
          map((response) => AuthActions.SignUpSuccess(response)),
          catchError((error) => {
            let errorMessage = '';
            if (error as HttpErrorResponse) {
              let status = (error as HttpErrorResponse).status;
              if (status == 409) {
                errorMessage = Messages.UserExists;
              } else {
                errorMessage = Messages.Error;
              }
            }
            return of(AuthActions.SignUpFail({ error }));
          })
        );
      })
    )
  );

  signUpSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.SignUpSuccess),
        tap(() => {
          this.router.navigate(['/home']);
        })
      ),
    { dispatch: false }
  );

  signUpFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.SignUpFail),
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
