import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';
import { RelationActions } from 'src/app/store/actions/relation.actinos';
import { RelationSelector } from 'src/app/store/selectors/relation.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css']
})
export class FollowersComponent implements OnInit {
  followers$: Observable<User[]> = this.store.select(RelationSelector.selectFollowers);

  constructor(private store: Store<AppState>) {}

  ngOnInit(): void {
    this.fetchFollowers();
  }

  fetchFollowers() {
    this.store.dispatch(RelationActions.GetFollowersRequest());
  }

  removeFollower(followerId: number) {
    this.store.dispatch(RelationActions.RemoveFollowerRequest({ followerId }));
  }
}
