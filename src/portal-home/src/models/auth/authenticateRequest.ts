export default interface AuthenticationRequest {
  header: {
    requestId: string;
  };
  traceId: string;
  passwordHash: string;
}
