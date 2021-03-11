import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { map, catchError, tap, concatMap } from 'rxjs/operators';

import { AuthService } from 'src/app/services/api/auth-service/auth.service';
import { AuthActions } from 'src/app/store/actions/auth.actions';
import { log } from 'src/app/utils/logger';

@Injectable()
export class SignUpEffects {
  signUpRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.SignUpRequest),
      concatMap((action) => {
        return this.authService.signUp(action).pipe(
          map((response) => AuthActions.SignUpSuccess(response)),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(AuthActions.SignUpFail({ error: error.error }));
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
          this.router.navigate(['/general']);
        })
      ),
    { dispatch: false }
  );

  signUpFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(AuthActions.SignUpFail),
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
