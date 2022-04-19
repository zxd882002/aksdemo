import { Guid } from "guid-typescript";
export default interface NumberGuessStartGameRequest {
  header: {
    requestId: Guid;
  };
}
