import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  AbstractControl
} from '@angular/forms';
import { Login } from 'src/app/models/requests/login.model';
import { Request } from 'src/app/models/requests/request.model';
import { CustomValidator } from 'src/app/utils/customValidator';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: Request<Login>;

  loginForm = new FormGroup({
    username: new FormControl('', [
      CustomValidator.ValidateString(3, 20),
      Validators.required
    ]),
    password: new FormControl('', [
      CustomValidator.ValidateString(4, 16),
      Validators.required
    ])
  });

  constructor() {
    this.user = {
      payload: {
        username: '',
        password: ''
      }
    };
  }

  ngOnInit(): void {}

  onSubmit() {
    this.user.payload = {
      username: this.username.value,
      password: this.password.value
    };

    console.log(JSON.stringify(this.user.payload, undefined, 2));
  }

  get username(): AbstractControl {
    return this.loginForm.get('username');
  }
  get password(): AbstractControl {
    return this.loginForm.get('password');
  }

  get validUsername(): boolean {
    return this.username.valid || this.username.value.length == 0;
  }

  get validPassword(): boolean {
    return this.password.valid || this.password.value.length == 0;
  }
}
