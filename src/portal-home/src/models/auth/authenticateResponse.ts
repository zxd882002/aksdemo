export default interface AuthenticationResponse {
  header: {
    responseId: string;
    statusCode: number;
  };
  authSuccess: boolean;
  authToken: string;
}
