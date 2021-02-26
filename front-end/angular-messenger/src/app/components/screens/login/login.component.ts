import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  AbstractControl,
  FormBuilder
} from '@angular/forms';
import { Login } from 'src/app/models/data/login.model';
import { Request } from 'src/app/models/requests/request.model';
import { CustomValidator } from 'src/app/utils/customValidator';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private user: Request<Login>;
  loginForm: FormGroup;
  showWarning: boolean;

  constructor(private fb: FormBuilder) {
    this.user = {
      payload: {
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
  }

  ngOnInit = (): void => {};

  onSubmit = () => {
    if (!this.loginForm.valid) {
      this.showWarning = true;
      return;
    }
    this.showWarning = false;
    this.user.payload = this.loginForm.value;
    console.log(JSON.stringify(this.user.payload, undefined, 2));
  };

  get username(): AbstractControl {
    return this.loginForm.get('username');
  }
  get password(): AbstractControl {
    return this.loginForm.get('password');
  }
}
