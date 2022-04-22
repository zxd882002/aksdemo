import { Guid } from "guid-typescript";
export default interface NumberGuessStartGameResponse {
  header: {
    responseId: string;
  };
  gameIdentifier: string;
  gameRetry: number;
  gameStatus: string;
  gameHistories: string[];
}
