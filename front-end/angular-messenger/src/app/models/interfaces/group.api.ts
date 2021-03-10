import { Observable } from 'rxjs';
import { Group } from '../data/group.model';
import { User } from '../data/user.model';

export interface GroupAPI {
  getGroupsRequest(): Observable<Group[]>;
  getGroupInfoRequest(groupId: number): Observable<Group>;
  getAvailableFriends(groupId: number): Observable<User[]>;
}
