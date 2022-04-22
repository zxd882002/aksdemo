import { Guid } from "guid-typescript";
export default interface NumberGuessCheckResultRequest {
  header: {
    requestId: string;
  };
  gameIdentifier: String;
  input: number[];
}
