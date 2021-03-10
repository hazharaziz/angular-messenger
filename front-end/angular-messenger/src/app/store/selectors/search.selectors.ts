import { createFeatureSelector, createSelector } from '@ngrx/store';
import { User } from 'src/app/models/data/user.model';
import { searchStateFeatureKey } from '..';

const selectSearchState = createFeatureSelector<User[]>(searchStateFeatureKey);

export const SearchSelectors = {
  selectSearchedUsers: createSelector(selectSearchState, (state: User[]) => state)
};
