import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';

import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';
import { ProfileActions } from 'src/app/store/actions/profile.actions';
import { CustomValidator } from 'src/app/utils/custom-validator';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {
  profileFormGroup: FormGroup;
  isPublicAccount: boolean;
  userParams: User = this.getRouteParams();

  constructor(
    private store: Store<AppState>,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) {
    this.profileFormGroup = this.fb.group({
      name: [this.userParams.name, [CustomValidator.ValidateString(3, 40), Validators.required]],
      username: [
        this.userParams.username,
        [CustomValidator.ValidateString(3, 20), Validators.required]
      ]
    });
    this.isPublicAccount = this.userParams.isPublic == 1;
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.profileFormGroup.invalid) {
      return;
    }
    let username = this.username.value;
    let name = this.name.value;
    let isPublic = this.isPublicAccount ? 1 : 0;
    let unChanged: boolean =
      username === this.userParams.username &&
      name === this.userParams.name &&
      isPublic === this.userParams.isPublic;
    if (unChanged) return;
    this.store.dispatch(
      ProfileActions.EditProfileRequest({
        username,
        name,
        isPublic
      })
    );
  }

  getRouteParams(): User {
    let user: User = {};
    this.route.params.subscribe((params) => {
      user = {
        ...user,
        name: params['name'],
        username: params['username'],
        isPublic: Number(params['access'])
      };
    });
    return user;
  }

  get username(): AbstractControl {
    return this.profileFormGroup.get('username');
  }

  get name(): AbstractControl {
    return this.profileFormGroup.get('name');
  }
}
