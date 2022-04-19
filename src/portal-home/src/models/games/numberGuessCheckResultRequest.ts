import { Guid } from "guid-typescript";
export default interface NumberGuessCheckResultRequest {
  header: {
    requestId: Guid;
  };
  gameIdentifier: String;
  input: number[];
}
