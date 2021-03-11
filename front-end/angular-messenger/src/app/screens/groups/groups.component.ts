import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Group } from 'src/app/models/data/group.model';
import { AppState } from 'src/app/store';
import { GroupActions } from 'src/app/store/actions/group.actions';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { GroupSelectors } from 'src/app/store/selectors/group.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {
  auth$: Observable<number> = this.store.select(AuthSelectors.selectUserId);
  groups$: Observable<Group[]> = this.store.select(GroupSelectors.selectUserGroups);

  constructor(private store: Store<AppState>, private router: Router) {}

  ngOnInit(): void {
    this.fetchUserGroups();
  }

  fetchUserGroups(): void {
    this.store.dispatch(GroupActions.GetGroupsRequest());
  }

  groupInfo(id: number) {
    this.router.navigate(['/groups/info', id]);
  }

  leaveGroup(groupId: number): void {
    this.store.dispatch(GroupActions.LeaveGroupRequest({ groupId }));
  }

  deleteGroup(groupId: number): void {
    this.store.dispatch(GroupActions.DeleteGroupRequest({ groupId }));
  }
}
