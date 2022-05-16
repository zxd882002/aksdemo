export default interface NumberGuessCheckResultResponse {
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
