import { Guid } from "guid-typescript";
export default interface NumberGuessStartGameResponse {
  header: {
    responseId: Guid;
  };
  gameIdentifier: string;
  gameRetry: number;
  gameStatus: string;
  gameHistories: string[];
}
