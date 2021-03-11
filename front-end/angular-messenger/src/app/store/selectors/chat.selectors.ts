import { createFeatureSelector, createSelector, State } from '@ngrx/store';

import { Chat } from 'src/app/models/data/chat.model';
import { generalChatStateFeatureKey } from '..';

const selectChatState = createFeatureSelector<Chat[]>(generalChatStateFeatureKey);

export const GeneralChatSelectors = {
  selectChatMessages: createSelector(selectChatState, (state: Chat[]) => state),
  selectMessageComposerName: createSelector(selectChatState, (state: Chat[], messageId: number) => {
    let name = 'Deleted Message';
    state.forEach((chat) => {
      chat.messages.forEach((msg) => {
        if (msg.id == messageId) {
          name = msg.composerName;
        }
      });
    });
    return name;
  })
};
