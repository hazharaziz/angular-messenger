import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';
import { RelationActions } from 'src/app/store/actions/relation.actinos';
import { RelationSelectors } from 'src/app/store/selectors/relation.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-received-requests',
  templateUrl: './received-requests.component.html',
  styleUrls: ['./received-requests.component.css']
})
export class ReceivedRequestsComponent implements OnInit {
  receivedRequests$: Observable<User[]> = this.store.select(
    RelationSelectors.selectReceivedRequests
  );

  constructor(private store: Store<AppState>) {}

  ngOnInit(): void {
    this.fetchReceivedRequests();
  }

  fetchReceivedRequests() {
    this.store.dispatch(RelationActions.GetRequestsReceivedRequest());
  }

  acceptRequest(userId: number) {
    this.store.dispatch(RelationActions.AcceptRequestRequest({ userId }));
  }

  rejectRequest(userId: number) {
    this.store.dispatch(RelationActions.RejectRequestRequest({ userId }));
  }
}
