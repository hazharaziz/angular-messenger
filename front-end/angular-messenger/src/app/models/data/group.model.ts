import { Message } from './message.model';
import { User } from './user.model';

export type Group = {
  groupId?: number;
  groupName?: string;
  creatorId?: number;
  creatorName?: string;
  membersCount?: number;
  addMemberAccess?: number;
  members?: User[];
  messages?: Message[];
};