using MediatR;
using Teamified.Api.Teams.Queries.ListTeams;

namespace Teamified.Api.Teams.Endpoints
{
    public class ListTeams
    {
        [Authorize]
        public static async Task<IResult> Handle(IMediator mediator)
        {
            var teams = await mediator.Send(new ListTeamsQuery());

            return Results.Ok(teams);
        }
    }
}
