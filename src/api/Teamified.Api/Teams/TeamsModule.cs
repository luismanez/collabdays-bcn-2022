using Teamified.Api.Teams.Endpoints;
using Teamified.Api.Teams.Infrastructure;
using Teamified.Api.Teams.Interfaces;
using Teamified.Api.Teams.Models;

namespace Teamified.Api.Teams
{
    public static class TeamsModule
    {
        public static IServiceCollection RegisterTeamsModule(this IServiceCollection services)
        {
            services.AddScoped<ITeamsService, TeamsService>();

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
