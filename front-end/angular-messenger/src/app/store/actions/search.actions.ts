import { createAction, props } from '@ngrx/store';
import { User } from 'src/app/models/data/user.model';
import { ActionTypes } from './types';

export const SearchActions = {
  searchRequest: createAction(ActionTypes.SearchRequest, props<{ query: string }>()),
  searchSuccess: createAction(ActionTypes.SearchSuccess, props<{ searchedUsers: User[] }>()),
  searchFail: createAction(ActionTypes.SearchFail, props<{ error: string }>())
};
