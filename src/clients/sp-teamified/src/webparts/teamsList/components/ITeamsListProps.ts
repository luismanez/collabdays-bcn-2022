import { AadHttpClientFactory, AadTokenProviderFactory } from '@microsoft/sp-http';

export interface ITeamsListProps {
  description: string;
  isDarkTheme: boolean;
  environmentMessage: string;
  hasTeamsContext: boolean;
  userDisplayName: string;
  aadHttpClientFactory: AadHttpClientFactory;
  aadTokenProviderFactory: AadTokenProviderFactory;
}