import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Relation } from 'src/app/models/data/relation.model';
import { relationStateFeatureKey } from '..';

const selectRelations = createFeatureSelector<Relation>(relationStateFeatureKey);

export const RelationSelector = {
  selectFollowers: createSelector(selectRelations, (state: Relation) => state.followers)
};
