import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import { User } from 'src/app/models/data/user.model';
import { Request } from 'src/app/models/requests/request.model';
import { CustomValidator } from 'src/app/utils/customValidator';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  private user: Request<User>;
  signUpForm: FormGroup;
  showWarning: boolean;
  hide: boolean;

  constructor(private fb: FormBuilder) {
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
    this.showWarning = false;
    this.hide = true;
  }

  ngOnInit(): void {}

  onSubmit = () => {
    if (!this.signUpForm.valid) {
      this.showWarning = true;
      return;
    }
    this.showWarning = false;
    this.user.payload = {
      ...this.signUpForm.value,
      isPublic: 1
    };
    console.log(JSON.stringify(this.user.payload, undefined, 2));
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
