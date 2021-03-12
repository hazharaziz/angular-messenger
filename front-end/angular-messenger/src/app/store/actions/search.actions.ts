import { createAction, props } from '@ngrx/store';
import { User } from 'src/app/models/data/user.model';
import { ActionTypes } from './types';

export const SearchActions = {
  SearchRequest: createAction(ActionTypes.SearchRequest, props<{ query: string }>()),
  SearchSuccess: createAction(ActionTypes.SearchSuccess, props<{ searchedUsers: User[] }>()),
  SearchFail: createAction(ActionTypes.SearchFail, props<{ error: string }>()),
  RemoveSearchItem: createAction(ActionTypes.RemoveSearchItem, props<{ id: number }>())
};
