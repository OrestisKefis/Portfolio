using Microsoft.AspNetCore.Http.HttpResults;
using Portfolio.Models.Interfaces;

namespace Portfolio.Admin.Users;

internal sealed class ClaimsPresence : IEndpoint
{
    public static Results<Ok, NoContent> Check(HttpContext httpContext)
    {
        var cookie = httpContext.Request.Cookies[".AspNetCore.Cookies"];
        if (cookie is null)
            return TypedResults.NoContent();

        return TypedResults.Ok();
    }

    public static void MapEndpoint(WebApplication app)
    {
        app.MapPost("/admin/claims", Check);
    }
}
