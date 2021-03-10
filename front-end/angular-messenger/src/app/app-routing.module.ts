import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChangePasswordComponent } from './screens/change-password/change-password.component';
import { DirectComponent } from './screens/direct/direct.component';
import { EditProfileComponent } from './screens/edit-profile/edit-profile.component';
import { FollowersComponent } from './screens/followers/followers.component';
import { GeneralComponent } from './screens/general/general.component';
import { GroupComponent } from './screens/group/group.component';
import { LoginComponent } from './screens/login/login.component';
import { PageNotFoundComponent } from './screens/page-not-found/page-not-found.component';
import { ProfileComponent } from './screens/profile/profile.component';
import { SearchComponent } from './screens/search/search.component';
import { SignupComponent } from './screens/signup/signup.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'sign-up', component: SignupComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'general', component: GeneralComponent },
  { path: 'search', component: SearchComponent },
  { path: 'direct', component: DirectComponent },
  { path: 'group', component: GroupComponent },
  {
    path: 'profile',
    component: ProfileComponent,
    children: [
      {
        path: 'edit/:name/:username/:access',
        component: EditProfileComponent,
        children: [{ path: 'change-pass', component: ChangePasswordComponent }]
      },
      {
        path: 'followers',
        component: FollowersComponent
      }
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
