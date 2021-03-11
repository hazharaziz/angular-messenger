import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from 'src/app/models/data/user.model';

@Component({
  selector: 'app-user-item',
  templateUrl: './user-item.component.html',
  styleUrls: ['./user-item.component.css']
})
export class UserItemComponent implements OnInit {
  @Input() user: User;
  @Input() relationStatus:
    | 'netural'
    | 'request-sent'
    | 'request-received'
    | 'follower'
    | 'following'
    | 'member';
  @Input() isCreator?: boolean;
  @Input() canRemove?: boolean;

  @Output() follow: EventEmitter<number> = new EventEmitter<number>();
  @Output() cancel: EventEmitter<number> = new EventEmitter<number>();
  @Output() accept: EventEmitter<number> = new EventEmitter<number>();
  @Output() reject: EventEmitter<number> = new EventEmitter<number>();
  @Output() remove: EventEmitter<number> = new EventEmitter<number>();
  @Output() unfollow: EventEmitter<number> = new EventEmitter<number>();

  constructor() {}

  ngOnInit(): void {}

  followUser(id: number) {
    this.follow.emit(id);
  }

  cancelRequest(id: number) {
    this.cancel.emit(id);
  }

  acceptRequest(id: number) {
    this.accept.emit(id);
  }

  rejectRequest(id: number) {
    this.reject.emit(id);
  }

  removeUser(id: number) {
    this.remove.emit(id);
  }

  unfollowUser(id: number) {
    this.unfollow.emit(id);
  }
}
