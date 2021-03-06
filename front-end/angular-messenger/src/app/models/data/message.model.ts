export type Message = {
  id?: number;
  directId?: number;
  groupId?: number;
  composerId?: number;
  composerName?: string;
  replyToId?: number;
  replyToName?: string;
  text: string;
  dateTime?: any;
};
