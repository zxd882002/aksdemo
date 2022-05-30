import { ApiConfig } from "../index";
import { Guid } from "guid-typescript";

interface AuthenticateRequest {
  traceId: string;
  passwordHash: string;
}
interface AuthenticateResponse {
  authSuccess: boolean;
  accessToken: string;
  refreshToken: string;
}

const authenticateApi = (
  traceId: string,
  passwordHash: string,
  onError: (e: unknown) => void
): ApiConfig<AuthenticateRequest> => {
  return {
    method: "post",
    url: "/api/Auth/Authenticate",
    params: {
      header: {
        requestId: Guid.create().toString(),
      },
      traceId: traceId,
      passwordHash: passwordHash,
    } as AuthenticateRequest,
    config: undefined,
    onError: onError,
  } as ApiConfig<AuthenticateRequest>;
};

export { AuthenticateRequest, AuthenticateResponse, authenticateApi };
