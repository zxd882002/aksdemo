import { ApiConfig } from "../index";
import { Guid } from "guid-typescript";

interface RefreshTokenRequest {
  header: {
    requestId: string;
  };
}
interface RefreshTokenResponse {
  header: {
    responseId: string;
    statusCode: number;
  };
  authSuccess: boolean;
  accessToken: string;
  refreshToken: string;
}

const refreshTokenApi = (refreshToken: string, onError: (e: unknown) => void): ApiConfig<RefreshTokenRequest> => {
  return {
    method: "post",
    url: "/api/Auth/RefreshToken",
    params: {
      header: {
        requestId: Guid.create().toString(),
      },
    } as RefreshTokenRequest,
    config: {
      headers: {
        Authorization: "Bearer " + refreshToken,
      },
    },
    onError: onError,
  } as ApiConfig<RefreshTokenRequest>;
};

export { RefreshTokenRequest, RefreshTokenResponse, refreshTokenApi };
