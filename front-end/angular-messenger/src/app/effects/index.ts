import { LoginEffects } from './authEffects/login.effects';
import { SignUpEffects } from './authEffects/sign-up.effects';
import { DeleteMessageEffects } from './generalChatEffects/delete-message.effects';
import { EditMessageEffects } from './generalChatEffects/edit-message.effects';
import { GetGeneralChatMessagesEffects } from './generalChatEffects/get-general-chat-messages.effects';
import { SendMessageEffects } from './generalChatEffects/send-message.effects';
import { ChangePasswordEffects } from './profileEffects/change-password.effects';
import { EditProfileEffects } from './profileEffects/edit-profile.effects';
import { GetProfileEffects } from './profileEffects/get-profile.effects';

export const effects = [
  LoginEffects,
  SignUpEffects,
  GetGeneralChatMessagesEffects,
  SendMessageEffects,
  EditMessageEffects,
  DeleteMessageEffects,
  GetProfileEffects,
  EditProfileEffects,
  ChangePasswordEffects
];
