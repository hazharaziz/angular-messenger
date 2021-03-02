import { Injectable, OnInit } from '@angular/core';
import { CanActivate } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { AppState } from 'src/app/store';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements OnInit, CanActivate {
  constructor(private store: Store<AppState>, private jwtHelper: JwtHelperService) {}

  ngOnInit(): void {}

  canActivate(): boolean | Observable<boolean> {
    let token = '';
    this.store.select(AuthSelectors.selectToken).subscribe((data) => (token = data));
    return (
      this.jwtHelper.isTokenExpired(token) && this.store.select(AuthSelectors.selectisLoggedIn)
    );
  }
}
