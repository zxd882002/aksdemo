import { Guid } from "guid-typescript";
export default interface NumberGuessCheckResultResponse {
  header: {
    responseId: string;
  };
  gameIdentifier: string;
  gameRetry: number;
  gameStatus: string;
  gameHistories: string[];
  gameAnswer: string | undefined;
}
