import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Group } from 'src/app/models/data/group.model';
import { AppState } from 'src/app/store';
import { GroupActions } from 'src/app/store/actions/group.actions';
import { CustomValidator } from 'src/app/utils/custom-validator';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-edit-group',
  templateUrl: './edit-group.component.html',
  styleUrls: ['./edit-group.component.css']
})
export class EditGroupComponent implements OnInit {
  groupParams: Group = this.getGroupParams();
  editGroupForm: FormGroup;
  addMemberAccess: boolean;

  constructor(
    private store: Store<AppState>,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.editGroupForm = this.fb.group({
      groupName: [
        this.groupParams.groupName,
        [CustomValidator.ValidateString(3, 40), Validators.required]
      ]
    });
    this.addMemberAccess = this.groupParams.addMemberAccess == 1 ? true : false;
  }

  ngOnInit(): void {}

  onSubmit(): void {
    if (this.editGroupForm.invalid) return;
    let name: string = this.groupName.value;
    let access: number = this.addMemberAccess ? 1 : 0;
    if (name == this.groupParams.groupName && access == this.groupParams.addMemberAccess) return;
    this.store.dispatch(
      GroupActions.EditGroupRequest({
        groupId: this.groupParams.groupId,
        groupName: name,
        addMemberAccess: access
      })
    );
    setTimeout(() => {
      this.router.navigate(['/groups/info', this.groupParams.groupId]);
    }, 200);
  }

  getGroupParams(): Group {
    let params = this.route.snapshot.paramMap;
    return {
      groupId: Number(params.get('id')),
      groupName: params.get('name'),
      addMemberAccess: Number(params.get('access'))
    };
  }

  get groupName(): AbstractControl {
    return this.editGroupForm.get('groupName');
  }
}
