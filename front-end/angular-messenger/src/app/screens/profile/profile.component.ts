import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { ConfirmDialogComponent } from 'src/app/components/confirm-dialog/confirm-dialog.component';
import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';
import { ProfileActions } from 'src/app/store/actions/profile.actions';
import { ProfileSelectors } from 'src/app/store/selectors/profile.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profile$: Observable<User> = this.store.select(ProfileSelectors.selectUser);

  constructor(
    private store: Store<AppState>,
    private router: Router,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.fetchProfile();
  }

  fetchProfile(): void {
    this.store.dispatch(ProfileActions.GetProfileRequest());
  }

  openModal() {
    const modalRef = this.modalService.open(ConfirmDialogComponent);
    modalRef.componentInstance.message = 'Are you sure of deleting your account?';
    modalRef.result
      .then((value) => {
        log(value);
      })
      .catch((error) => {
        log(error);
      });
  }
}
