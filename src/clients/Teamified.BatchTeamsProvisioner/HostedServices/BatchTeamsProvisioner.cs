using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Kiota.Authentication.Azure;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Teamified.Sdk;

namespace Teamified.BatchTeamsProvisioner.HostedServices;

internal sealed class BatchTeamsProvisioner : IHostedService
{
    private readonly IConfiguration _configuration;

    public BatchTeamsProvisioner(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var clientId = _configuration.GetValue<string>("ClientId");
        var tenantId = _configuration.GetValue<string>("TenantId");
        var apiClientId = _configuration.GetValue<string>("ApiClientId");

        string[] allowedHosts = { "localhost" };
        string[] scopes = { $"api://{apiClientId}/Teams.Manage" };

        var options = new InteractiveBrowserCredentialOptions
        {
            ClientId = clientId,
            TenantId = tenantId,
            RedirectUri = new Uri("http://localhost")
        };

        var credentials = new InteractiveBrowserCredential(options);

        var authProvider = new AzureIdentityAuthenticationProvider(credentials, allowedHosts, null, scopes);
        var requestAdapter = new HttpClientRequestAdapter(authProvider);

        var teamifiedServiceClient = new TeamifiedApiClient(requestAdapter);

        var teams = await teamifiedServiceClient.Teams.GetAsync(cancellationToken: cancellationToken);

        foreach (var team in teams)
        {
            Console.WriteLine(team.DisplayName);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
