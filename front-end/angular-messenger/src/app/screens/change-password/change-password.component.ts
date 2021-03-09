import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store';
import { CustomValidator } from 'src/app/utils/custom-validator';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  passwordFormGroup: FormGroup;

  constructor(private store: Store<AppState>, private fb: FormBuilder) {
    this.passwordFormGroup = this.fb.group({
      currentPassword: ['', [CustomValidator.ValidateString(4, 16), Validators.required]],
      newPassword: ['', [CustomValidator.ValidateString(4, 16), Validators.required]]
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.passwordFormGroup.invalid) return;
    if (this.currentPassword.value == this.newPassword.value) return;
    log({
      currentPassword: this.currentPassword.value,
      newPassword: this.newPassword.value
    });
  }

  get currentPassword(): AbstractControl {
    return this.passwordFormGroup.get('currentPassword');
  }

  get newPassword(): AbstractControl {
    return this.passwordFormGroup.get('newPassword');
  }
}
