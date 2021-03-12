import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';
import { RelationActions } from 'src/app/store/actions/relation.actinos';
import { RelationSelectors } from 'src/app/store/selectors/relation.selectors';

@Component({
  selector: 'app-sent-requests',
  templateUrl: './sent-requests.component.html',
  styleUrls: ['./sent-requests.component.css']
})
export class SentRequestsComponent implements OnInit {
  sentRequests$: Observable<User[]> = this.store.select(RelationSelectors.selectSentRequests);

  constructor(private store: Store<AppState>) {}

  ngOnInit(): void {
    this.fetchSentRequest();
  }

  fetchSentRequest() {
    this.store.dispatch(RelationActions.GetRequestsSentRequest());
  }

  cancelRequest(userId: number) {
    this.store.dispatch(RelationActions.CancelRequestRequest({ userId }));
  }
}
