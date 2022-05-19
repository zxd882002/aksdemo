import { ApiConfig } from "../../index";

interface StartGameRequest {
  header: {
    requestId: string;
  };
}

interface StartGameResponse {
  header: {
    responseId: string;
    statusCode: number;
  };
  gameIdentifier: string;
  gameRetry: number;
  gameStatus: string;
  gameHistories: string[];
}

const startGameApi = (requestId: string) => {
  return {
    method: "post",
    url: "/api/NumberGuess/StartGame",
    params: {
      header: {
        requestId: requestId,
      },
    } as StartGameRequest,
    config: undefined,
  } as ApiConfig<StartGameRequest>;
};

export { StartGameRequest, StartGameResponse, startGameApi };
