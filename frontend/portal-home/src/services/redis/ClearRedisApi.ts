import type { ApiConfig } from "..";
const clearRedisApi = (onError: (e: unknown) => void): ApiConfig<undefined> => {
  return {
    method: "post",
    url: "/api/redis/ClearCache",
    params: undefined,
    config: undefined,
    onError: onError,
  } as ApiConfig<undefined>;
};

export { clearRedisApi };
