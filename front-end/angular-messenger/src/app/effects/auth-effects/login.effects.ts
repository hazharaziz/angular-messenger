import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { concatMap, map, catchError, tap } from 'rxjs/operators';

import { AuthService } from 'src/app/services/api/auth-service/auth.service';
import { AuthActions } from 'src/app/store/actions/auth.actions';

@Injectable()
export class LoginEffects {
  loginRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.LoginRequest),
      concatMap((action) => {
        return this.authService.login(action).pipe(
          map((response) => AuthActions.LoginSuccess(response)),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(AuthActions.LoginFail({ error: error.error }));
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
          this.toast.error(error);
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
