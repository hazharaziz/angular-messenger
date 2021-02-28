import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  Validators,
  AbstractControl,
  FormBuilder
} from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Login } from 'src/app/models/data/login.model';
import { Request } from 'src/app/models/requests/request.model';
import { AuthService } from 'src/app/services/api/auth.service';
import AuthActions from '../../../actions/auth.actions';
import { CustomValidator } from 'src/app/utils/customValidator';
import { AppState } from 'src/app/state/app.state';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private user: Request<Login>;
  loginForm: FormGroup;
  showWarning: boolean;
  hide: boolean;

  constructor(private store: Store<AppState>, private fb: FormBuilder) {
    this.user = {
      data: {
        username: '',
        password: ''
      }
    };
    this.loginForm = this.fb.group({
      username: [
        '',
        [CustomValidator.ValidateString(3, 20), Validators.required]
      ],
      password: [
        '',
        [CustomValidator.ValidateString(4, 16), Validators.required]
      ]
    });
    this.showWarning = false;
    this.hide = true;
  }

  ngOnInit = (): void => {};

  onSubmit = (): void => {
    if (!this.loginForm.valid) {
      this.showWarning = true;
      return;
    }
    this.showWarning = false;
    this.user.data = this.loginForm.value;
    this.store.dispatch(AuthActions.LoginRequest(this.user));
  };

  togglePassword = () => {
    this.hide = !this.hide;
  };

  get username(): AbstractControl {
    return this.loginForm.get('username');
  }
  get password(): AbstractControl {
    return this.loginForm.get('password');
  }
}
