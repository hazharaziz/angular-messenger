import { createFeatureSelector, createSelector } from '@ngrx/store';

import { Chat } from 'src/app/models/data/chat.model';
import { generalChatStateFeatureKey } from '..';

export const selectChatState = createFeatureSelector<Chat[]>(generalChatStateFeatureKey);

export const ChatSelectors = {
  selectChatMessages: createSelector(selectChatState, (state: Chat[]) => state)
};
