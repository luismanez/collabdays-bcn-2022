namespace Teamified.Api.Teams.Models;

public sealed class Team
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public IEnumerable<IdentityPrincipal> Owners { get; set; }
    public IEnumerable<IdentityPrincipal> Members { get; set; }
    public IEnumerable<Channel> Channels { get; set; }

    public Team()
    {
        Id = "unknown";
        DisplayName = "unknown";
        Description = "unknown";
        Owners = new List<IdentityPrincipal>();
        Members = new List<IdentityPrincipal>();
        Channels = new List<Channel>();
    }
}

public sealed class Channel
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDateTime { get; set; }

    public Channel()
    {
        Id = "unknown";
        DisplayName = "unknown";
        Description = "unknown";
    }
}

public sealed class IdentityPrincipal
{
    public Guid Id { get; set; }
    public string UserPrincipalName { get; set; }
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string JobTitle { get; set; }

    public IdentityPrincipal()
    {
        UserPrincipalName = "unknown";
        Email = "unknown";
        DisplayName = "unknown";
        JobTitle = "unknown";
    }
}