export type Response<T> = {
  status?: number;
  token?: string;
  data?: T;
};
