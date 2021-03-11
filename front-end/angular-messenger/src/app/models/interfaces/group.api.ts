import { Observable } from 'rxjs';
import { Group } from '../data/group.model';
import { Message } from '../data/message.model';
import { User } from '../data/user.model';

export interface GroupAPI {
  getGroupsRequest(): Observable<Group[]>;
  getGroupInfoRequest(groupId: number): Observable<Group>;
  getAvailableFriends(groupId: number): Observable<User[]>;
  createGroupRequest(group: Group): Observable<string>;
  editGroupRequest(groupId: string, group: Group): Observable<string>;
  deleteGroupRequest(groupId: number): Observable<string>;
  addMemberToGroupRequest(groupId: number, members: number[]): Observable<string>;
  removeMemberFromGroupRequest(groupId: number, memberId: number): Observable<string>;
  getGroupMessages(groupId: number): Observable<Message[]>;
  sendGroupMessageRequest(groupId: number, message: Message): Observable<string>;
  editGroupMessageRequest(groupId: number, messageId: number, message: Message): Observable<string>;
  deleteGroupMessageRequest(groupId: number, messageId: number): Observable<string>;
  clearGroupChatHistoryRequest(groupId: number): Observable<string>;
  leaveGroupRequest(groupId: number): Observable<string>;
}
