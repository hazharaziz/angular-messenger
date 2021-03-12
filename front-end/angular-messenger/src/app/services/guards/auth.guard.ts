import { Injectable, OnInit } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree
} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { AppState } from 'src/app/store';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { log } from 'src/app/utils/logger';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements OnInit, CanActivate {
  constructor(
    private store: Store<AppState>,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  canActivate(): boolean | Observable<boolean> {
    let authorized = false;
    this.store
      .select(AuthSelectors.selectToken)
      .subscribe((token) => (authorized = !this.jwtHelper.isTokenExpired(token)));
    this.store
      .select(AuthSelectors.selectisLoggedIn)
      .subscribe((isLoggedIn) => (authorized &&= isLoggedIn));

    if (!authorized) {
      this.router.navigate(['/login']).then(() => {
        return authorized;
      });
    }
    return authorized;
  }
}
