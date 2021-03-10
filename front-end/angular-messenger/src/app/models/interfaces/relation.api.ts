import { Observable } from 'rxjs';
import { User } from '../data/user.model';

export interface RelationAPI {
  getFollowersRequest(): Observable<User[]>;
}
