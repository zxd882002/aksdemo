import { defineStore } from "pinia";

export const useStore = defineStore("main", {
  state: () => ({
    token: "",
  }),
  actions: {
    setToken(token: string) {
      this.token = token;
      localStorage.setItem("BearerToken", token);
    },
  },
});
