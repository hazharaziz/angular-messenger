import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/data/user.model';

import { AppState } from 'src/app/store';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  user$: Observable<User>;

  constructor(private store: Store<AppState>) {}

  ngOnInit(): void {
    this.user$ = this.store.select(AuthSelectors.selectUser);
  }
}
