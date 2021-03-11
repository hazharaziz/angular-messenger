import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { AppState } from 'src/app/store';
import { GroupActions } from 'src/app/store/actions/group.actions';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { CustomValidator } from 'src/app/utils/custom-validator';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.css']
})
export class CreateGroupComponent implements OnInit {
  auth$: Observable<number> = this.store.select(AuthSelectors.selectUserId);
  groupInfoForm: FormGroup;

  constructor(private store: Store<AppState>, private fb: FormBuilder) {
    this.groupInfoForm = fb.group({
      groupName: fb.control('', [CustomValidator.ValidateString(3, 40), Validators.required])
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.groupInfoForm.invalid) return;
    let userId = 0;
    this.auth$.subscribe((id) => (userId = id));
    this.store.dispatch(
      GroupActions.CreateGroupRequest({
        groupName: this.groupName.value,
        creatorId: userId
      })
    );
  }

  get groupName() {
    return this.groupInfoForm.get('groupName');
  }
}
