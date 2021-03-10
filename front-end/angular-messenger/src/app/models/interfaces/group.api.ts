import { Observable } from 'rxjs';
import { Group } from '../data/group.model';

export interface GroupAPI {
  getGroupsRequest(): Observable<Group[]>;
}
