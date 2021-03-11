import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Group } from 'src/app/models/data/group.model';
import { Message } from 'src/app/models/data/message.model';
import { User } from 'src/app/models/data/user.model';
import { GroupAPI } from 'src/app/models/interfaces/group.api';
import { API_URL } from 'src/secrets';
import { HttpService } from '../http-service/http.service';

@Injectable({
  providedIn: 'root'
})
export class GroupService implements GroupAPI {
  constructor(private http: HttpClient, private httpService: HttpService) {}

  getGroupsRequest(): Observable<Group[]> {
    return this.http.get<Group[]>(API_URL + '/groups', {
      headers: this.httpService.authorizationHeader()
    });
  }

  getGroupInfoRequest(groupId: number): Observable<Group> {
    return this.http.get<Group>(API_URL + `/groups/${groupId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  getAvailableFriends(groupId: number): Observable<User[]> {
    return this.http.get<User[]>(API_URL + `groups/${groupId}/friends`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  createGroupRequest(group: Group): Observable<string> {
    return this.http.post<string>(API_URL + '/groups', group, {
      headers: this.httpService.authorizationHeader()
    });
  }

  editGroupRequest(groupId: string, group: Group): Observable<string> {
    return this.http.put<string>(API_URL + `/groups/${groupId}`, group, {
      headers: this.httpService.authorizationHeader()
    });
  }

  deleteGroupRequest(groupId: number): Observable<string> {
    return this.http.delete<string>(API_URL + `/groups/${groupId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  addMemberToGroupRequest(groupId: number, members: number[]): Observable<string> {
    return this.http.post<string>(API_URL + `/groups/${groupId}/add-member`, members, {
      headers: this.httpService.authorizationHeader()
    });
  }

  removeMemberFromGroupRequest(groupId: number, memberId: number): Observable<string> {
    return this.http.delete<string>(API_URL + `/groups/${groupId}/add-member/${memberId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  getGroupMessages(groupId: number): Observable<Message[]> {
    return this.http.get<Message[]>(API_URL + `/groups/${groupId}/messages`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  sendGroupMessageRequest(groupId: number, message: Message): Observable<string> {
    return this.http.post<string>(API_URL + `/groups/${groupId}/messages`, message, {
      headers: this.httpService.authorizationHeader()
    });
  }

  editGroupMessageRequest(
    groupId: number,
    messageId: number,
    message: Message
  ): Observable<string> {
    return this.http.put<string>(API_URL + `/groups/${groupId}/messages/${messageId}`, message, {
      headers: this.httpService.authorizationHeader()
    });
  }

  deleteGroupMessageRequest(groupId: number, messageId: number): Observable<string> {
    return this.http.delete<string>(API_URL + `/groups/${groupId}/messages/${messageId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  clearGroupChatHistoryRequest(groupId: number): Observable<string> {
    return this.http.delete<string>(API_URL + `/groups/${groupId}/messages`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  leaveGroupRequest(groupId: number): Observable<string> {
    return this.http.delete<string>(API_URL + `/groups/${groupId}/leave`, {
      headers: this.httpService.authorizationHeader()
    });
  }
}
