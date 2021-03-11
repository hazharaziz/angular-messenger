import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Group } from 'src/app/models/data/group.model';
import { AppState } from 'src/app/store';
import { GroupActions } from 'src/app/store/actions/group.actions';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { GroupSelectors } from 'src/app/store/selectors/group.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-group-info',
  templateUrl: './group-info.component.html',
  styleUrls: ['./group-info.component.css']
})
export class GroupInfoComponent implements OnInit {
  groupIdParam: number;
  auth$: Observable<number> = this.store.select(AuthSelectors.selectUserId);
  groupInfo$: Observable<Group> = this.store.select(GroupSelectors.selectGroupInfo, {
    groupId: Number(this.route.snapshot.paramMap.get('id'))
  });

  constructor(private store: Store<AppState>, private route: ActivatedRoute) {
    this.groupIdParam = Number(this.route.snapshot.paramMap.get('id'));
  }

  ngOnInit(): void {
    this.fetchGroupInfo(this.groupIdParam);
  }

  fetchGroupInfo(groupId: number): void {
    if (groupId != undefined) {
      this.store.dispatch(GroupActions.GetGroupInfoRequest({ groupId }));
    }
  }

  removeMember(id: number): void {
    this.store.dispatch(
      GroupActions.RemoveMemberFromGroupRequest({ groupId: this.groupIdParam, memberId: id })
    );
  }
}
