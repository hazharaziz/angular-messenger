import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DirectComponent } from './screens/direct/direct.component';
import { GeneralComponent } from './screens/general/general.component';
import { GroupComponent } from './screens/group/group.component';
import { HomeComponent } from './screens/home/home.component';
import { LoginComponent } from './screens/login/login.component';
import { PageNotFoundComponent } from './screens/page-not-found/page-not-found.component';
import { ProfileComponent } from './screens/profile/profile.component';
import { SearchComponent } from './screens/search/search.component';
import { SignupComponent } from './screens/signup/signup.component';
import { AuthGuard } from './services/guards/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'sign-up', component: SignupComponent },
  {
    path: 'home',
    component: HomeComponent,
    children: [
      { path: '', redirectTo: 'general', pathMatch: 'full' },
      { path: 'general', component: GeneralComponent },
      { path: 'search', component: SearchComponent },
      { path: 'direct', component: DirectComponent },
      { path: 'group', component: GroupComponent },
      { path: 'profile', component: ProfileComponent }
    ]
  },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  declarations: []
})
export class AppRoutingModule {}
