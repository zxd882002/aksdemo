import { ApiConfig } from "../../index";
import { Guid } from "guid-typescript";

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

const startGameApi = (onError: (e: unknown) => void): ApiConfig<StartGameRequest> => {
  return {
    method: "post",
    url: "/api/NumberGuess/StartGame",
    params: {
      header: {
        requestId: Guid.create().toString(),
      },
    } as StartGameRequest,
    config: undefined,
    onError: onError,
  } as ApiConfig<StartGameRequest>;
};

export { StartGameRequest, StartGameResponse, startGameApi };
