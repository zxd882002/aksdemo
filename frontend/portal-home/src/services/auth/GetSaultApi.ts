import type { ApiConfig } from "..";
interface GetSaltResponse {
  salt: string;
  traceId: string;
}

const getSaultApi = (onError: (e: unknown) => void): ApiConfig<undefined> => {
  return {
    method: "get",
    url: "/api/Auth/GetSalt",
    params: undefined,
    config: undefined,
    onError: onError,
  } as ApiConfig<undefined>;
};

export { type GetSaltResponse, getSaultApi };
