import { ApiConfig } from "../index";

interface RefreshTokenResponse {
  authSuccess: boolean;
  accessToken: string;
  refreshToken: string;
}

const refreshTokenApi = (refreshToken: string, onError: (e: unknown) => void): ApiConfig<undefined> => {
  return {
    method: "post",
    url: "/api/Auth/RefreshToken",
    params: undefined,
    config: {
      headers: {
        Authorization: "Bearer " + refreshToken,
      },
    },
    onError: onError,
  } as ApiConfig<undefined>;
};

export { RefreshTokenResponse, refreshTokenApi };
