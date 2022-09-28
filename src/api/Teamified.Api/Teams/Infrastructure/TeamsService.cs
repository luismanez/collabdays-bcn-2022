using Microsoft.Graph;
using Teamified.Api.Teams.Interfaces;

namespace Teamified.Api.Teams.Infrastructure;
public class TeamsService : ITeamsService
{
    private readonly GraphServiceClient _graphServiceClient;

    public TeamsService(GraphServiceClient graphServiceClient)
    {
        _graphServiceClient = graphServiceClient;
    }
    public Task<Models.Team> GetTeamByGroupId(Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Models.Team>> ListTeams()
    {
        var teamsCollection = await _graphServiceClient.Groups
            .Request()
            .Filter("resourceProvisioningOptions/Any(x:x eq 'Team')")
            .GetAsync();

        var teams = teamsCollection.CurrentPage;

        var result = teams.Select(t => new Models.Team
        {
            Id = t.Id, Description = t.Description, DisplayName = t.DisplayName
        });

        return result;
    }

    public Task ProvisionTeam(Models.Team team)
    {
        throw new NotImplementedException();
    }
}
