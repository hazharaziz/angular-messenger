export type Request<T> = {
  token?: string;
  data?: T;
};

export type Response<T> = {
  status?: number;
  token?: string;
  data?: T;
};
