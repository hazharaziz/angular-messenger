import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddMemberComponent } from './screens/add-member/add-member.component';
import { ChangePasswordComponent } from './screens/change-password/change-password.component';
import { CreateGroupComponent } from './screens/create-group/create-group.component';
import { DirectComponent } from './screens/direct/direct.component';
import { EditGroupComponent } from './screens/edit-group/edit-group.component';
import { EditProfileComponent } from './screens/edit-profile/edit-profile.component';
import { FollowersComponent } from './screens/followers/followers.component';
import { FollowingsComponent } from './screens/followings/followings.component';
import { GeneralChatComponent } from './screens/general-chat/general-chat.component';
import { GroupChatComponent } from './screens/group-chat/group-chat.component';
import { GroupInfoComponent } from './screens/group-info/group-info.component';
import { GroupsComponent } from './screens/groups/groups.component';
import { LoginComponent } from './screens/login/login.component';
import { PageNotFoundComponent } from './screens/page-not-found/page-not-found.component';
import { ProfileComponent } from './screens/profile/profile.component';
import { ReceivedRequestsComponent } from './screens/received-requests/received-requests.component';
import { SearchComponent } from './screens/search/search.component';
import { SentRequestsComponent } from './screens/sent-requests/sent-requests.component';
import { SignupComponent } from './screens/signup/signup.component';
import { AuthGuard } from './services/guards/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'sign-up', component: SignupComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'general', component: GeneralChatComponent, canActivate: [AuthGuard] },
  { path: 'search', component: SearchComponent, canActivate: [AuthGuard] },
  { path: 'direct', component: DirectComponent, canActivate: [AuthGuard] },
  {
    path: 'groups',
    component: GroupsComponent,
    children: [
      { path: 'create', component: CreateGroupComponent },
      {
        path: 'info/:id',
        component: GroupInfoComponent,
        children: [
          { path: 'edit/:id/:name/:access', component: EditGroupComponent },
          { path: 'add-member/:id', component: AddMemberComponent }
        ]
      },
      { path: 'chat/:id/:name/:creator', component: GroupChatComponent }
    ]
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'edit/:name/:username/:access',
        component: EditProfileComponent,
        children: [{ path: 'change-pass', component: ChangePasswordComponent }]
      },
      {
        path: 'followers',
        component: FollowersComponent
      },
      {
        path: 'followings',
        component: FollowingsComponent
      },
      {
        path: 'sent-requests',
        component: SentRequestsComponent
      },
      {
        path: 'received-requests',
        component: ReceivedRequestsComponent
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
