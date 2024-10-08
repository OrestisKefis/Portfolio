using Portfolio.Admin.Users;
using Portfolio.Models.Interfaces;

namespace Portfolio.EndpointMappings;

public class UserMappings : IMapping
{
    public static void MapEndpoints(WebApplication app)
    {
        CreateUser.MapEndpoint(app);
        Login.MapEndpoint(app);
        Logout.MapEndpoint(app);
        ClaimsPresence.MapEndpoint(app);
        GetAll.MapEndpoint(app);
        UpdateUser.MapEndpoint(app);
        DeleteUser.MapEndpoint(app);
    }
}
