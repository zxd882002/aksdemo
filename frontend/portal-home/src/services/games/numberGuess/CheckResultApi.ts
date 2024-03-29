import type { ApiConfig } from "../..";

interface CheckResultRequest {
  gameIdentifier: string;
  input: number[];
}

interface CheckResultResponse {
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
      gameIdentifier: gameIdentifier,
      input: input,
    } as CheckResultRequest,
    config: undefined,
    onError: onError,
  } as ApiConfig<CheckResultRequest>;
};

export { type CheckResultRequest, type CheckResultResponse, checkResultApi };
