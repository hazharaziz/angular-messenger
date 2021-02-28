import { HttpErrorResponse } from '@angular/common/http';
import { User } from '../models/data/user.model';

export interface AppState {
  isLoggedIn: boolean;
  token?: string;
  user: User;
  error?: HttpErrorResponse;
}
