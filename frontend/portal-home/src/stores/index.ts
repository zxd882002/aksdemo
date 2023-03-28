import { defineStore } from "pinia";

export const useStore = defineStore("main", {
  state: () => ({
    accessTokenExp: new Date(),
    refreshTokenExp: new Date(),
  }),
}); 
