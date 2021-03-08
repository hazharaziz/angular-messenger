import { LoginEffects } from './authEffects/login.effects';
import { SignUpEffects } from './authEffects/sign-up.effects';
import { GetGeneralChatMessagesEffects } from './generalChatEffects/get-general-chat-messages.effects';
import { SendMessageEffects } from './generalChatEffects/send-message.effects';

export const fromEffects = {
  LoginEffects,
  SignUpEffects,
  GetGeneralChatMessagesEffects,
  SendMessageEffects
};
