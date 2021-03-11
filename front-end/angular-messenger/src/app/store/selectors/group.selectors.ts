import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Chat } from 'src/app/models/data/chat.model';

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
      if (group == undefined) return [];
      if (group.friends == undefined) return [];
      return group.friends;
    }
  ),
  selectGroupMessages: createSelector(
    selectGroupState,
    (state: Group[], props: { groupId: number }) => {
      let group = state.find((group) => group.groupId == props.groupId);
      if (group == undefined) return [];
      if (group.messages == undefined) return [];
      return group.messages;
    }
  )
};
