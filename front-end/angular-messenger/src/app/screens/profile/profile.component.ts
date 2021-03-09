import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';
import { ProfileActions } from 'src/app/store/actions/profile.actions';
import { ProfileSelectors } from 'src/app/store/selectors/profile.selectors';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profile$: Observable<User> = this.store.select(ProfileSelectors.selectUser);

  constructor(private store: Store<AppState>, private router: Router) {}

  ngOnInit(): void {
    this.fetchProfile();
  }

  fetchProfile(): void {
    this.store.dispatch(ProfileActions.GetProfileRequest());
  }
}
