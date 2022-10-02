import { AadTokenProvider } from '@microsoft/sp-http';
import { AuthenticationProvider, RequestInformation } from '@microsoft/kiota-abstractions';



export class AadTokenAuthenticationProvider implements AuthenticationProvider {
  private static readonly authorizationHeaderKey: string = "Authorization";

  // UPDATE this to your AAD App value !!!!!!!!!!
  private readonly azureAdApplicationIdUri: string = "UPDATE this to your AAD App value";

  constructor(public readonly tokenProvider: AadTokenProvider) {
  }

  public authenticateRequest = async (
    request: RequestInformation,
    additionalAuthenticationContext?: Record<string, unknown>
  ): Promise<void> => {
    if (!request) {
      throw new Error("request info cannot be null");
    }
    if (additionalAuthenticationContext &&
      additionalAuthenticationContext.claims &&
      request.headers[AadTokenAuthenticationProvider.authorizationHeaderKey]) {
      delete request.headers[AadTokenAuthenticationProvider.authorizationHeaderKey];
    }
    if (!request.headers ||
      !request.headers[AadTokenAuthenticationProvider.authorizationHeaderKey]) {
      const token = await this.tokenProvider.getToken(this.azureAdApplicationIdUri);
      if (!request.headers) {
        request.headers = {};
      }
      if (token) {
        request.headers[AadTokenAuthenticationProvider.authorizationHeaderKey] = `Bearer ${token}`;
      }
    }
  };
}
