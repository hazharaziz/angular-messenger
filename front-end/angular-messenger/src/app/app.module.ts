import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StoreModule } from '@ngrx/store';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSliderModule } from '@angular/material/slider';
import { MatFormFieldModule } from '@angular/material/form-field';
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
import { HomeComponent } from './screens/home/home.component';
import { fromEffects } from './effects';
import { reducers, metaReducers } from './store';
import { JwtModule } from '@auth0/angular-jwt';
import { ToastrModule } from 'ngx-toastr';
import { GeneralComponent } from './screens/general/general.component';
import { SearchComponent } from './screens/search/search.component';
import { ProfileComponent } from './screens/profile/profile.component';
import { DirectComponent } from './screens/direct/direct.component';
import { GroupComponent } from './screens/group/group.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    InputFormComponent,
    SignupComponent,
    PageNotFoundComponent,
    HomeComponent,
    GeneralComponent,
    SearchComponent,
    ProfileComponent,
    DirectComponent,
    GroupComponent
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
    MatSliderModule,
    MatFormFieldModule,
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production
    }),
    EffectsModule.forRoot([fromEffects.LoginEffects, fromEffects.SignUpEffects]),
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
