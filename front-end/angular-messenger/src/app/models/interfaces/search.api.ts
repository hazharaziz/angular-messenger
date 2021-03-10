import { Observable } from 'rxjs';
import { User } from '../data/user.model';

export interface SearchAPI {
  searchRequest(query: string): Observable<User[]>;
}
