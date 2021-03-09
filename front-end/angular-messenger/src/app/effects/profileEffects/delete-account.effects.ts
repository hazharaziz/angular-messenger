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
export class DeleteAccountEffects {
  DeleteAccountRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProfileActions.DeleteAccountRequest),
      concatMap(() =>
        this.profileService.deleteAccountRequest().pipe(
          map(() => ProfileActions.DeleteAccountSuccess()),
          catchError((error) => {
            let errorMessage = '';
            let status: number = error.status;
            if (status == 404) {
              errorMessage = Messages.NoUserWithUsername;
            } else {
              errorMessage = Messages.Error;
            }
            return of(ProfileActions.DeleteAccountFail({ error: errorMessage }));
          })
        )
      )
    )
  );

  DeleteAccountSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(ProfileActions.DeleteAccountSuccess),
        tap(() => {
          this.router.navigate(['/login']);
          this.toast.success('Account deleted successfully', undefined, {
            progressBar: false
          });
        })
      ),
    { dispatch: false }
  );

  DeleteAccountFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(ProfileActions.DeleteAccountFail),
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
