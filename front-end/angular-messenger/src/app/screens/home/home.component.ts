import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/data/user.model';

import { AppState } from 'src/app/store';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  childPath: string;
  constructor(private store: Store<AppState>, private route: ActivatedRoute) {
    this.childPath = '';
  }

  ngOnInit(): void {}

  changeRoute(url: string) {
    this.childPath = url;
    log(this.childPath);
  }
}
