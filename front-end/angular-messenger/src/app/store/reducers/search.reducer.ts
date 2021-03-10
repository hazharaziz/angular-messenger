import { createReducer, on } from '@ngrx/store';
import { User } from 'src/app/models/data/user.model';
import { SearchActions } from '../actions/search.actions';

const initialState: User[] = [];

export const searchReducer = createReducer(
  initialState,
  on(
    SearchActions.searchSuccess,
    (state: User[], payload: { searchedUsers: User[] }) => payload.searchedUsers
  )
);
