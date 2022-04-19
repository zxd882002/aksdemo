import { Guid } from "guid-typescript";
export default interface NumberGuessCheckResultResponse {
  header: {
    responseId: Guid;
  };
  gameIdentifier: string;
  gameRetry: number;
  gameStatus: string;
  gameHistories: string[];
  gameAnswer: string | undefined;
}
