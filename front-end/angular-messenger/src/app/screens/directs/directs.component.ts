import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Direct } from 'src/app/models/data/direct.model';
import { AppState } from 'src/app/store';
import { DirectActions } from 'src/app/store/actions/direct.actions';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { DirectSelectors } from 'src/app/store/selectors/direct.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-direct',
  templateUrl: './directs.component.html',
  styleUrls: ['./directs.component.css']
})
export class DirectsComponent implements OnInit {
  auth$: Observable<number> = this.store.select(AuthSelectors.selectUserId);
  directs$: Observable<Direct[]> = this.store.select(DirectSelectors.selectDirects);

  constructor(private store: Store<AppState>, private router: Router) {}

  ngOnInit(): void {
    this.fetchUserDirects();
  }

  fetchUserDirects(): void {
    this.store.dispatch(DirectActions.GetDirectsRequest());
  }

  directChat(data: { targetId?: number; name?: string }): void {
    this.router.navigate(['/directs/chat', data.targetId, data.name]);
  }

  deleteDirect(directId: number): void {
    this.store.dispatch(DirectActions.RemoveDirectRequest({ directId }));
  }
}
