import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';
import { RelationActions } from 'src/app/store/actions/relation.actinos';
import { RelationSelectors } from 'src/app/store/selectors/relation.selectors';

@Component({
  selector: 'app-followings',
  templateUrl: './followings.component.html',
  styleUrls: ['./followings.component.css']
})
export class FollowingsComponent implements OnInit {
  followings$: Observable<User[]> = this.store.select(RelationSelectors.selectFollowings);

  constructor(private store: Store<AppState>) {}

  ngOnInit(): void {
    this.fetchFollowings();
  }

  fetchFollowings() {
    this.store.dispatch(RelationActions.GetFollowingsRequest());
  }

  unfollowUser(followingId: number) {
    this.store.dispatch(RelationActions.UnfollowRequest({ followingId }));
  }
}
