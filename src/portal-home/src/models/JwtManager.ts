import { useStore } from "@/stores";
import { axiosCallApi } from "@/services";
import { RefreshTokenRequest, RefreshTokenResponse, refreshTokenApi } from "@/services/auth/refreshTokenApi";

class JwtManager {
  private store;
  constructor() {
    this.store = useStore();
  }

  setAccessToken(token: string) {
    const payloadBase64 = token.split(".")[1];
    const payloadString = Buffer.from(payloadBase64, "base64").toString("binary");
    const payload = JSON.parse(payloadString);
    this.store.accessTokenExp = new Date(payload.exp * 1000);
    localStorage.setItem("AccessToken", token);
  }

  getAccessToken() {
    if (this.store.accessTokenExp < new Date()) return undefined;
    const item = localStorage.getItem("AccessToken");
    if (item) return item as string;
    return undefined;
  }

  setRefreshToken(token: string) {
    const payloadBase64 = token.split(".")[1];
    const payloadString = Buffer.from(payloadBase64, "base64").toString("binary");
    const payload = JSON.parse(payloadString);
    this.store.refreshTokenExp = new Date(payload.exp * 1000);
    localStorage.setItem("RefreshToken", token);
  }

  getRefreshToken() {
    if (this.store.refreshTokenExp < new Date()) return undefined;
    const item = localStorage.getItem("RefreshToken");
    if (item) return item as string;
    return undefined;
  }

  async generateAccessTokenFromRefreshToken() {
    const current = new Date();
    if (this.store.accessTokenExp < current && current < this.store.refreshTokenExp) {
      const refreshToken = this.getRefreshToken();
      if (refreshToken) {
        const data = await axiosCallApi<RefreshTokenRequest, RefreshTokenResponse>(
          refreshTokenApi(refreshToken, (e) => {
            throw e;
          })
        );
        if (data && data.authSuccess) {
          this.setAccessToken(data.accessToken);
          this.setRefreshToken(data.refreshToken);
        }
        return this.getAccessToken();
      }
    }
    return undefined;
  }

  async isAuthenticated() {
    let accessToken = this.getAccessToken();
    if (!accessToken) accessToken = await this.generateAccessTokenFromRefreshToken();
    return accessToken != undefined;
  }
}

const jwtManager = new JwtManager();
export { jwtManager };
