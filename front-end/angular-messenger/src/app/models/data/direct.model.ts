import { Chat } from './chat.model';

export type Direct = {
  directId?: number;
  targetId?: number;
  targetName?: string;
  messages?: Chat[];
};
