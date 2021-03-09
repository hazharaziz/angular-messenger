import { Observable } from 'rxjs';
import { User } from '../data/user.model';

export interface ProfileAPI {
  getProfileRequest(): Observable<User>;
  editProfileRequest(editedUser: User): Observable<string>;
  changePasswordRequest(oldPassword: string, newPassword: string): Observable<string>;
}
