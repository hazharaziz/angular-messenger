import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { JwtHelperService } from '@auth0/angular-jwt';
import { select, Store } from '@ngrx/store';

import { AppModule } from './app/app.module';
import { Request } from './app/models';
import { User } from './app/models/data/user.model';
import { AppState } from './app/store';
import { AuthActions } from './app/store/actions/auth.actions';
import { AuthSelectors } from './app/store/selectors/auth.selectors';
import { CronJob } from './app/utils/cronJob';
import { log } from './app/utils/logger';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}
platformBrowserDynamic()
  .bootstrapModule(AppModule)
  .then((module) => {
    // the following lines are for refreshing tokens
    // let store: Store<AppState> = module.injector.get(Store);
    // let jwtHelpler = module.injector.get(JwtHelperService);
    // CronJob.start('* */55 * * * *', () => {
    //   let isLoggedIn = false;
    //   store.pipe(select(AuthSelectors.selectisLoggedIn)).subscribe((data) => (isLoggedIn = data));
    //   if (!isLoggedIn) {
    //     return;
    //   }
    // });
  })
  .catch((err) => console.error(err));
