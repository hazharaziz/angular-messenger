import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Relation } from 'src/app/models/data/relation.model';
import { relationStateFeatureKey } from '..';

const selectRelations = createFeatureSelector<Relation>(relationStateFeatureKey);

export const RelationSelectors = {
  selectFollowers: createSelector(selectRelations, (state: Relation) => state.followers),
  selectFollowings: createSelector(selectRelations, (state: Relation) => state.followings),
  selectSentRequests: createSelector(selectRelations, (state: Relation) => state.sentRequests),
  selectReceivedRequests: createSelector(
    selectRelations,
    (state: Relation) => state.receivedRequest
  )
};
