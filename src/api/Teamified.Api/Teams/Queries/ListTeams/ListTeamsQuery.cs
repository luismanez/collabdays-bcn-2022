using MediatR;
using Teamified.Api.Teams.Models;

namespace Teamified.Api.Teams.Queries.ListTeams;

public class ListTeamsQuery : IRequest<IEnumerable<Team>>
{

}

public class ListTeamsQueryHandler : IRequestHandler<ListTeamsQuery, IEnumerable<Team>>
{
    public async Task<IEnumerable<Team>> Handle(ListTeamsQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(50, cancellationToken);

        return new List<Team>
        {
            new Team { Id = "Team 1"},
            new Team { Id = "Team 2"},
            new Team { Id = "Team 3"}
        };
    }
}