import { createFeatureSelector, createSelector, State } from '@ngrx/store';

import { Chat } from 'src/app/models/data/chat.model';
import { generalChatStateFeatureKey } from '..';

const selectChatState = createFeatureSelector<Chat[]>(generalChatStateFeatureKey);

export const GeneralChatSelectors = {
  selectChatMessages: createSelector(selectChatState, (state: Chat[]) => state)
};
