import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StoreModule } from '@ngrx/store';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { EffectsModule } from '@ngrx/effects';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './core/app.component';
import { LoginComponent } from './screens/login/login.component';
import { InputFormComponent } from './components/input-form/input-form.component';
import { AppRoutingModule } from './app-routing.module';
import { SignupComponent } from './screens/signup/signup.component';
import { PageNotFoundComponent } from './screens/page-not-found/page-not-found.component';
import { environment } from 'src/environments/environment';
import { effects } from './effects';
import { reducers, metaReducers } from './store';
import { JwtModule } from '@auth0/angular-jwt';
import { ToastrModule } from 'ngx-toastr';
import { GeneralComponent } from './screens/general/general.component';
import { SearchComponent } from './screens/search/search.component';
import { ProfileComponent } from './screens/profile/profile.component';
import { DirectComponent } from './screens/direct/direct.component';
import { GroupComponent } from './screens/group/group.component';
import { MessageContainerComponent } from './components/message-container/message-container.component';
import { LeftNavBarComponent } from './components/left-nav-bar/left-nav-bar.component';
import { EditProfileComponent } from './screens/edit-profile/edit-profile.component';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { ChangePasswordComponent } from './screens/change-password/change-password.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    InputFormComponent,
    SignupComponent,
    PageNotFoundComponent,
    GeneralComponent,
    SearchComponent,
    ProfileComponent,
    DirectComponent,
    GroupComponent,
    MessageContainerComponent,
    LeftNavBarComponent,
    EditProfileComponent,
    ConfirmDialogComponent,
    ChangePasswordComponent
  ],
  imports: [
    BrowserModule,
    StoreModule.forRoot(reducers, {
      metaReducers,
      runtimeChecks: {
        strictStateImmutability: true,
        strictActionImmutability: true
      }
    }),
    !environment.production ? StoreDevtoolsModule.instrument() : [],
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('access_token');
        },
        allowedDomains: ['localhost:4200', 'localhost:5000']
      }
    }),
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production
    }),
    EffectsModule.forRoot(effects),
    ToastrModule.forRoot({
      maxOpened: 1,
      preventDuplicates: true,
      progressBar: true,
      progressAnimation: 'decreasing',
      closeButton: true,
      positionClass: 'toast-top-left'
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
