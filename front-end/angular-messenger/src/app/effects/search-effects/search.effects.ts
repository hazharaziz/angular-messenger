import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, concatMap, delay, map } from 'rxjs/operators';
import { SearchService } from 'src/app/services/api/search-service/search.service';
import { SearchActions } from 'src/app/store/actions/search.actions';
import { Messages } from 'src/assets/common/strings';

@Injectable()
export class SearchEffects {
  searchRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(SearchActions.searchRequest),
      delay(2000),
      concatMap((payload) =>
        this.searchService.searchRequest(payload.query).pipe(
          map((response) => SearchActions.searchSuccess({ searchedUsers: response })),
          catchError((error) => {
            let errorMessage = '';
            let status = error.status;
            if (status == 401) {
              errorMessage = Messages.AuthorizationFailed;
            } else if (status == 404) {
              errorMessage = Messages.NotFound;
            } else {
              errorMessage = Messages.Error;
            }
            return of(SearchActions.searchFail({ error: errorMessage }));
          })
        )
      )
    )
  );

  constructor(private actions$: Actions, private searchService: SearchService) {}
}
