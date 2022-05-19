import { defineStore } from "pinia";

export const useStore = defineStore("main", {
  state: () => ({
    isAuthenticated: false,
    tokenExp: new Date(),
  }),
  actions: {
    setToken(token: string) {
      const tokenSplit = token.split(".");
      console.log(tokenSplit);
      const payloadBase64 = tokenSplit[1];
      console.log(payloadBase64);
      const payloadString = Buffer.from(payloadBase64, "base64").toString(
        "binary"
      );
      console.log(payloadString);
      const payload = eval(payloadString);
      console.log(payload);
      this.isAuthenticated = true;
      localStorage.setItem("BearerToken", token);
    },
  },
});
