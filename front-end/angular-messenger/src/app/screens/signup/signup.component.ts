import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';

import { AuthActions } from 'src/app/store/actions/auth.actions';
import { User } from 'src/app/models/data/user.model';
import { Request } from '../../models';
import { CustomValidator } from 'src/app/utils/custom-validator';
import { AppState } from 'src/app/store';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  user: Request<User>;
  signUpForm: FormGroup;

  constructor(private store: Store<AppState>, private fb: FormBuilder) {
    this.user = {};
    this.signUpForm = this.fb.group({
      name: ['', [CustomValidator.ValidateString(3, 40), Validators.required]],
      username: ['', [CustomValidator.ValidateString(3, 20), Validators.required]],
      password: ['', [CustomValidator.ValidateString(4, 16), Validators.required]]
    });
  }

  ngOnInit(): void {
    this.store.dispatch(AuthActions.ClearCredentials());
  }

  onSubmit(): void {
    this.user.data = this.signUpForm.value;
    this.store.dispatch(AuthActions.SignUpRequest(this.user));
  }

  get name(): AbstractControl {
    return this.signUpForm.get('name');
  }
  get username(): AbstractControl {
    return this.signUpForm.get('username');
  }
  get password(): AbstractControl {
    return this.signUpForm.get('password');
  }
}
