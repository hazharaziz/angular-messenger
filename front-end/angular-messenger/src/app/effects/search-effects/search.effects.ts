import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, concatMap, delay, map } from 'rxjs/operators';

import { SearchService } from 'src/app/services/api/search-service/search.service';
import { SearchActions } from 'src/app/store/actions/search.actions';

@Injectable()
export class SearchEffects {
  searchRequest$ = createEffect(() =>
    this.actions$.pipe(
      ofType(SearchActions.SearchRequest),
      delay(2000),
      concatMap((payload) =>
        this.searchService.searchRequest(payload.query).pipe(
          map((response) => SearchActions.SearchSuccess({ searchedUsers: response })),
          catchError((err) => {
            let error: HttpErrorResponse = err as HttpErrorResponse;
            return of(SearchActions.SearchFail({ error: error.error }));
          })
        )
      )
    )
  );

  constructor(private actions$: Actions, private searchService: SearchService) {}
}
