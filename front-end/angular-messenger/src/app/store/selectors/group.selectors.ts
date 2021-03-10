import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Group } from 'src/app/models/data/group.model';
import { User } from 'src/app/models/data/user.model';
import { friendStateFeatureKey, groupStateFeatureKey } from '..';

const selectGroupState = createFeatureSelector<Group[]>(groupStateFeatureKey);
const selectFriendState = createFeatureSelector<User[]>(friendStateFeatureKey);

export const GroupSelectors = {
  selectUserGroups: createSelector(selectGroupState, (state: Group[]) => state),
  selectGroupInfo: createSelector(selectGroupState, (state: Group[], groupId: number) =>
    state.find((group) => group.groupId == groupId)
  ),
  selectGroupMessages: createSelector(
    selectGroupState,
    (state: Group[], groupId: number) => state.find((group) => group.groupId == groupId).messages
  ),
  selectAvailableFriends: createSelector(selectFriendState, (state: User[]) => state)
};
