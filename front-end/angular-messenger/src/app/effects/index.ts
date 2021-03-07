import { LoginEffects } from './authEffects/login.effects';
import { SignUpEffects } from './authEffects/sign-up.effects';
import { GetChatMessagesEffects } from './generalChatEffects/get-chat-messages.effects';

export const fromEffects = { LoginEffects, SignUpEffects, GetChatMessagesEffects };
