import { defineStore } from "pinia";

export const useStore = defineStore("main", {
  state: () => ({
    isAuthenticated: false,
    tokenExp: new Date(),
  }),
  actions: {
    setToken(token: string) {
      const payloadBase64 = token.split(".")[1];
      const payloadString = Buffer.from(payloadBase64, "base64").toString(
        "binary"
      );
      console.log(payloadString);
      const payload = JSON.parse(payloadString);
      console.log(payload);
      this.isAuthenticated = true;
      this.tokenExp = new Date(payload.exp);
      console.log(this.tokenExp);
      localStorage.setItem("BearerToken", token);
    },
  },
});
