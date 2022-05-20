import { ApiConfig } from "../../index";
import { Guid } from "guid-typescript";

interface CheckResultRequest {
  header: {
    requestId: string;
  };
  gameIdentifier: string;
  input: number[];
}

interface CheckResultResponse {
  header: {
    responseId: string;
    statusCode: number;
  };
  gameIdentifier: string;
  gameRetry: number;
  gameStatus: string;
  gameHistories: string[];
  gameAnswer: string | undefined;
}

const checkResultApi = (
  gameIdentifier: string,
  input: number[],
  onError: (e: unknown) => void
): ApiConfig<CheckResultRequest> => {
  return {
    method: "post",
    url: "/api/NumberGuess/CheckResult",
    params: {
      header: {
        requestId: Guid.create().toString(),
      },
      gameIdentifier: gameIdentifier,
      input: input,
    } as CheckResultRequest,
    config: undefined,
    onError: onError,
  } as ApiConfig<CheckResultRequest>;
};

export { CheckResultRequest, CheckResultResponse, checkResultApi };
