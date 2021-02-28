import { Observable } from 'rxjs';
import { Login } from 'src/app/models/data/login.model';
import { User } from 'src/app/models/data/user.model';
import { Request } from 'src/app/models/requests/request.model';
import { Response } from 'src/app/models/responses/response.model';

export interface AuthAPI {
  login(user: Request<Login>): Observable<Response<User>>;
  signUp(user: Request<User>): Observable<any>;
}
