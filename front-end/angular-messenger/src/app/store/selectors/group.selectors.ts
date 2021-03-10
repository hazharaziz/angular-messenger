import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Group } from 'src/app/models/data/group.model';
import { groupStateFeatureKey } from '..';

const selectGroupState = createFeatureSelector<Group[]>(groupStateFeatureKey);

export const GroupSelectors = {
  selectUserGroups: createSelector(selectGroupState, (state: Group[]) => state),
  selectGroupInfo: createSelector(selectGroupState, (state: Group[], groupId: number) =>
    state.find((group) => group.groupId == groupId)
  ),
  selectGroupMessages: createSelector(
    selectGroupState,
    (state: Group[], groupId: number) => state.find((group) => group.groupId == groupId).messages
  )
};
