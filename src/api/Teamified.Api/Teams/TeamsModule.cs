using Teamified.Api.Teams.Endpoints;
using Teamified.Api.Teams.Models;

namespace Teamified.Api.Teams
{
    public static class TeamsModule
    {
        public static IServiceCollection RegisterTeamsModule(this IServiceCollection services)
        {            
            return services;
        }

        public static IEndpointRouteBuilder MapTeamsEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/teams", ListTeams.Handle)
                .Produces<IEnumerable<Team>>(200)
                .WithName("ListTeams");

            return endpoints;
        }
    }
}
