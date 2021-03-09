import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, AbstractControl, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';

import { Request } from '../../models';
import { CustomValidator } from 'src/app/utils/custom-validator';
import { User } from 'src/app/models/data/user.model';
import { AuthActions } from 'src/app/store/actions/auth.actions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  request: Request<User>;
  loginForm: FormGroup;
  showWarning: boolean;

  constructor(private store: Store<User>, private fb: FormBuilder, private router: Router) {
    this.request = {
      data: {
        username: '',
        password: ''
      }
    };
    this.loginForm = this.fb.group({
      username: ['', [CustomValidator.ValidateString(3, 20), Validators.required]],
      password: ['', [CustomValidator.ValidateString(4, 16), Validators.required]]
    });
    this.showWarning = false;
  }

  ngOnInit(): void {
    this.store.dispatch(AuthActions.ClearCredentials());
  }

  onSubmit(): void {
    if (!this.loginForm.valid) {
      this.showWarning = true;
      return;
    }
    this.showWarning = false;
    this.request.data = this.loginForm.value;
    this.store.dispatch(AuthActions.LoginRequest(this.request));
  }

  get username(): AbstractControl {
    return this.loginForm.get('username');
  }

  get password(): AbstractControl {
    return this.loginForm.get('password');
  }
}
