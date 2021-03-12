import { createFeatureSelector, createSelector } from '@ngrx/store';

import { User } from 'src/app/models/data/user.model';
import { userStateFeatureKey } from '..';

const selectUserState = createFeatureSelector<User>(userStateFeatureKey);

export const ProfileSelectors = {
  selectUser: createSelector(selectUserState, (state: User) => state),
  selectUserId: createSelector(selectUserState, (state: User) => state?.id),
  selectUserAccountPrivacy: createSelector(selectUserState, (state: User) => state?.isPublic)
};
