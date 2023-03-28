import type { ApiConfig } from "..";

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
      traceId: traceId,
      passwordHash: passwordHash,
    } as AuthenticateRequest,
    config: undefined,
    onError: onError,
  } as ApiConfig<AuthenticateRequest>;
};

export { type AuthenticateRequest, type AuthenticateResponse, authenticateApi };
