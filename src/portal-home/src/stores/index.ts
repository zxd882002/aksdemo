import { defineStore } from "pinia";

export const useStore = defineStore("main", {
  state: () => ({
    isAuthenticated: false,
    tokenExp: new Date(),
  }),
  actions: {
    setToken(token: string) {
      const payloadBase64 = token.split(".")[1];
      const payloadString = Buffer.from(payloadBase64, "base64").toString("binary");
      const payload = JSON.parse(payloadString);
      this.isAuthenticated = true;
      this.tokenExp = new Date(payload.exp * 1000);
      console.log(this.tokenExp);
      localStorage.setItem("BearerToken", token);
    },
  },
});
