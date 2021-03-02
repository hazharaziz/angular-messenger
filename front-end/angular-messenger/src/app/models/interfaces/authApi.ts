import { Observable } from 'rxjs';

import { User } from 'src/app/models/data/user.model';
import { Request, Response } from '..';

export interface AuthAPI {
  login(user: Request<User>): Observable<Response<User>>;
  signUp(user: Request<User>): Observable<Response<User>>;
}
