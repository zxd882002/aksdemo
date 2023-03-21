import { ApiConfig } from "../../index";

interface StartGameResponse {
  gameIdentifier: string;
  gameRetry: number;
  gameStatus: string;
  gameHistories: string[];
}

const startGameApi = (onError: (e: unknown) => void): ApiConfig<undefined> => {
  return {
    method: "post",
    url: "/api/NumberGuess/StartGame",
    params: undefined,
    config: undefined,
    onError: onError,
  } as ApiConfig<undefined>;
};

export { StartGameResponse, startGameApi };
