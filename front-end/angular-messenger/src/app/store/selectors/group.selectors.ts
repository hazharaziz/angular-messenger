import { createFeatureSelector, createSelector } from '@ngrx/store';

import { Group } from 'src/app/models/data/group.model';
import { groupStateFeatureKey } from '..';

const selectGroupState = createFeatureSelector<Group[]>(groupStateFeatureKey);

export const GroupSelectors = {
  selectUserGroups: createSelector(selectGroupState, (state: Group[]) => state),
  selectGroupInfo: createSelector(selectGroupState, (state: Group[], groupId: number) => {
    state.find((group) => group.groupId == groupId);
  }),
  selectAvailableFriends: createSelector(selectGroupState, (state: Group[], groupId: number) => {
    let friends = state.find((group) => group.groupId == groupId).friends;
    friends = friends == undefined ? [] : friends;
    return friends;
  }),
  selectGroupMessages: createSelector(selectGroupState, (state: Group[], groupId: number) => {
    let messages = state.find((group) => group.groupId == groupId).messages;
    messages = messages == undefined ? [] : messages;
    return messages;
  })
};
