import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { map, catchError, exhaustMap } from 'rxjs/operators';
import AuthActions from '../actions/auth.actions';
import { AuthService } from '../services/api/auth.service';

@Injectable()
export class AuthEffects {
  loginUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.LoginRequest),
      exhaustMap((action) => {
        return this.authService.login(action).pipe(
          map(
            (response) => AuthActions.LoginSuccess(response),
            catchError((e) =>
              of(AuthActions.LoginFail({ error: e as HttpErrorResponse }))
            )
          )
        );
      })
    )
  );

  signUpUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.SignUpRequest),
      exhaustMap((action) => {
        return this.authService.signUp(action).pipe(
          map(
            (response) => AuthActions.SignUpSuccess(response),
            catchError((e) =>
              of(AuthActions.SignUpFail({ error: e as HttpErrorResponse }))
            )
          )
        );
      })
    )
  );

  constructor(private actions$: Actions, private authService: AuthService) {}
}
