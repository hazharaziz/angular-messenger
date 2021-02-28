import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import AuthActions from 'src/app/actions/auth.actions';
import { User } from 'src/app/models/data/user.model';
import { Request } from 'src/app/models/requests/request.model';
import { AppState } from 'src/app/state/app.state';
import { CustomValidator } from 'src/app/utils/customValidator';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  private user: Request<User>;
  signUpForm: FormGroup;
  hide: boolean;

  constructor(private store: Store<AppState>, private fb: FormBuilder) {
    this.signUpForm = this.fb.group({
      name: ['', [CustomValidator.ValidateString(3, 40), Validators.required]],
      username: [
        '',
        [CustomValidator.ValidateString(3, 20), Validators.required]
      ],
      password: [
        '',
        [CustomValidator.ValidateString(4, 16), Validators.required]
      ]
    });
    this.user = {};
    this.hide = true;
  }

  ngOnInit(): void {}

  onSubmit = (): void => {
    this.user.data = this.signUpForm.value;
    this.store.dispatch(AuthActions.SignUpRequest(this.user));
  };

  togglePassword = () => {
    this.hide = !this.hide;
  };

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
