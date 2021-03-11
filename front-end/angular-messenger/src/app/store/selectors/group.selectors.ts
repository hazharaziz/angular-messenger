import { createFeatureSelector, createSelector } from '@ngrx/store';

import { Group } from 'src/app/models/data/group.model';
import { groupStateFeatureKey } from '..';

const selectGroupState = createFeatureSelector<Group[]>(groupStateFeatureKey);

export const GroupSelectors = {
  selectUserGroups: createSelector(selectGroupState, (state: Group[]) => state),
  selectGroupInfo: createSelector(selectGroupState, (state: Group[], props: { groupId: number }) =>
    state.find((group) => group.groupId == props.groupId)
  ),
  selectAvailableFriends: createSelector(
    selectGroupState,
    (state: Group[], props: { groupId: number }) => {
      let group = state.find((group) => group.groupId == props.groupId);
      if (group == undefined || group.friends == undefined) return [];
      return group.friends;
    }
  ),
  selectGroupMessages: createSelector(
    selectGroupState,
    (state: Group[], props: { groupId: number }) => {
      let messages = state.find((group) => group.groupId == props.groupId).messages;
      messages = messages == undefined ? [] : messages;
      return messages;
    }
  )
};
