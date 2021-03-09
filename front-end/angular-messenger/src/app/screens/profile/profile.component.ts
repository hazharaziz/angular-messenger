import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  constructor(private store: Store<AppState>) {}

  ngOnInit(): void {}
}
