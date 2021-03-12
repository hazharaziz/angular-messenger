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
export class EditProfileEffects {
  editProfileRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProfileActions.EditProfileRequest),
      concatMap((editedUser) =>
        this.profileService.editProfileRequest(editedUser).pipe(
          map(() => ProfileActions.GetProfileRequest()),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(ProfileActions.EditProfileFail({ error: error.error }));
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
