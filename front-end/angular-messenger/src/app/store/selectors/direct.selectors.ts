import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Direct } from 'src/app/models/data/direct.model';
import { directStateFeatureKey } from '..';

const selectDirectState = createFeatureSelector<Direct[]>(directStateFeatureKey);

export const DirectSelectors = {
  selectDirects: createSelector(selectDirectState, (state: Direct[]) => state),
  selectDirectMessages: createSelector(
    selectDirectState,
    (state: Direct[], props: { targetId: number }) => {
      let direct = state.find((direct) => direct.targetId == props.targetId);
      if (direct == undefined) return [];
      if (direct.messages == undefined) return [];
      return direct.messages;
    }
  )
};
