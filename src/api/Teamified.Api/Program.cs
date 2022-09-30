using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Teamified.Api.Teams;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => c.AddServer(new OpenApiServer() // Required by Kiota when creating the SDK
    {
        Url = "https://localhost:7295"
    }));

builder.Services.AddMediatR(m => m.AsScoped(), typeof(Program));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddMicrosoftIdentityWebApi(builder.Configuration)
            .EnableTokenAcquisitionToCallDownstreamApi(options => { builder.Configuration.Bind("AzureAd", options); })
               .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
               .AddInMemoryTokenCaches(); // Advice: Use Distributed TokenCache (redis, sql...)

builder.Services.AddAuthorization(cfg => {
    cfg.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddHttpContextAccessor();

builder.Services.RegisterTeamsModule();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapTeamsEndpoints();

app.Run();