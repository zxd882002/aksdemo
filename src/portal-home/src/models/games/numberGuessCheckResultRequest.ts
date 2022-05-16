export default interface NumberGuessCheckResultRequest {
  header: {
    requestId: string;
  };
  gameIdentifier: String;
  input: number[];
}
