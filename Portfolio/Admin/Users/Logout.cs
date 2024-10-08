using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Portfolio.Models.Interfaces;

namespace Portfolio.Admin.Users;

public sealed class Logout : IEndpoint
{
    internal static SignOutHttpResult Handle()
    {
        return TypedResults.SignOut(null, [CookieAuthenticationDefaults.AuthenticationScheme]);
    }

    public static void MapEndpoint(WebApplication app)
    {
        app.MapPost("/admin/logout", Handle);
    }
}
