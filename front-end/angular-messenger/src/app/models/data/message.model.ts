export type Message = {
  id?: number;
  groupId?: number;
  directId?: number;
  targetId?: number;
  composerId?: number;
  composerName?: string;
  replyToId?: number;
  replyToName?: string;
  text: string;
  dateTime?: any;
};
