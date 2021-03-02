export type User = {
  isLoggedIn?: boolean;
  token?: string;
  id?: number;
  username?: string;
  name?: string;
  password?: string;
  isPublic?: number;
  error?: string;
};
