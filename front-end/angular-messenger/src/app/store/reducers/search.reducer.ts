import { createReducer, on } from '@ngrx/store';
import { User } from 'src/app/models/data/user.model';
import { log } from 'src/app/utils/logger';
import { SearchActions } from '../actions/search.actions';

const initialState: User[] = [];

export const searchReducer = createReducer(
  initialState,
  on(
    SearchActions.SearchSuccess,
    (state: User[], payload: { searchedUsers: User[] }) => payload.searchedUsers
  ),
  on(SearchActions.RemoveSearchItem, (state: User[], payload: { id: number }) => {
    let newState: User[] = [];
    state.forEach((user) => {
      if (user.id != payload.id) {
        newState.push(user);
      }
    });
    return newState;
  })
);
