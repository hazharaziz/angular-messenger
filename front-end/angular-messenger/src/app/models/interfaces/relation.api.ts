import { Observable } from 'rxjs';
import { User } from '../data/user.model';

export interface RelationAPI {
  getFollowersRequest(): Observable<User[]>;
  removeFollowerRequest(followerId: number): Observable<string>;
  getFollowingsRequest(): Observable<User[]>;
  unfollowRequest(followingId: number): Observable<string>;
  getRequestsSentRequest(): Observable<User[]>;
  acceptRequestRequest(userId: number): Observable<string>;
}
