import { User } from './user.model';

export type Relation = {
  followers?: User[];
  followings?: User[];
  receivedRequest?: User[];
  sentRequests?: User[];
  friends?: User[];
};
