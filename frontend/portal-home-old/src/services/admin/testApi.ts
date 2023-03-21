import { ApiConfig } from "../index";
const testApi = (method: string, url: string, param: any, onError: (e: unknown) => void): ApiConfig<any> => {
  return {
    method: method,
    url: url,
    params: param,
    config: undefined,
    onError: onError,
  } as ApiConfig<any>;
};

export { testApi };
