import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';
import { GroupActions } from 'src/app/store/actions/group.actions';
import { GroupSelectors } from 'src/app/store/selectors/group.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-add-member',
  templateUrl: './add-member.component.html',
  styleUrls: ['./add-member.component.css']
})
export class AddMemberComponent implements OnInit {
  membersIds: number[] = [];
  groupIdParam: number = Number(this.route.snapshot.paramMap.get('id'));
  friends$: Observable<User[]> = this.store.select(GroupSelectors.selectAvailableFriends, {
    groupId: this.groupIdParam
  });

  constructor(private store: Store<AppState>, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.fetchFriends(this.groupIdParam);
  }

  fetchFriends(groupId: number): void {
    this.store.dispatch(GroupActions.GetAvailableFriendsRequest({ groupId }));
  }

  onAddMembers(): void {
    if (this.membersIds.length == 0) return;
    this.store.dispatch(
      GroupActions.AddMemberToGroupRequest({ groupId: this.groupIdParam, members: this.membersIds })
    );

    setTimeout(() => {
      this.fetchFriends(this.groupIdParam);
    }, 1000);
  }

  onSelect(id: number): void {
    let index = this.membersIds.findIndex((m) => m == id);
    if (index < 0) {
      this.membersIds.push(id);
    }
  }

  onDeSelect(id: number): void {
    let index = this.membersIds.findIndex((m) => m == id);
    if (index >= 0) {
      this.membersIds.splice(index, 1);
    }
  }
}
