import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css']
})
export class FollowersComponent implements OnInit {
  user: User;

  constructor(private store: Store<AppState>) {
    this.user = {
      id: 1,
      name: 'Peshawa Aziz',
      username: 'peshawa',
      isPublic: 0
    };
  }

  ngOnInit(): void {}
}
